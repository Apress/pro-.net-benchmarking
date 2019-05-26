using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

namespace Inlining4
{
    [InliningDiagnoser]
    [DisassemblyDiagnoser(printIL: true, recursiveDepth: 2)]
    [LegacyJitX86Job]
    [LegacyJitX64Job]
    public class Benchmarks
    {
        [Benchmark]
        public int Calc()
        {
            return WithoutStarg(0x11) + WithStarg(0x12);
        }

        private static int WithoutStarg(int value)
        {
            return value;
        }

        private static int WithStarg(int value)
        {
            if (value < 0)
                value = -value;
            return value;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}