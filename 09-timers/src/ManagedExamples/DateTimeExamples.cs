using System;
using System.Threading;
using static System.Console;

namespace Examples
{
    public class DateTimeExamples
    {
        public void MeasurementsExample()
        {
            DateTime start = DateTime.UtcNow;
            // Logic
            DateTime end = DateTime.UtcNow;
            TimeSpan elapsed = end - start;
            WriteLine(elapsed.TotalMilliseconds);
        }

        public void TimeSpanProperties()
        {
            TimeSpan span = new TimeSpan(
                days: 8,
                hours: 19,
                minutes: 46,
                seconds: 57,
                milliseconds: 876
            );
            WriteLine("TimeSpan = {0}", span);

            WriteLine("Days:         {0,3} TotalDays:         {1}",
                span.Days, span.TotalDays);
            WriteLine("Hours:        {0,3} TotalHours:        {1}",
                span.Hours, span.TotalHours);
            WriteLine("Minutes:      {0,3} TotalMinutes:      {1}",
                span.Minutes, span.TotalMinutes);
            WriteLine("Seconds:      {0,3} TotalSeconds:      {1}",
                span.Seconds, span.TotalSeconds);
            WriteLine("Milliseconds: {0,3} TotalMilliseconds: {1}",
                span.Milliseconds, span.TotalMilliseconds);
            WriteLine("                  Ticks:             {0}",
                span.Ticks);
        }

        public void IncorrectApiUsage()
        {
            var start = DateTime.UtcNow;
            Thread.Sleep(2500);
            var end = DateTime.UtcNow;
            WriteLine((end - start).Milliseconds);
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
            RunWithHeader(nameof(TimeSpanProperties), TimeSpanProperties);
            RunWithHeader(nameof(IncorrectApiUsage), IncorrectApiUsage);
        }
    }
}