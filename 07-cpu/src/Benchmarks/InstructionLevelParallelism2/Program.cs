using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace InstructionLevelParallelism2
{
    public class Benchmarks
    {
        private int n = 1000001;
        private double x0, x1, x2, x3, x4, x5, x6, x7;

        [Benchmark(Baseline = true)]
        public void WithoutHazards()
        {
            for (int i = 0; i < n; i++)
            {
                x0++; x1++; x2++; x3++;
                x4++; x5++; x6++; x7++;
            }
        }

        [Benchmark]
        public void WithHazards()
        {
            for (int i = 0; i < n; i++)
            {
                x0++; x0++; x0++; x0++;
                x0++; x0++; x0++; x0++;
            }
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}