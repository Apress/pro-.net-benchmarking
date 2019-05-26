using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace InstructionLevelParallelism3
{
    public class Benchmarks
    {
        private double[] a = new double[100];

        [Benchmark]
        public double Loop()
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
                sum += a[i];
            return sum;
        }

        [Benchmark]
        public double UnrolledLoop()
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i += 4)
                sum += a[i] + a[i + 1] + a[i + 2] + a[i + 3];
            return sum;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}