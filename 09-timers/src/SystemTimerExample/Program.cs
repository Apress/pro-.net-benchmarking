using System;
using System.Runtime.InteropServices;

namespace SystemTimerExample
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
    
    // Windows-only
    class Program
    {
        static void Main()
        {
            var resolutionInfo = WinApi.QueryTimerResolution();
            Console.WriteLine($"Min     = {resolutionInfo.Min}");
            Console.WriteLine($"Max     = {resolutionInfo.Max}");
            Console.WriteLine($"Current = {resolutionInfo.Current}");
        }
    }
}