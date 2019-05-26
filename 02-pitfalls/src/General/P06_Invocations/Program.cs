using System;
using System.Diagnostics;

namespace P06_Iterations
{
    class Program
    {
        static void Bad()
        {
            const int N = 100000;
            for (int iter = 0; iter < 10; iter++)
            {
                var stopwatch = Stopwatch.StartNew();
                int counter = 0;
                for (int i = 1; i <= N; i++)
                    if (N % i == 0)
                        counter++;
                stopwatch.Stop();                
                var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
                Console.WriteLine(elapsedMs + " ms");
            }
        }

        static void Better()
        {
            const int N = 100000;
            const int Invocations = 3000;
            for (int iter = 0; iter < 10; iter++)
            {
                var stopwatch = Stopwatch.StartNew();
                for (int rep = 0; rep < Invocations; rep++)
                {
                    int counter = 0;
                    for (int i = 1; i <= N; i++)
                        if (N % i == 0)
                            counter++;
                }
                stopwatch.Stop();
                var elapsedMs = stopwatch.Elapsed.TotalMilliseconds / Invocations;
                Console.WriteLine(elapsedMs + " ms");
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