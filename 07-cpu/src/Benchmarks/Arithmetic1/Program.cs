using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Arithmetic1
{
    [LegacyJitX86Job, RyuJitX64Job]
    [IterationCount(50)]
    public class Benchmarks
    {
        [Params(100000, 1000000)]
        public int N;

        [Benchmark]
        public double PowerA()
        {
            double res = 1.0;
            for (int i = 0; i < N; i++)
                res = res * 0.96;
            return res;
        }

        private double resB;

        [Benchmark]
        public double PowerB()
        {
            resB = 1.0;
            for (int i = 0; i < N; i++)
                resB = resB * 0.96;
            return resB;
        }

        [Benchmark]
        public double PowerC()
        {
            double res = 1.0;
            for (int i = 0; i < N; i++)
                res = res * 0.96 + 0.1 - 0.1;
            return res;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}