using System;
using System.Diagnostics;

namespace P06_ConditionalJitting_Bad
{
    // Should be executed on Windows (LegacyJIT-x86 is required)
    class Program
    {
        static string Measure1()
        {
            double sum = 1, inc = 1;
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000001; i++)
                sum = sum + inc;
            return $"Result = {sum}, Time = {stopwatch.ElapsedMilliseconds}";
        }
        
        static string Measure2()
        {
            double sum = 1, inc = 1;
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000001; i++)
                sum = sum + inc;
            return $"Result = {sum}, Time = {stopwatch.ElapsedMilliseconds}";
        }
        
        static void Main()
        {
            Console.WriteLine(Measure1());
            Console.WriteLine(Measure2());
        }

    }
}