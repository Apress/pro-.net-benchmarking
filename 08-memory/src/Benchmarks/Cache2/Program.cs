using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

namespace Cache2
{
    [HardwareCounters(HardwareCounter.CacheMisses)]
    public class Benchmarks
    {
        private const int N = 16 * 1024 * 1024;
        private byte[] data;

        [Params(1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768)]
        public int SizeKb;

        [GlobalSetup]
        public void Setup()
        {
            data = new byte[SizeKb * 1024];
        }

        [Benchmark]
        public void Calc()
        {
            int mask = data.Length - 1;
            for (int i = 0; i < N; i++)
                data[(i * 64) & mask]++;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}