using System;
using System.Diagnostics;

namespace P07_InterfaceMethodDispatching_Better2
{
    class Program
    {
        interface IIncrementer
        {
            int Inc(int x);
        }

        class Incrementer1 : IIncrementer
        {
            public int Inc(int x) => x + 1;
        }

        class Incrementer2 : IIncrementer
        {
            public int Inc(int x) => x + 1;
        }

        static void Measure(IIncrementer incrementer)
        {
            for (int i = 0; i < 100000001; i++)
                incrementer.Inc(0);
        }

        static void Main()
        {
            var stopwatch2 = Stopwatch.StartNew();
            Measure(new Incrementer2());
            stopwatch2.Stop();

            Console.WriteLine(stopwatch2.ElapsedMilliseconds);
        }
    }
}