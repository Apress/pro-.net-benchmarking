using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Intrinsics2
{
    [LegacyJitX86Job, RyuJitX64Job]
    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        public static ulong RotateRight64(ulong value, int count)
        {
            return (value >> count) | (value << (64 - count));
        }

        private ulong a = 100;
        private int b = 2;

        [Benchmark]
        public ulong Foo()
        {
            return RotateRight64(a, b);
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}