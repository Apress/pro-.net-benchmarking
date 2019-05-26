using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Intrinsics1
{
    public static class MyMath 
    {
        public static double Round(double a)
        {
            if (a == (double)((long)a))
            {
                return a;
            }

            double flrTempVal = Math.Floor(a + 0.5);
            if ((a == (Math.Floor(a) + 0.5)) && (flrTempVal % 2.0 != 0))
            {
                flrTempVal -= 1.0;
            }

            return copysign(flrTempVal, a);
        }
        
        
        private static double copysign(double x, double y)
        {
            var xbits = BitConverter.DoubleToInt64Bits(x);
            var ybits = BitConverter.DoubleToInt64Bits(y);

            if (((xbits ^ ybits) >> 63) != 0)
            {
                return BitConverter.Int64BitsToDouble(xbits ^ long.MinValue);
            }

            return x;
        }
    }

    [DisassemblyDiagnoser(recursiveDepth: 3)]
    [LegacyJitX64Job, RyuJitX64Job]
    public class Benchmarks
    {
        private double doubleValue = 1.3;
        
        [Benchmark]
        public double SystemRound()
        {
            return Math.Round(doubleValue);
        }

        [Benchmark]
        public double MyRound()
        {
            return MyMath.Round(doubleValue);
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}