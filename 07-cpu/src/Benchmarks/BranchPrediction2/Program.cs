using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BranchPrediction2
{
    public class Benchmarks
    {
        private const int n = 100000;
        private int[] a = new int[n];
        private int[] b = new int[n];
        private int[] c = new int[n];

        [Params(false, true)]
        public bool RandomData;

        [GlobalSetup]
        public void Setup()
        {
            if (RandomData)
            {
                var random = new Random(42);
                for (int i = 0; i < n; i++)
                {
                    a[i] = random.Next(2);
                    b[i] = random.Next(2);
                    c[i] = random.Next(2);
                }
            }
        }

        [Benchmark(Baseline = true)]
        public int OneCondition()
        {
            int counter = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] * b[i] * c[i] != 0)
                    counter++;
            return counter;
        }

        [Benchmark]
        public int TwoConditions()
        {
            int counter = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] * b[i] != 0 && c[i] != 0)
                    counter++;
            return counter;
        }

        [Benchmark]
        public int ThreeConditions()
        {
            int counter = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != 0 && b[i] != 0 && c[i] != 0)
                    counter++;
            return counter;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}