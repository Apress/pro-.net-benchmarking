# Environment.TickCount Internals

Let's look at how `TickCount` is implemented in .NET Framework, .NET Core, and Mono.

## Source code of TickCount in .NET Framework

In .NET Framework, `TickCount` is an extern property (.NET Framework 4.7.2 is shown):

[`Environment.cs`](https://referencesource.microsoft.com/#mscorlib/system/environment.cs):
```cs
public static extern int TickCount {
    [MethodImplAttribute(MethodImplOptions.InternalCall)]
    get;
}
```

Internally, it calls the `GetTickCount64` WinAPI function.

## Source code of TickCount in .NET Core

The managed implementation is the same with .NET Framework:

[`Environment.cs`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/mscorlib/src/System/Environment.cs#L71):
```cs
public static extern int TickCount {
    [MethodImplAttribute(MethodImplOptions.InternalCall)]
    get;
}
```

We already know that we can find the mapping for extern functions in `ecalllist.h`.

[`src/vm/ecalllist.h`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/vm/ecalllist.h#L1296):
```c
FCClassElement("Environment", "System", gEnvironmentFuncs)
```

This line means that implementation of methods in `System.Environment` will be provided via `gEnvironmentFuncs`.
We can find the corresponding definition in the same file.

[`src/vm/ecalllist.h`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/vm/ecalllist.h#L148):
```c
FCFuncStart(gEnvironmentFuncs)
    FCFuncElement("get_TickCount", SystemNative::GetTickCount)
```

`TickCount` is a property with an extern `get` declaration.
It means that the target logic should place at the `get_TickCount` method.
`FCFuncElement` states that we should find the implementation in `SystemNative::GetTickCount`.
Let's find its implementation.

[`classlibnative/bcltype/system.cpp`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/classlibnative/bcltype/system.cpp#L104):
```c
FCIMPL0(UINT32, SystemNative::GetTickCount)
{
  FCALL_CONTRACT;
  return ::GetTickCount();
}
FCIMPLEND;
```

`SystemNative::GetTickCount` just redirect us to the global `GetTickCount` function.
On Windows, it's a WinAPI function which is provided by OS.
On Unix, we have to implement it manually.
Implementations of time-related functions can be found in `time.cpp`.

[`pal/src/misc/time.cpp`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/pal/src/misc/time.cpp#L155):
```c
/*++
The GetTickCount function retrieves the number of milliseconds that
have elapsed since the system was started. It is limited to the
resolution of the system timer. To obtain the system timer resolution,
use the GetSystemTimeAdjustment function.

The return value is the number of milliseconds that have elapsed since
the system was started.
In the ROTOR implementation, the return value is the elapsed time since
the start of the epoch.
--*/
DWORD
PALAPI
GetTickCount(
         VOID)
{
    DWORD retval = 0;
    PERF_ENTRY(GetTickCount);
    ENTRY("GetTickCount ()\n");

    // Get the 64-bit count from GetTickCount64 and truncate the results.
    retval = (DWORD) GetTickCount64();

    LOGEXIT("GetTickCount returns DWORD %u\n", retval);
    PERF_EXIT(GetTickCount);
    return retval;
}
```

We can see that `GetTickCount` calls to `GetTickCount64` which is defined in the same file.

[`pal/src/misc/time.cpp`](https://github.com/dotnet/coreclr/blob/v2.1.0/src/pal/src/misc/time.cpp#L326):
```c
/*++
Returns a 64-bit tick count with a millisecond resolution. It tries its best
to return monotonically increasing counts and avoid being affected by changes
to the system clock (either due to drift or due to explicit changes to system
time).
--*/
PALAPI
ULONGLONG
GetTickCount64()
{
  ULONGLONG retval = 0;

#if HAVE_CLOCK_MONOTONIC_COARSE || HAVE_CLOCK_MONOTONIC
  {
    clockid_t clockType = 
#if HAVE_CLOCK_MONOTONIC_COARSE
      CLOCK_MONOTONIC_COARSE; // good enough resolution, fastest speed
#else
      CLOCK_MONOTONIC;
#endif
    struct timespec ts;
    if (clock_gettime(clockType, &ts) != 0)
    {
      ASSERT("clock_gettime(CLOCK_MONOTONIC*) failed; errno is %d (%s)\n",
          errno, strerror(errno));
      goto EXIT;
    }
    retval = (ts.tv_sec * tccSecondsToMillieSeconds)+
             (ts.tv_nsec / tccMillieSecondsToNanoSeconds);
  }
#elif HAVE_MACH_ABSOLUTE_TIME
  {
    // use denom == 0 to indicate that s_TimebaseInfo is uninitialised.
    if (s_TimebaseInfo.denom == 0)
    {
      ASSERT("s_TimebaseInfo is uninitialized.\n");
      goto EXIT;
    }
    retval = (mach_absolute_time() * s_TimebaseInfo.numer 
                                   / s_TimebaseInfo.denom) / 
             tccMillieSecondsToNanoSeconds;
  }
#elif HAVE_GETHRTIME
  {
    retval = (ULONGLONG)(gethrtime() / tccMillieSecondsToNanoSeconds);
  }
#elif HAVE_READ_REAL_TIME
  {
    timebasestruct_t tb;
    read_real_time(&tb, TIMEBASE_SZ);
    if (time_base_to_time(&tb, TIMEBASE_SZ) != 0)
    {
      ASSERT("time_base_to_time() failed; errno is %d (%s)\n",
          errno, strerror(errno));
      goto EXIT;
    }
    retval = (tb.tb_high * tccSecondsToMillieSeconds)+
             (tb.tb_low / tccMillieSecondsToNanoSeconds);
  }
#else
  {
    struct timeval tv;
    if (gettimeofday(&tv, NULL) == -1)
    {
      ASSERT("gettimeofday() failed; errno is %d (%s)\n",
          errno, strerror(errno));
      goto EXIT;
    }
    retval = (tv.tv_sec * tccSecondsToMillieSeconds) + 
             (tv.tv_usec / tccMillieSecondsToMicroSeconds);
  }
#endif // HAVE_CLOCK_MONOTONIC
EXIT:    
    return retval;
}
```

The implementation of `GetTickCount64` is a little tricky.
Here we check which API is available and try to use a function with best achievable (on the local system) resolution and latency.
Here are the list of target function (sorted by priority):

* `clock_gettime()`:
We try to use `CLOCK_MONOTONIC_COARSE` (if it's available) instead of `CLOCK_MONOTONIC` as `clockid_t`
  because `TickCount` should have small latency
  (the resolution doesn't matter here because `TickCount` returns milliseconds).
* `mach_absolute_time()`
* `gethrtime()`
* `read_real_time()`
* `gettimeofday()`

Now let's check how it's implemented in Mono.

## Source code of TickCount in Mono

Mono uses another logic for `TickCount`, so let's look at the implementation.
We consider Mono 5.12.0.226 here (in the future, the implementation can be changed).
The managed implementation is the same with .NET Framework and .NET Core:
  we have an `extern` property with a getter.
We can find the mapping between extern properties and native implementation in `icall-def.h`.

[`icall-def.h`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/metadata/icall-def.h#L262):
```c
ICALL_TYPE(ENV, "System.Environment", ENV_1)
ICALL(ENV_15, "get_TickCount", ves_icall_System_Environment_get_TickCount)
```

On Mono, the method with implementation for `get_TickCount` is called `ves_icall_System_Environment_get_TickCount`.
We can find it in `icall.c`.

[`icall.c`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/metadata/icall.c#L7013):
```c
ICALL_EXPORT
gint32
ves_icall_System_Environment_get_TickCount (void)
{
    /* this will overflow after ~24 days */
    return (gint32) (mono_msec_boottime () & 0xffffffff);
}
```

Everything is simple: `ves_icall_System_Environment_get_TickCount` calls `mono_msec_boottime`.
Let's go to `mono-time.c` and look at the implementation.
As usual, we have different implementation for Windows and non-Windows OS.
Here is the Windows implementation:

[`mono-time.c`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/utils/mono-time.c#L54):
```c
#ifdef HOST_WIN32
// ...
gint64
mono_msec_boottime (void)
{
    /* GetTickCount () is reportedly monotonic */
    return GetTickCount64 ();
}
```

On Windows, `mono_msec_boottime` calls `GetTickCount64` (WinAPI function).
In the same file, we can find the non-Windows implementation.
Until Mono 5.8 the implementation was pretty simple:

[`mono-time.c`](https://github.com/mono/mono/blob/mono-5.8.0.129/mono/utils/mono-time.c#L128) (Mono 5.8.0.129):
```c
// Non-windows platforms
#else
// ...
/* Returns the number of milliseconds from boot time:
   this should be monotonic */
gint64
mono_msec_boottime (void)
{
    static gint64 boot_time = 0;
    gint64 now;
    if (!boot_time)
        boot_time = get_boot_time ();
    now = mono_100ns_datetime ();
    return (now - boot_time)/10000;
}
```

It looks much easier than on .NET Core:
  we just take `mono_100ns_datetime` (we covered this method in the “DateTime” section)
  and subtract `get_boot_time`.
The result of subtraction should be divided into 10000 because
  the time unit of `mono_100ns_datetime` is `100 ns`
  and we have to return a number of milliseconds.
In Mono 5.10, the .NET Core implementation was [adopted](https://github.com/mono/mono/commit/c6b39a0614244815b18748c0ca53bfc892df96a0)
  (it solved some [issues](https://bugzilla.xamarin.com/show_bug.cgi?id=58413) on Android).
After that, some minor changes (e.g.,
  [mono/c7a91c01](https://github.com/mono/mono/commit/c7a91c01fa32a7267df7512d847978b370907b7c) and
  [mono/dd6e1193](https://github.com/mono/mono/commit/dd6e11936be663b86e916499663541684fabe81e))
  were added in order to support different Apple distributions
  (`clock_gettime` is available only since iOS 10, macOS 10.12, tvOS 10 and watchOS 3).

