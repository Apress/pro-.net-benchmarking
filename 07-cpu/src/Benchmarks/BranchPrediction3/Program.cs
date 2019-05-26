using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BranchPrediction3
{
    [LegacyJitX86Job, LegacyJitX64Job, RyuJitX64Job, MonoJob]
    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        const int N = 100001;

        private int[] a = new int[N];
        private int[] b = new int[N];
        private int[] c = new int[N];

        [Params(false, true)]
        public bool RandomData;

        [GlobalSetup]
        public void Setup()
        {
            if (RandomData)
            {
                var random = new Random(42);
                for (int i = 0; i < N; i++)
                {
                    a[i] = random.Next();
                    b[i] = random.Next();
                }
            }
        }

        [Benchmark]
        public void Ternary()
        {
            for (int i = 0; i < N; i++)
            {
                int x = a[i], y = b[i];
                c[i] = x < y ? x : y;
            }
        }

        [Benchmark]
        public void BitHacks()
        {
            for (int i = 0; i < N; i++)
            {
                int x = a[i], y = b[i];
                c[i] = x & ((x - y) >> 31) | y & (~(x - y) >> 31);
            }
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}