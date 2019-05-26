using System.Collections.Generic;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace TimerBenchmarks
{
    public struct ResolutionInfo
    {
        public uint Min;
        public uint Max;
        public uint Current;
    }

    public static class WinApi
    {
        [DllImport("winmm.dll",
            EntryPoint = "timeBeginPeriod",
            SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll",
            EntryPoint = "timeEndPeriod",
            SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern uint NtQueryTimerResolution
        (out uint min,
            out uint max,
            out uint current);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern uint NtSetTimerResolution
        (uint desiredResolution,
            bool setResolution,
            ref uint currentResolution);

        public static ResolutionInfo QueryTimerResolution()
        {
            var info = new ResolutionInfo();
            NtQueryTimerResolution(out info.Min,
                out info.Max,
                out info.Current);
            return info;
        }

        public static ulong SetTimerResolution(uint ticks)
        {
            uint currentRes = 0;
            NtSetTimerResolution(ticks, true, ref currentRes);
            return currentRes;
        }
    }
    
    public class WindowsConfig : ConfigBase
    {
        public WindowsConfig()
        {
            Add(Job.Clr);
        }
    }

    [Config(typeof(WindowsConfig))]
    public class WindowsBenchmarks : Benchmarks
    {
        [ParamsSource(nameof(Resolutions))]
        public uint SystemTimerResolution { get; set; }

        public IEnumerable<uint> Resolutions
        {
            get
            {
                var info = WinApi.QueryTimerResolution();
                yield return info.Min;
                yield return info.Max;
            }
        }

        [GlobalSetup]
        public void GlobalSetup() => WinApi.SetTimerResolution(SystemTimerResolution);
    }
}