using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

namespace Inlining3
{
    [LegacyJitX86Job]
    [InliningDiagnoser]
    [DisassemblyDiagnoser(printSource: true, recursiveDepth: 2)]
    public class Benchmarks
    {
        private double x1, x2;

        [Benchmark(Baseline = true)]
        public double Foo()
        {
            return Calc(true);
        }

        public double Calc(bool dry)
        {
            double res = 0;
            double sqrt1 = Math.Sqrt(x1);
            double sqrt2 = Math.Sqrt(x2);
            if (!dry)
            {
                res += sqrt1;
                res += sqrt2;
            }

            return res;
        }

        [Benchmark]
        public double Bar()
        {
            return CalcAggressive(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalcAggressive(bool dry)
        {
            double res = 0;
            double sqrt1 = Math.Sqrt(x1);
            double sqrt2 = Math.Sqrt(x2);
            if (!dry)
            {
                res += sqrt1;
                res += sqrt2;
            }
            return res;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}