using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace P04_TrickyDistributions
{
    class Program
    {
        static void Bad()
        {
            byte[] data = new byte[64 * 1024 * 1024];
            var stopwatch = Stopwatch.StartNew();
            var fileName = Path.GetTempFileName();
            File.WriteAllBytes(fileName, data);
            File.Delete(fileName);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void Better()
        {
            int N = 1000;
            byte[] data = new byte[64 * 1024 * 1024];
            var measurements = new long[N];
            for (int i = 0; i < N; i++)
            {
                var stopwatch = Stopwatch.StartNew();
                var fileName = Path.GetTempFileName();
                File.WriteAllBytes(fileName, data);
                File.Delete(fileName);
                stopwatch.Stop();
                measurements[i] = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(measurements[i]);
            }
            Console.WriteLine("Minimum = " + measurements.Min());
            Console.WriteLine("Maximum = " + measurements.Max());
            Console.WriteLine("Average = " + measurements.Average());
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