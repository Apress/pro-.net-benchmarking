using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Cache3
{
    public class Benchmarks
    {
        private int[,] a;

        [Params(1023, 1024, 1025)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            a = new int[N, N];
        }

        [Benchmark]
        public int Max()
        {
            int max = int.MinValue;
            for (int i = 0; i < N; i++)
                max = Math.Max(max, a[i, 0]);
            return max;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}