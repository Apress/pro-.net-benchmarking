using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Cache4
{
    public class Benchmarks
    {
        private static int[] x = new int[1024];

        private void Inc(int p)
        {
            for (int i = 0; i < 1000001; i++)
            {
                x[p]++; x[p]++; x[p]++; x[p]++;
                x[p]++; x[p]++; x[p]++; x[p]++;
                x[p]++; x[p]++; x[p]++; x[p]++;
                x[p]++; x[p]++; x[p]++; x[p]++;
            }
        }

        [Params(1, 256)]
        public int Step;

        [Benchmark]
        public void Run()
        {
            Task.WaitAll(
                Task.Factory.StartNew(() => Inc(0 * Step)),
                Task.Factory.StartNew(() => Inc(1 * Step)),
                Task.Factory.StartNew(() => Inc(2 * Step)),
                Task.Factory.StartNew(() => Inc(3 * Step)));
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}