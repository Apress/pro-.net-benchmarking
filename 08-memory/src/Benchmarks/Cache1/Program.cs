using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Cache1
{
    public class Benchmarks
    {
        private int n = 512;
        private long[,] a;

        [GlobalSetup]
        public void Setup()
        {
            a = new long[n, n];
        }

        [Benchmark(Baseline = true)]
        public long SumIj()
        {
            long sum = 0;
            for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                sum += a[i, j];
            return sum;
        }

        [Benchmark]
        public long SumJi()
        {
            long sum = 0;
            for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                sum += a[j, i];
            return sum;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}