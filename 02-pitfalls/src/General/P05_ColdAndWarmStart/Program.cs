using System;
using System.Diagnostics;

namespace P05_ColdAndWarmStart
{
    class Program
    {
        static void Bad()
        {
            int[] x = new int[100000000];
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < x.Length; i++)
                x[i]++;
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void Better()
        {
            int[] x = new int[100000000];
            for (int iter = 0; iter < 5; iter++)
            {
                var stopwatch = Stopwatch.StartNew();
                for (int i = 0; i < x.Length; i++)
                    x[i]++;
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
        }
        
        static void Main()
        {
            Console.WriteLine("*** Bad benchmark ***");
            Bad();
            Console.WriteLine("*** Better benchmark ***");
            Better();
        }
    }
}