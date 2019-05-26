using System;
using System.Diagnostics;

namespace P02_Executing
{
    class Program
    {
        static void Main()
        {
            // Run this code in DEBUG and RELEASE modes
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 100000000; i++);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}