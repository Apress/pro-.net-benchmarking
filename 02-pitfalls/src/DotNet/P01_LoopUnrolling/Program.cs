using System;
using System.Diagnostics;

namespace P01_LoopUnrolling
{
    // Should be executed on Windows because we need LegacyJIT-x64
    class Program
    {
        static void Bad()
        {
            var stopwatch1 = Stopwatch.StartNew();
            for (int i = 0; i < 1000000001; i++) ;
            stopwatch1.Stop();
            var stopwatch2 = Stopwatch.StartNew();
            for (int i = 0; i < 1000000002; i++) ;
            stopwatch2.Stop();
            
            Console.WriteLine(stopwatch1.ElapsedMilliseconds + " vs. " +
                              stopwatch2.ElapsedMilliseconds);
        }

        private static int N1 = 1000000001, N2 = 1000000002;
        
        static void Better()
        {
            var stopwatch1 = Stopwatch.StartNew();
            for (int i = 0; i < N1; i++) ;
            stopwatch1.Stop();
            var stopwatch2 = Stopwatch.StartNew();
            for (int i = 0; i < N2; i++) ;
            stopwatch2.Stop();
            
            Console.WriteLine(stopwatch1.ElapsedMilliseconds + " vs. " +
                              stopwatch2.ElapsedMilliseconds);
        }
        
        static void Main()
        {
            Console.WriteLine(Environment.Is64BitProcess ? "x64" : "x86");
            Console.WriteLine("*** Bad benchmark ***");
            Bad();
            Console.WriteLine("*** Better benchmark ***");
            Better();
        }
    }
}