using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace P08_UnequalIterations
{
    class Program
    {
        static void Measure(int n)
        {
            var list = new List<int>();
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < n; i++)
                list.Add(0);
            stopwatch.Stop();
            Console.Write("Capacity: " + list.Capacity + ", Time = ");
            Console.WriteLine("{0:0.00} ns", 
                stopwatch.ElapsedMilliseconds * 1000000.0 / n);
        }

        static void Bad()
        {
            Measure(16777216);
            Measure(16777217);
        }
        
        static void Main()
        {
            Bad();
        }
    }
}