using System.Runtime.InteropServices;

namespace QpcExample
{
    // Windows-only
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool QueryPerformanceCounter(out long value);

        [DllImport("kernel32.dll")]
        static extern bool QueryPerformanceFrequency(out long value);
        
        static void Main()
        {
            long ticks;
            QueryPerformanceCounter(out ticks);
        }
    }
}