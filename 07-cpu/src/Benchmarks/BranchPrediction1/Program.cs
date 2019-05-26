using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BranchPrediction1
{
    public class Benchmarks
    {
        private const int n = 100000;
        private byte[] sorted = new byte[n];
        private byte[] unsorted = new byte[n];

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(42);
            for (int i = 0; i < n; i++)
                sorted[i] = unsorted[i] = (byte) random.Next(256);
            Array.Sort(sorted);
        }

        [Benchmark(Baseline = true)]
        public int SortedBranch()
        {
            int counter = 0;
            for (int i = 0; i < sorted.Length; i++)
                if (sorted[i] >= 128)
                    counter++;
            return counter;
        }

        [Benchmark]
        public int UnsortedBranch()
        {
            int counter = 0;
            for (int i = 0; i < unsorted.Length; i++)
                if (unsorted[i] >= 128)
                    counter++;
            return counter;
        }

        [Benchmark]
        public int SortedBranchless()
        {
            int counter = 0;
            for (int i = 0; i < sorted.Length; i++)
                counter += sorted[i] >> 7;
            return counter;
        }

        [Benchmark]
        public int UnsortedBranchless()
        {
            int counter = 0;
            for (int i = 0; i < unsorted.Length; i++)
                counter += unsorted[i] >> 7;
            return counter;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}