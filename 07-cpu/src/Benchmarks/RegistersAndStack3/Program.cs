using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RegistersAndStack3
{
    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        private const int N = 93;

        [Benchmark(Baseline = true)]
        public long Fibonacci1()
        {
            long a = 0, b = 0, c = 1;
            for (int i = 1; i < N; i++)
            {
                a = b;
                b = c;
                c = a + b;
            }
            return c;
        }

        [Benchmark]
        public long Fibonacci2()
        {
            long a = 0, b = 0, c = 1;
            try
            {
                for (int i = 1; i < N; i++)
                {
                    a = b;
                    b = c;
                    c = a + b;
                }
            }
            catch {}
            return c;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}