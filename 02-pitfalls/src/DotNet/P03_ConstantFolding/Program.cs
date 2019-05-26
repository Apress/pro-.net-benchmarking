using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace P03_ConstantFolding
{
    public class Benchmarks
    {
        [Benchmark]
        public double BadSqrt13()
        {
            return
                Math.Sqrt(1) + Math.Sqrt(2) + Math.Sqrt(3) + Math.Sqrt(4) + 
                Math.Sqrt(5) + Math.Sqrt(6) + Math.Sqrt(7) + Math.Sqrt(8) + 
                Math.Sqrt(9) + Math.Sqrt(10) + Math.Sqrt(11) + Math.Sqrt(12) + 
                Math.Sqrt(13);
        }
        
        [Benchmark]
        public double BadSqrt14()
        {
            return
                Math.Sqrt(1) + Math.Sqrt(2) + Math.Sqrt(3) + Math.Sqrt(4) + 
                Math.Sqrt(5) + Math.Sqrt(6) + Math.Sqrt(7) + Math.Sqrt(8) + 
                Math.Sqrt(9) + Math.Sqrt(10) + Math.Sqrt(11) + Math.Sqrt(12) + 
                Math.Sqrt(13) + Math.Sqrt(14);
        }

        public double x1 = 1, x2 = 2, x3 = 3, x4 = 4, x5 = 5, x6 = 6, 
            x7 = 7, x8 = 8, x9 = 9, x10 = 10, x11 = 11, 
            x12 = 12, x13 = 13, x14 = 14;
        
        [Benchmark]
        public double BetterSqrt14()
        {
            return
                Math.Sqrt(x1) + Math.Sqrt(x2) + Math.Sqrt(x3) + Math.Sqrt(x4) + 
                Math.Sqrt(x5) + Math.Sqrt(x6) + Math.Sqrt(x7) + Math.Sqrt(x8) + 
                Math.Sqrt(x9) + Math.Sqrt(x10) + Math.Sqrt(x11) + Math.Sqrt(x12) + 
                Math.Sqrt(x13) + Math.Sqrt(x14);
        }

    }
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}