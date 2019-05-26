using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

namespace Inlining2
{
    [InliningDiagnoser]
    [DisassemblyDiagnoser(printSource: true, recursiveDepth: 2)]
    [LegacyJitX86Job]
    public class Benchmarks
    {
        private const int n = 100;
        private bool flag = false;

        [Benchmark(Baseline = true)]
        public int Foo()
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (flag)
                    sum += InlinedLoop();
                sum += i * 3 + i * 4;
            }
            return sum;
        }

        [Benchmark]
        public int Bar()
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (flag)
                    sum += NotInlinedLoop();
                sum += i * 3 + i * 4;
            }
            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int InlinedLoop()
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (i + 1) * (i + 2);
            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int NotInlinedLoop()
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (i + 1) * (i + 2);
            return sum;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}