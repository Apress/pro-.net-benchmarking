using System;
using System.Diagnostics;

namespace P04_BoundCheckElimination
{
    // Should be executed on Windows
    class Program
    {
        static void Bad()
        {
            const int N = 1000001;
            int[] a = new int[N];

            var stopwatch1 = Stopwatch.StartNew();
            for (int iteration = 0; iteration < 101; iteration++)
            for (int i = 0; i < N; i++)
                a[i]++;
            stopwatch1.Stop();

            var stopwatch2 = Stopwatch.StartNew();
            for (int iteration = 0; iteration < 101; iteration++)
            for (int i = 0; i < a.Length; i++)
                a[i]--;
            stopwatch2.Stop();

            Console.WriteLine(stopwatch1.ElapsedMilliseconds + " vs. " +
                              stopwatch2.ElapsedMilliseconds);
        }

        static void Better()
        {
            const int N = 1000001;
            int[] a = new int[N];
            var stopwatch1 = Stopwatch.StartNew();
            for (int iteration = 0; iteration < 101; iteration++)
            for (int i = 0; i < N; i++)
                a[i]++;
            stopwatch1.Stop();
            var stopwatch2 = Stopwatch.StartNew();
            for (int iteration = 0; iteration < 101; iteration++)
            for (int i = 0; i < N; i++)
                a[i]--;
            stopwatch2.Stop();
            Console.WriteLine(stopwatch1.ElapsedMilliseconds + " vs. " +
                              stopwatch2.ElapsedMilliseconds);
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