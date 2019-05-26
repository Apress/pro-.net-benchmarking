using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static System.Console;

namespace Examples
{
    public class StopwatchExamples
    {
        public void MeasurementsExample()
        {
            // Simple time measurement
            Stopwatch stopwatch = Stopwatch.StartNew();
            // <Measured logic>
            stopwatch.Stop();

            // Elapsed time in different measurement units
            TimeSpan elapsed = stopwatch.Elapsed;
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            long elapsedTicks = stopwatch.ElapsedTicks;
            double elapsedNanoseconds = stopwatch.ElapsedTicks * 1000000000.0 /
                                        Stopwatch.Frequency;
            WriteLine(elapsedMilliseconds + " ns");

            // Reusing existed stopwatch
            stopwatch.Restart();
            // Measured logic
            stopwatch.Stop();
        }

        public void MeasurementsExample2()
        {
            // Measurements without an instance of Stopwatch
            long timestamp1 = Stopwatch.GetTimestamp();
            long timestamp2 = Stopwatch.GetTimestamp();
            double elapsedSeconds = (timestamp2 - timestamp1) * 1.0 /
                                    Stopwatch.Frequency;
            WriteLine(elapsedSeconds + " sec");
        }

        public void DeltaHistogram()
        {
            // (1)
            const int N = 10000000;
            var values = new long[N];
            for (int i = 0; i < N; i++)
                values[i] = Stopwatch.GetTimestamp();
            // (2)
            var deltas = new long[N - 1];
            for (int i = 0; i < N - 1; i++)
                deltas[i] = values[i + 1] - values[i];
            // (3)
            var table =
                from d in deltas
                group d by d into g
                orderby g.Key
                select new
                {
                    Ticks = g.Key,
                    Microseconds = g.Key * 1000000.0 / Stopwatch.Frequency,
                    Count = g.Count()
                };
            // (4)
            WriteLine("Ticks      | Time(us) | Count   ");
            WriteLine("-----------|----------|---------");
            foreach (var line in table)
            {
                var ticks = line.Ticks.ToString().PadRight(8);
                var us = line.Microseconds.ToString("0.0").PadRight(8);
                var count = line.Count.ToString();
                WriteLine($"{ticks}   | {us} | {count}");
            }
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
            RunWithHeader(nameof(MeasurementsExample2), MeasurementsExample2);
            RunWithHeader(nameof(DeltaHistogram), DeltaHistogram);
        }
    }
}