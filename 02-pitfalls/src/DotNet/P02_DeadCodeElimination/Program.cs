using System;
using System.Diagnostics;

namespace P02_DeadCodeElimination
{
    class Program
    {
        static void Bad()
        {
            double x = 0;

            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 100000001; i++)
                Math.Sqrt(x);
            stopwatch.Stop();

            var stopwatch2 = Stopwatch.StartNew();
            for (int i = 0; i < 100000001; i++);
            stopwatch2.Stop();

            var target = stopwatch.ElapsedMilliseconds;
            var overhead = stopwatch2.ElapsedMilliseconds;
            var result = target - overhead;
            Console.WriteLine("Target   = " + target   + "ms");
            Console.WriteLine("Overhead = " + overhead + "ms");
            Console.WriteLine("Result   = " + result   + "ms");
        }

        static void Better()
        {
            double x = 0, y = 0;

            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 100000001; i++)
                y += Math.Sqrt(x);
            stopwatch.Stop();
            Console.WriteLine(y);

            var stopwatch2 = Stopwatch.StartNew();
            for (int i = 0; i < 100000001; i++);
            stopwatch2.Stop();

            var target = stopwatch.ElapsedMilliseconds;
            var overhead = stopwatch2.ElapsedMilliseconds;
            var result = target - overhead;
            Console.WriteLine("Target   = " + target   + "ms");
            Console.WriteLine("Overhead = " + overhead + "ms");
            Console.WriteLine("Result   = " + result   + "ms");
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