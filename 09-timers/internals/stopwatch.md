# Stopwatch Internals

In this section, we will look at the source code of Stopwatch
  on .NET Framework, .NET Core, and Mono.

## Source code of Stopwatch in .NET Framework

In .NET Framework, `Stopwatch` simply uses `QueryPerformanceFrequency`/`QueryPerformanceCounter` API
  (.NET Framework 4.7.2 version is shown):

[`Stopwatch.cs`](http://referencesource.microsoft.com/#System/services/monitoring/system/diagnosticts/Stopwatch.cs):
```cs
public class Stopwatch
{
  private const long TicksPerMillisecond = 10000;
  private const long TicksPerSecond = TicksPerMillisecond * 1000;

  // "Frequency" stores the frequency of the high-resolution performance counter,
  // if one exists. Otherwise it will store TicksPerSecond.
  // The frequency cannot change while the system is running,
  // so we only need to initialize it once.
  public static readonly long Frequency;
  public static readonly bool IsHighResolution;

  // performance-counter frequency, in counts per ticks.
  // This can speed up conversion from high frequency performance-counter
  // to ticks.
  private static readonly double tickFrequency;

  static Stopwatch() {
    bool succeeded = SafeNativeMethods.QueryPerformanceFrequency(out Frequency);
    if(!succeeded) {
      IsHighResolution = false;
      Frequency = TicksPerSecond;
      tickFrequency = 1;
    }
    else {
      IsHighResolution = true;
      tickFrequency = TicksPerSecond;
      tickFrequency /= Frequency;
    }
  }

  public static long GetTimestamp() {
    if(IsHighResolution) {
      long timestamp = 0;
      SafeNativeMethods.QueryPerformanceCounter(out timestamp);
      return timestamp;
    }
    else {
      return DateTime.UtcNow.Ticks;
    }
  }
}
```

The `QueryPerformanceCounter` and `QueryPerformanceFrequency` methods are defined in the `SafeNativeMethods.cs`.

[`SafeNativeMethods.cs`](http://referencesource.microsoft.com/#System/compmod/microsoft/win32/SafeNativeMethods.cs,122):
```cs
[DllImport(ExternDll.Kernel32)]
[ResourceExposure(ResourceScope.None)]
public static extern bool QueryPerformanceCounter(out long value);

[DllImport(ExternDll.Kernel32)]
[ResourceExposure(ResourceScope.None)]
public static extern bool QueryPerformanceFrequency(out long value);
```

As you can see, here we have usual `extern` declarations which import native functions from `kernel32.dll`.
.NET Framework works only on Windows, so we can rely on the WinAPI.

What do you think, is it possible to get a negative elapsed time interval using Stopwatch?
In fact, it's possible on old versions of .NET Framework.
Stopwatch uses hardware timers, so we can't be sure that
  a difference between two sequential timestamps are also non-negative.
The bug was fixed in .NET 4.0
  (originally with `#if NET_4_0`..`#endif`;
  these directives were dropped later).
The fix is very simple: if the elapsed time is negative,
  we replace it with zero.
You can still find a comment with an explanation in the source code of the `Stop` method:

`Stopwatch.cs`:
```cs
if (_elapsed < 0)
{
  // When measuring small time periods the StopWatch.Elapsed*
  // properties can return negative values.  This is due to 
  // bugs in the basic input/output system (BIOS) or the hardware
  // abstraction layer (HAL) on machines with variable-speed CPUs
  // (e.g. Intel SpeedStep).
  _elapsed = 0;
}
```

## Source code of Stopwatch in .NET Core

In this subsection, we will look at the source code of Stopwatch on .NET Core 2.1 for Windows and Unix.
Basically, .NET Core contains almost the same
  [implementation](https://github.com/dotnet/corefx/blob/v2.1.0/src/System.Runtime.Extensions/src/System/Diagnostics/Stopwatch.cs)
  as in a case of .NET Framework except for some minor changes in private variable names,
  `SecuritySafeCritical` attribute usages, and `FEATURE_NETCORE` depended on code.

The main difference is the following:
  .NET Core is a cross platform runtime,
  so we don't which timestamping API do we have on the OS level.
Thus, we have different declarations of `QPC` and `QPF` methods
  depend on the target platform (instead of using fixed `SafeNativeMethods.cs` implementation).

Let's learn how it's implemented for different operating systems in detail.
We can find the Windows part of the implementation in the `Stopwatch.Windows.cs` (the `corefx` repository):

[`Stopwatch.Windows.cs`](https://github.com/dotnet/corefx/blob/v2.1.0/src/System.Runtime.Extensions/src/System/Diagnostics/Stopwatch.Windows.cs):
```cs
public partial class Stopwatch
{
  private static bool QueryPerformanceFrequency(out long value)
  {
    return Interop.Kernel32.QueryPerformanceFrequency(out value);
  }

  private static bool QueryPerformanceCounter(out long value)
  {
    return Interop.Kernel32.QueryPerformanceCounter(out value);
  }
}
```

The `QPF` and `QPC` methods of the `Stopwatch` class just call the same methods from `Interop.Kernel32`.
This is a very simple class:

[`Interop.QueryPerformanceCounter.cs`](https://github.com/dotnet/corefx/blob/v2.1.0/src/Common/src/Interop/Windows/kernel32/Interop.QueryPerformanceCounter.cs):
```cs
internal partial class Interop
{
  internal partial class Kernel32
  {
    [DllImport(Libraries.Kernel32)]
    internal static extern bool QueryPerformanceCounter(out long value);
  }
}
```

[`Interop.QueryPerformanceFrequency.cs`](https://github.com/dotnet/corefx/blob/v2.1.0/src/Common/src/Interop/Windows/kernel32/Interop.QueryPerformanceFrequency.cs):
```cs
internal partial class Interop
{
  internal partial class Kernel32
  {
    [DllImport(Libraries.Kernel32)]
    internal static extern bool QueryPerformanceFrequency(out long value);
  }
}
```

In `Interop.Kernel32` we just call the native `QPC`/`QPF` which provided by Windows.
Thus, we have exactly the same behavior as in the case of .NET Framework.
If we are looking for the implementation of `Stopwatch` on Unix, we have to go to `Stopwatch.Unix.cs`:

[`Stopwatch.Unix.cs`](https://github.com/dotnet/corefx/blob/v2.1.0/src/System.Runtime.Extensions/src/System/Diagnostics/Stopwatch.Unix.cs):
```cs
public partial class Stopwatch
{
  private static bool QueryPerformanceFrequency(out long value)
  {
    return Interop.Sys.GetTimestampResolution(out value);
  }

  private static bool QueryPerformanceCounter(out long value)
  {
    return Interop.Sys.GetTimestamp(out value);
  }
}
```

As you can see, we call other methods here:
  `QueryPerformanceFrequency` calls `Interop.Sys.GetTimestampResolution` and
  `QueryPerformanceCounter` calls `Interop.Sys.GetTimestamp`.
Ok, let's check the source code of `Interop.Sys`.

[`Interop.GetTimestamp.cs`](https://github.com/dotnet/corefx/blob/v2.1.0/src/Common/src/Interop/Unix/System.Native/Interop.GetTimestamp.cs):
```cs
internal static partial class Interop
{
  internal static partial class Sys
  {
    [DllImport(Libraries.SystemNative,
               EntryPoint = "SystemNative_GetTimestampResolution")]
    internal static extern bool GetTimestampResolution(out long resolution);

    [DllImport(Libraries.SystemNative,
               EntryPoint = "SystemNative_GetTimestamp")]
    internal static extern bool GetTimestamp(out long timestamp);
  }
}
```

The `GetTimestampResolution` and `GetTimestamp` methods are imported
  from a native library (where they are implemented in C++).
Here we need `pal_time.cpp`.
This file contains many interesting time-specific source code,
  but we need only a small part of it for now.
Let's start with the implementation of `GetTimestampResolution` 

[`pal_time.cpp`](https://github.com/dotnet/corefx/blob/v2.1.0/src/Native/Unix/System.Native/pal_time.cpp):
```cpp
extern "C" int32_t SystemNative_GetTimestampResolution(uint64_t* resolution)
{
  assert(resolution);

#if HAVE_CLOCK_MONOTONIC
  // Make sure we can call clock_gettime with MONOTONIC. Stopwatch invokes
  // GetTimestampResolution as the very first thing, and by calling this here
  // to verify we can successfully, we don't have to branch in GetTimestamp.
  struct timespec ts;
  if (clock_gettime(CLOCK_MONOTONIC, &ts) == 0) 
  {
    *resolution = SecondsToNanoSeconds;
    return 1;
  }
  else
  {
    *resolution = 0;
    return 0;
  }

#elif HAVE_MACH_ABSOLUTE_TIME
  mach_timebase_info_data_t mtid;
  if (mach_timebase_info(&mtid) == KERN_SUCCESS)
  {
    *resolution = SecondsToNanoSeconds * (static_cast<uint64_t>(mtid.denom) / 
                  static_cast<uint64_t>(mtid.numer));
    return 1;
  }
  else
  {
    *resolution = 0;
    return 0;
  }

#else /* gettimeofday */
  *resolution = SecondsToMicroSeconds;
  return 1;

#endif
}
```

As you can see, we use 3 different APIs depends on which one is available:

* if `HAVE_CLOCK_MONOTONIC`, we use `clock_gettime` (and the nominal resolution is `1 ns`),
* otherwise if `HAVE_MACH_ABSOLUTE_TIME`, we use `mach_timebase_info`,
* otherwise we use `gettimeofday`.

Now let's look at the `GetTimestamp` implementation at the same file.

[`pal_time.cpp`](https://github.com/dotnet/corefx/blob/v2.1.0/src/Native/Unix/System.Native/pal_time.cpp#L81):
```cpp
extern "C" int32_t SystemNative_GetTimestamp(uint64_t* timestamp)
{
  assert(timestamp);

#if HAVE_CLOCK_MONOTONIC
  struct timespec ts;
  int result = clock_gettime(CLOCK_MONOTONIC, &ts);

  // only possible errors are if MONOTONIC
  // isn't supported or ts is an invalid address
  assert(result == 0);

  (void)result; // suppress unused parameter warning in release builds
  *timestamp = (static_cast<uint64_t>(ts.tv_sec) * SecondsToNanoSeconds) + 
                static_cast<uint64_t>(ts.tv_nsec);
  return 1;

#elif HAVE_MACH_ABSOLUTE_TIME
  *timestamp = mach_absolute_time();
  return 1;

#else
  struct timeval tv;
  if (gettimeofday(&tv, NULL) == 0)
  {
    *timestamp = (static_cast<uint64_t>(tv.tv_sec) * SecondsToMicroSeconds) + 
                  static_cast<uint64_t>(tv.tv_usec);
    return 1;
  }
  else
  {
    *timestamp = 0;
    return 0;
  }

#endif
}
```

We have the same 3 cases for the `SystemNative_GetTimestamp` function which returns the counter value.

## Source code of Stopwatch in Mono

Let's look at an implementation of `Stopwatch` in Mono (we work with Mono 5.12.0.226 here).
The managed implementation is very simple:

[`Stopwatch.cs`](https://github.com/mono/mono/blob/mono-5.12.0.226/mcs/class/System/System.Diagnostics/Stopwatch.cs#L39):
```cs
public class Stopwatch
{
    [MethodImplAttribute(MethodImplOptions.InternalCall)]
    public static extern long GetTimestamp();

    public static readonly long Frequency = 10000000;

    public static readonly bool IsHighResolution = true;
}
```

As you can see, the frequency of `Stopwatch` in Mono is a const (`10'000'000`).
For some historical reasons, this `Stopwatch` uses the same tick value (`100 ns`) as the `DateTime`.
It's important to understand that
  this value is not related to the actual timer frequency,
  it just specifies the value of a single tick which is provided by `GetTimestamp()`.

Now let's look at the internal implementation.
As usual, we can find details about extern functions in `icall-def.h`.

[`icall-def.h`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/metadata/icall-def.h#L250):
```cpp
ICALL_TYPE(STOPWATCH, "System.Diagnostics.Stopwatch", STOPWATCH_1)
ICALL(STOPWATCH_1, "GetTimestamp", mono_100ns_ticks)
```

Here we declare that we should use `mono_100ns_ticks` as the implementation of `Stopwatch.GetTimestamp`.
The source code of `mono_100ns_ticks` can be found in `mono-time.c`.
  (this is the main place for all time-related functions in mono).
On Windows, the implementation is simple:

[`mono-time.c`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/utils/mono-time.c#L61):
```cpp
#ifdef HOST_WIN32
// ...
/* Returns the number of 100ns ticks from unspecified time:
   this should be monotonic */
gint64
mono_100ns_ticks (void)
{
    static LARGE_INTEGER freq;
    static UINT64 start_time;
    UINT64 cur_time;
    LARGE_INTEGER value;

    if (!freq.QuadPart) {
        if (!QueryPerformanceFrequency (&freq))
            return mono_100ns_datetime ();
        QueryPerformanceCounter (&value);
        start_time = value.QuadPart;
    }
    QueryPerformanceCounter (&value);
    cur_time = value.QuadPart;
    /* we use unsigned numbers and return the difference to avoid overflows */
    return (cur_time - start_time) * (double)MTICKS_PER_SEC / freq.QuadPart;
}
```

Here we are trying to call `QPC` from WinAPI.
If this approach is not available, we have a fallback to `mono_100ns_datetime` (the same function is used in DateTime).

There is also another implementation for Unix:

[`mono-time.c`](https://github.com/mono/mono/blob/mono-5.12.0.226/mono/utils/mono-time.c#L208):
```cpp
// Non-windows case
#else
// ...

/* Returns the number of 100ns ticks from unspecified time:
   this should be monotonic */
gint64
mono_100ns_ticks (void)
{
  struct timeval tv;
#if defined(HOST_DARWIN)
  /* http://developer.apple.com/library/mac/#qa/qa1398/_index.html */
  static mach_timebase_info_data_t timebase;
  guint64 now = mach_absolute_time ();
  if (timebase.denom == 0) {
    mach_timebase_info (&timebase);
    timebase.denom *= 100; /* we return 100ns ticks */
  }
  return now * timebase.numer / timebase.denom;
#elif defined(CLOCK_MONOTONIC)
  struct timespec tspec;
  static struct timespec tspec_freq = {0};
  static int can_use_clock = 0;
  if (!tspec_freq.tv_nsec) {
    can_use_clock = clock_getres (CLOCK_MONOTONIC, &tspec_freq) == 0;
  }
  if (can_use_clock) {
    if (clock_gettime (CLOCK_MONOTONIC, &tspec) == 0) {
      return ((gint64)tspec.tv_sec * MTICKS_PER_SEC + tspec.tv_nsec / 100);
    }
  }
#endif
  if (gettimeofday (&tv, NULL) == 0)
    return ((gint64)tv.tv_sec * 1000000 + tv.tv_usec) * 10;
  return 0;
}
```

As you can see, the implementation is similar to .NET Core:
  it uses `mach_absolute_time`/`clock_gettime` as a primary way and `gettimeofday` as a fallback case.
The order of `mach_absolute_time` and `clock_gettime` was changed in mono 4.8 in order to fix the problem with macOS 10.12
  (see https://github.com/mono/mono/pull/3165).