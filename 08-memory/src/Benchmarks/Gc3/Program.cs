using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Gc3
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark]
        public byte[] Allocate84900()
        {
            return new byte[84900];
        }
        
        [Benchmark]
        public byte[] Allocate85000()
        {
            return new byte[85000];
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}