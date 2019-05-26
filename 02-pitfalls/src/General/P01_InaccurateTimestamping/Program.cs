using System;
using System.Diagnostics;
using System.Linq;

namespace P01_InaccurateTimestamping
{
    class Program
    {
        static void Bad()
        {
            var list = Enumerable.Range(0, 10000).ToList();
            DateTime start = DateTime.Now;
            list.Sort();
            DateTime end = DateTime.Now;
            TimeSpan elapsedTime = end - start;
            Console.WriteLine(elapsedTime.TotalMilliseconds);
        }

        static void Better()
        {
            var list = Enumerable.Range(0, 10000).ToList();
            var stopwatch = Stopwatch.StartNew();
            list.Sort();
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine(elapsedTime.TotalMilliseconds);   
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