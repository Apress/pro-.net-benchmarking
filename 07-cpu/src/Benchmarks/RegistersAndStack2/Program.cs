using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RegistersAndStack2
{
    public struct Struct
    {
        public Struct(uint? someValue)
        {
            SomeValue = someValue;
        }

        public uint? SomeValue { get; }
    }

    [DisassemblyDiagnoser(printIL: true)]
    public class Benchmarks
    {
        [Benchmark(Baseline = true)]
        public uint? Foo()
        {
            return new Struct(100).SomeValue;
        }

        [Benchmark]
        public uint? Bar()
        {
            Struct s = new Struct(100);
            return s.SomeValue;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}