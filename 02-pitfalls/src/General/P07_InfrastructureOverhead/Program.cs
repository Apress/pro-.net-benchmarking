using System;
using System.Diagnostics;

namespace P07_InfrastructureOverhead
{
    class Program
    {
        static void Bad()
        {
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 100000001; i++)
                Convert.ToInt32(0.0);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void Better()
        {
            var stopwatchOverhead = Stopwatch.StartNew();
            for (int i = 0; i < 100000001; i++)
            {
            }
            stopwatchOverhead.Stop();
            var stopwatchTarget = Stopwatch.StartNew();
            for (int i = 0; i < 100000001; i++)
                Convert.ToInt32(0.0);
            stopwatchTarget.Stop();
            var resultOverhead =
                stopwatchTarget.ElapsedMilliseconds -
                stopwatchOverhead.ElapsedMilliseconds;
            Console.WriteLine(resultOverhead);
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