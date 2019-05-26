using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Inlining1
{
    public class Benchmarks
    {
        private const int N = 1000;
        private int[] x = new int[N];
        private int[] y = new int[N];
        private int[] z = new int[N];

        [Benchmark(Baseline = true)]
        public void Foo()
        {
            for (int i = 0; i < z.Length; i++)
                z[i] = Sum(x[i], y[i]);
        }

        [Benchmark]
        public void Bar()
        {
            for (int i = 0; i < z.Length; i++)
                z[i] = VirtualSum(x[i], y[i]);
        }

        public int Sum(int a, int b) => a + b;

        public virtual int VirtualSum(int a, int b) => a + b;
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}