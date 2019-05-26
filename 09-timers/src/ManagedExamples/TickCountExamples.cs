using System;
using static System.Console;

namespace Examples
{
    public class TickCountExamples
    {
        public void MeasurementsExample()
        {
            int start = Environment.TickCount;
            // Logic
            int end = Environment.TickCount;
            int elapsedMilliseconds = end - start;
            WriteLine(elapsedMilliseconds);
        }
        
        public void RunWithHeader(string name, Action action)
        {
            WriteLine($"*** {name} ***");
            action();
            WriteLine("******************************");
            WriteLine();
        }

        public void RunAll()
        {
            RunWithHeader(nameof(MeasurementsExample), MeasurementsExample);
        }
    }
}