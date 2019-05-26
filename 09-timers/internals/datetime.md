# DateTime Internals

Let's dive deeper into the source code for .NET Framework, .NET Core and Mono.

## Source code of DateTime in .NET Framework

In the .NET Framework, the `DateTime` structure is represented by a `long` value called `Ticks`.
One tick equals to `100 ns`, ticks are counted starting from *12:00 AM January 1, year 1 A.D* (Gregorian Calendar).

We already know that there is another native Windows time structure for called `FILETIME`.
It also uses `100 ns`-ticks, but the starting point is *January 1, 1601 (UTC)*.
You can get current `FILETIME` via `GetSystemTimeAsFileTime`.

Now, let's look at the source code of [`DateTime`](https://referencesource.microsoft.com/#mscorlib/system/datetime.cs)
  (.NET Framework 4.7.2 version is presented):

```cs
public static DateTime UtcNow {
  [System.Security.SecuritySafeCritical]  // auto-generated
  get {
    Contract.Ensures(Contract.Result<DateTime>().Kind == DateTimeKind.Utc);
    // following code is tuned for speed.
    // Don't change it without running benchmark.
    long ticks = 0;
    ticks = GetSystemTimeAsFileTime();

#if FEATURE_LEGACYNETCF
    // Windows Phone 7.0/7.1 return the ticks up to millisecond,
    // not up to the 100th nanosecond.
    if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
    {
      long ticksms = ticks / TicksPerMillisecond;
      ticks = ticksms * TicksPerMillisecond;
    }
#endif
    return new DateTime( ((UInt64)(ticks + FileTimeOffset)) | KindUtc);
  }
}

[System.Security.SecurityCritical]  // auto-generated
[MethodImplAttribute(MethodImplOptions.InternalCall)]
internal static extern long GetSystemTimeAsFileTime();
```

The implementation is based on `GetSystemTimeAsFileTime` and use `FileTimeOffset` for conversion.
You may have noticed `KindUtc` in the constructor argument.
In fact, `DateTime` keeps actual `Ticks` only in bits 01-62 of the `dateData` field;
  bits 63-64 are used for `DateTimeKind` (`Local`, `Utc`, or `Unspecified`).

In the .NET Framework source code, there are a lot of tricks that make it backward compatible.
As you can see, `DateTime.UtcNow` contains logic for Windows Phone 7.0/7.1 which
  require millisecond resolution.

## Source code of DateTime in .NET Core

In this section, we will look at the important implementation details of the `DateTime` class in .NET Core v2.1.0.
The implementation can be changed in future versions of .NET Core, but no major changes are expected.

[`src/vm/ecalllist.h`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/vm/ecalllist.h#L1286):
```cpp
FCClassElement("DateTime", "System", gDateTimeFuncs)
```

This line means that implementation of methods in `System.DateTime` will be provided via `gDateTimeFuncs`.

[`src/vm/ecalllist.h`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/vm/ecalllist.h#L144):
```cpp
FCFuncStart(gDateTimeFuncs)
  FCFuncElement("GetSystemTimeAsFileTime", SystemNative::__GetSystemTimeAsFileTime)
FCFuncEnd()
```

As we already know, `DateTime.UtcNow` uses `GetSystemTimeAsFileTime` internally.
In these lines, we want to say that the implementation of `GetSystemTimeAsFileTime` in `gDateTimeFuncs`
  will be declared as `SystemNative::__GetSystemTimeAsFileTime`.
The implementation of this method was pretty simple in .NET Core 1.x:

[classlibnative/bcltype/system.cpp/system.cpp](https://github.com/dotnet/coreclr/blob/v1.0.0/src/classlibnative/bcltype/system.cpp#L48)
(.NET Core 1.0):
```cpp
FCIMPL0(INT64, SystemNative::__GetSystemTimeAsFileTime)
{
  FCALL_CONTRACT;

  INT64 timestamp;

  ::GetSystemTimeAsFileTime((FILETIME*)&timestamp);

#if BIGENDIAN
  timestamp = (INT64)(((UINT64)timestamp >> 32) |
                      ((UINT64)timestamp << 32));
#endif

  return timestamp;
}
FCIMPLEND;
```

In these lines, we call `GetSystemTimeAsFileTime` to get the current `timestamp` in the `FILETIME` format.
For big-endian platforms, we also swap the low and high 32-bit parts of `timestamp`.
If you want to know more about what `FCIMPL0` means and how it works, the definition can be found in
  [`src/vm/fcall.h`](https://github.com/dotnet/coreclr/blob/v1.0.0/src/vm/fcall.h)
  (the detailed explanation of CoreCLR internals is out of scope of this book).

In .NET Core 2.0, the implementation was updated:

[`classlibnative/bcltype/system.cpp/system.cpp`](https://github.com/dotnet/coreclr/blob/v2.0.0/src/classlibnative/bcltype/system.cpp#L67)
(.NET Core 2.0):
```cpp
pfnGetSystemTimeAsFileTime g_pfnGetSystemTimeAsFileTime =
  &InitializeGetSystemTimeAsFileTime;

FCIMPL0(INT64, SystemNative::__GetSystemTimeAsFileTime)
{
  FCALL_CONTRACT;

  INT64 timestamp;
  g_pfnGetSystemTimeAsFileTime((FILETIME*)&timestamp);

#if BIGENDIAN
  timestamp = (INT64)(((UINT64)timestamp >> 32) | ((UINT64)timestamp << 32));
#endif

  return timestamp;
}
FCIMPLEND;
```

Now `__GetSystemTimeAsFileTime` calls `g_pfnGetSystemTimeAsFileTime`
  which is initialized in `InitializeGetSystemTimeAsFileTime`:

```cpp
void WINAPI InitializeGetSystemTimeAsFileTime(LPFILETIME lpSystemTimeAsFileTime)
{
  pfnGetSystemTimeAsFileTime func = NULL;

#ifndef FEATURE_PAL
  HMODULE hKernel32 = WszLoadLibrary(W("kernel32.dll"));
  if (hKernel32 != NULL)
  {
    func = (pfnGetSystemTimeAsFileTime)
      GetProcAddress(hKernel32, "GetSystemTimePreciseAsFileTime");
  }
  if (func == NULL)
#endif
  {
    func = &::GetSystemTimeAsFileTime;
  }

  g_pfnGetSystemTimeAsFileTime = func;
  func(lpSystemTimeAsFileTime);
}
```

This changed was made in [coreclr#9736](https://github.com/dotnet/coreclr/pull/9736)
  (see also: [coreclr#5061](https://github.com/dotnet/coreclr/issues/5061)).
Thus, in .NET Core 2.0, `DateTime.UtcNow` uses GetSystemTimePreciseAsFileTime on Windows (if available).
However, another problems was introduced: [coreclr#14187](https://github.com/dotnet/coreclr/issues/14187):
  on misconfigured systems, the `GetSystemTimePreciseAsFileTime` drifts and returns incorrect results.
In .NET Core 2.1, a workaround was added:

[`classlibnative/bcltype/system.cpp/system.cpp`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/classlibnative/bcltype/system.cpp#L45)
(.NET Core 2.1):

```cpp
func = (pfnGetSystemTimeAsFileTime)
    GetProcAddress(hKernel32, "GetSystemTimePreciseAsFileTime");
if (func != NULL)
{
  // GetSystemTimePreciseAsFileTime exists and we'd like to use it.  However, on
  // misconfigured systems, it's possible for the "precise" time to be inaccurate:
  //     https://github.com/dotnet/coreclr/issues/14187
  // If it's inaccurate, though, we expect it to be wildly inaccurate, so as a
  // workaround/heuristic, we get both the "normal" and "precise" times, and as
  // long as they're close, we use the precise one. This workaround can be removed
  // when we better understand what's causing the drift and the issue is no longer
  // a problem or can be better worked around on all targeted OSes.

  FILETIME systemTimeResult;
  ::GetSystemTimeAsFileTime(&systemTimeResult);

  FILETIME preciseSystemTimeResult;
  func(&preciseSystemTimeResult);

  LONG64 systemTimeLong100ns =
      (LONG64)((((ULONG64)systemTimeResult.dwHighDateTime) << 32) |
                 (ULONG64)systemTimeResult.dwLowDateTime);
  LONG64 preciseSystemTimeLong100ns = 
      (LONG64)((((ULONG64)preciseSystemTimeResult.dwHighDateTime) << 32) 
               | (ULONG64)preciseSystemTimeResult.dwLowDateTime);

  const INT32 THRESHOLD_100NS = 1000000; // 100ms
  if (abs(preciseSystemTimeLong100ns - systemTimeLong100ns) > THRESHOLD_100NS)
  {
     // Too much difference.  Don't use GetSystemTimePreciseAsFileTime.
     func = NULL;
  }
}
```

On Windows, `GetSystemTimeAsFileTime` and `GetSystemTimePreciseAsFileTime` are
  WinAPI functions which are provided by the operating system.
`GetSystemTimePreciseAsFileTime` is used only on Windows because it requires `kernel32.dll`.
`GetSystemTimeAsFileTime` should be implemented manually on non-Windows systems.
Let's start with the .NET Core 1.x implementation:

[`pal/src/file/filetime.cpp`](https://github.com/dotnet/coreclr/blob/v1.0.0/src/pal/src/file/filetime.cpp#L502)
(.NET Core 1.0):
```cpp
VOID
PALAPI
GetSystemTimeAsFileTime(
            OUT LPFILETIME lpSystemTimeAsFileTime)
{
  struct timeval Time;

  PERF_ENTRY(GetSystemTimeAsFileTime);
  ENTRY("GetSystemTimeAsFileTime(lpSystemTimeAsFileTime=%p)\n", 
        lpSystemTimeAsFileTime);

  if ( gettimeofday( &Time, NULL ) != 0 )
  {
    ASSERT("gettimeofday() failed");
    /* no way to indicate failure, so set time to zero */
    *lpSystemTimeAsFileTime = FILEUnixTimeToFileTime( 0, 0 );
  }
  else
  {
    /* use (tv_usec * 1000) because 2nd arg is in nanoseconds */
    *lpSystemTimeAsFileTime = FILEUnixTimeToFileTime( Time.tv_sec,
                                                      Time.tv_usec * 1000 );
  }

  LOGEXIT("GetSystemTimeAsFileTime returns.\n");
  PERF_EXIT(GetSystemTimeAsFileTime);
}
```

The `GetSystemTimeAsFileTime` function uses the `gettimeofday` function to get current time in the `timeval` format.
This value contains `tv_sec` and `tv_usec` fields and can be converted to requsted format via `FILEUnixTimeToFileTime`.
If `gettimeofday` fails ( an unlikely event), the zero value will be returned.

In .NET Core 2.x, the implementation was changed:

[`pal/src/file/filetime.cpp`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/pal/src/file/filetime.cpp#L151)
(.NET Core 2.1):
```cpp
VOID
PALAPI
GetSystemTimeAsFileTime(
            OUT LPFILETIME lpSystemTimeAsFileTime)
{
  PERF_ENTRY(GetSystemTimeAsFileTime);
  ENTRY("GetSystemTimeAsFileTime(lpSystemTimeAsFileTime=%p)\n", 
        lpSystemTimeAsFileTime);

#if HAVE_WORKING_CLOCK_GETTIME
  struct timespec Time;
  if (clock_gettime(CLOCK_REALTIME, &Time) == 0)
  {
    *lpSystemTimeAsFileTime = FILEUnixTimeToFileTime( Time.tv_sec, Time.tv_nsec );
  }
#else
  struct timeval Time;
  if (gettimeofday(&Time, NULL) == 0)
  {
    /* use (tv_usec * 1000) because 2nd arg is in nanoseconds */
    *lpSystemTimeAsFileTime = FILEUnixTimeToFileTime( Time.tv_sec,
                                                      Time.tv_usec * 1000);
  }
#endif
  else
  {
      /* no way to indicate failure, so set time to zero */
      ASSERT("clock_gettime or gettimeofday failed");
      *lpSystemTimeAsFileTime = FILEUnixTimeToFileTime( 0, 0 );
  }

  LOGEXIT("GetSystemTimeAsFileTime returns.\n");
  PERF_EXIT(GetSystemTimeAsFileTime);
}
```

In [coreclr#9772](https://github.com/dotnet/coreclr/pull/9772), it was decided
  to use `clock_gettime(CLOCK_REALTIME)` in this method to get better accuracy.

We have to more function: `FILEUnixTimeToFileTime`.
The implementation was the same for .NET Core 1.0--.NET Core 2.1:

[`pal/src/file/filetime.cpp`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/pal/src/file/filetime.cpp#L221):
```cpp
/*++
Convert a time_t value to a win32 FILETIME structure, as described in
MSDN documentation. time_t is the number of seconds elapsed since 
00:00 01 January 1970 UTC (Unix epoch), while FILETIME represents a 
64-bit number of 100-nanosecond intervals that have passed since 00:00 
01 January 1601 UTC (win32 epoch).
--*/
FILETIME FILEUnixTimeToFileTime( time_t sec, long nsec )
{
    __int64 Result;
    FILETIME Ret;

    Result = ((__int64)sec + SECS_BETWEEN_1601_AND_1970_EPOCHS) * SECS_TO_100NS +
        (nsec / 100);

    Ret.dwLowDateTime = (DWORD)Result;
    Ret.dwHighDateTime = (DWORD)(Result >> 32);

    TRACE("Unix time = [%ld.%09ld] converts to Win32 FILETIME = [%#x:%#x]\n", 
          sec, nsec, Ret.dwHighDateTime, Ret.dwLowDateTime);

    return Ret;
}
```

`FILEUnixTimeToFileTime` converts values from `gettimeofday` to the `FILETIME` format
  (see comments in the code for clarifications).

Now we know all implementation details for DateTime in .NET Core 1.0--.NET Core 2.1 on all OS.

### Source code of DateTime in Mono

Let's also look at the same code in the source code of Mono 5.12.0.226.
We already know the managed implementation of DateTime: Mono reuses the reference source code of .NET Framework.
Thus, we should learn how `GetSystemTimeAsFileTime` is implemented.
Let's start to observe the source code from `icall-def.h`.

[`icall-def.h`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/metadata/icall-def.h#L189):

```cpp
ICALL_TYPE(DTIME, "System.DateTime", DTIME_1)
ICALL(DTIME_1, "GetSystemTimeAsFileTime", mono_100ns_datetime)
```

Here we declare that `System.DateTime.GetSystemTimeAsFileTime` will be implemented in `mono_100ns_datetime`.
All implementations of time-related functions in Mono are placed in `mono-time.c`.
Let's look at the Windows implementation:

[`mono-time.c`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/utils/mono-time.c#L82):
```cpp
#ifdef HOST_WIN32
#include <windows.h>
//...

/* Returns the number of 100ns ticks since Jan 1, 1601, UTC timezone */
gint64
mono_100ns_datetime (void)
{
    ULARGE_INTEGER ft;

    if (sizeof(ft) != sizeof(FILETIME))
        g_assert_not_reached ();

    GetSystemTimeAsFileTime ((FILETIME*) &ft);
    return ft.QuadPart;
}

#else
// ...
```

On Windows, `mono_100ns_datetime` just calls `GetSystemTimeAsFileTime` (a WinAPI function) to get the requested `FILETIME`.
Now let's look at the implementation on other operating systems.

[`mono-time.c`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/utils/mono-time.c#L242):
```cpp
/*
 * Magic number to convert unix epoch start to windows epoch start
 * Jan 1, 1970 into a value which is relative to Jan 1, 1601.
 */
#define EPOCH_ADJUST    ((guint64)11644473600LL)

/* Returns the number of 100ns ticks since 1/1/1601, UTC timezone */
gint64
mono_100ns_datetime (void)
{
    struct timeval tv;
    if (gettimeofday (&tv, NULL) == 0)
        return mono_100ns_datetime_from_timeval (tv);
    return 0;
}

gint64
mono_100ns_datetime_from_timeval (struct timeval tv)
{
    return (((gint64)tv.tv_sec + EPOCH_ADJUST) * 1000000 + tv.tv_usec) * 10;
}

#endif
```

On Unix, Mono uses `gettimeofday` and convert the returned value to `FILETIME`
  (we have already seen such conversion in .NET Core sources).

## Application

On Unix + .NET Core 1.x/Mono, `DateTime.UtcNow` is emulated via `gettimeofday`.
In this case, we have to convert
  the returned `timeval` (Unix epoch, *01 January 1970 UTC*)
  to `FILETIME` (Win32 epoch, *01 January 1601 UTC*).
Both epochs are Gregorian, the difference between years: `1970 - 1601 = 369`.
We have one leap year per four years, a number of leap years can be easily calculated: `369 / 4 = 92`.
However, the interval of years `1601..1970` contains 3 non-leap years (1700, 1800, 1900) which are divided into 4.
Thus, we have `92 - 3 = 89` leap years (and 280 non-leap years).
So, it's easy to calculate amount of days between epochs: `89 * 366 + 280 * 365 = 134774`.
We also know the amount of seconds per day (`60 * 60 * 24 = 86400`).
Thus, amount of seconds between epochs equals to `134774 * 86400 = 11644473600`.
`timeval` contains to fields: `tv_sec` (seconds) and `tv_usec` (microseconds), but the 1 tick in `FILETIME` equals to `100ns`.
For conversion, we have to
  add 11644473600 to `tv_sec` (epoch conversion),
  multiply it by $10^7$ (amount of `100ns` units in `1sec`),
  add `tv_usec * 10` (there is ten `100ns` units in `1usec`):

```cs
Win32EpochTime = (UnixEpochTime.tv_sec + 11644473600) * 10000000 + 
                  UnixEpochTime.tv_usec * 10;
```

As soon as we get the current time in the `FILETIME` format,
  we have to convert it to the DateTime epoch (*12:00 AM January 1, year 1 A.D.*) and add `KindUtc` flag.
Since both `FILETIME` and `DateTime` use the same `100ns` ticks,
  the conversion can be implemented by adding `FileTimeOffset`
  which can be easily calculated with the help of few private `DateTime` constants:

```cs
long TicksPerMillisecond = 10000;                 // 10000
long TicksPerSecond = TicksPerMillisecond * 1000; // 10000000
long TicksPerMinute = TicksPerSecond * 60;        // 600000000
long TicksPerHour = TicksPerMinute * 60;          // 36000000000
long TicksPerDay = TicksPerHour * 24;             // 864000000000

int DaysPerYear = 365;                            // 365
int DaysPer4Years = DaysPerYear * 4 + 1;          // 1461
int DaysPer100Years = DaysPer4Years * 25 - 1;     // 36524
int DaysPer400Years = DaysPer100Years * 4 + 1;    // 146097
int DaysTo1601 = DaysPer400Years * 4;             // 584388
long FileTimeOffset = DaysTo1601 * TicksPerDay;   // 504911232000000000

ticks = GetSystemTimeAsFileTime();
return new DateTime(((UInt64)(ticks + FileTimeOffset)) | KindUtc);
```

