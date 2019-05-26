using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp19
{
    class Program
    {
        static List<long> m = new List<long>();
        static byte[] data = new byte[64 * 1024 * 1024];

        static long Measure()
        {
            var stopwatch = Stopwatch.StartNew();
            var fileName = Path.GetTempFileName();
            File.WriteAllBytes(fileName, data);
            File.Delete(fileName);
            stopwatch.Stop();
            m.Add(stopwatch.ElapsedMilliseconds);
            return stopwatch.ElapsedMilliseconds;
        }

        static void Main()
        {
            int iteration = 0;
            while (true)
            {
                m.Clear();
                Console.WriteLine("Iteration: " + ++iteration);

                Console.Write("A: ");
                for (int i = 0; i < 5; i++)
                    Console.Write(Measure() + " ms ");
                Console.WriteLine();
                Console.Write("B: ");
                for (int i = 0; i < 5; i++)
                    Console.Write(Measure() + " ms ");
                Console.WriteLine();

                if (m[0] < m[5] && m[1] < m[6] && m[2] < m[7] && m[3] < m[8] && m[4] < m[9])
                {
                    Console.WriteLine("Hooray!");
                    break;
                }
            }
        }
    }
}