using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace P06_ConditionalJitting_Better
{
    // Should be executed on Windows (LegacyJIT-x86 is required)
    class Program
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static double Measure1()
        {
            double sum = 1, inc = 1;
            for (int i = 0; i < 1000000001; i++)
                sum = sum + inc;
            return sum;
        }
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static double Measure2()
        {
            double sum = 1, inc = 1;
            for (int i = 0; i < 1000000001; i++)
                sum = sum + inc;
            return sum;
        }
        
        public static void Main()
        {
            var stopwatch1 = Stopwatch.StartNew();
            Measure1();
            stopwatch1.Stop();
            var stopwatch2 = Stopwatch.StartNew();
            Measure2();
            stopwatch2.Stop();
            Console.WriteLine(stopwatch1.ElapsedMilliseconds + " vs. " + 
                              stopwatch2.ElapsedMilliseconds);
        }

    }
}