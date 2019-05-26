using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;

namespace TimerBenchmarks
{
    public class Program
    {
        static void Main()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                BenchmarkRunner.Run<WindowsBenchmarks>();
            else
                BenchmarkRunner.Run<UnixBenchmarks>();
        }
    }
}