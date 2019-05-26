using System;
using System.Diagnostics;

namespace InstructionLevelParallelism4
{
    public class Program
    {
        private static int n = 10000000;
        private static int rep = 100;

        static void Main()
        {
            MeasureAll();
            MeasureAll();
        }

        public static void MeasureAll()
        {
            Measure("Loop 00", () => Loop00());
            Measure("Loop 01", () => Loop01());
            Measure("Loop 02", () => Loop02());
            Measure("Loop 03", () => Loop03());
            Measure("Loop 04", () => Loop04());
            Measure("Loop 05", () => Loop05());
            Measure("Loop 06", () => Loop06());
            Measure("Loop 07", () => Loop07());
        }

        public static void Measure(string title, Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < rep; i++)
                action();
            stopwatch.Stop();
            Console.WriteLine(title + ": " + stopwatch.ElapsedMilliseconds);
        }

        public static void Loop00()
        {
            for (int i = 0; i < n; i++) { }
        }

        public static void Loop01()
        {
            for (int i = 0; i < n; i++) { }
        }

        public static void Loop02()
        {
            for (int i = 0; i < n; i++) { }
        }

        public static void Loop03()
        {
            for (int i = 0; i < n; i++) { }
        }
        
        public static void Loop04()
        {
            for (int i = 0; i < n; i++) { }
        }
        
        public static void Loop05()
        {
            for (int i = 0; i < n; i++) { }
        }
        
        public static void Loop06()
        {
            for (int i = 0; i < n; i++) { }
        }
        
        public static void Loop07()
        {
            for (int i = 0; i < n; i++) { }
        }
    }
}