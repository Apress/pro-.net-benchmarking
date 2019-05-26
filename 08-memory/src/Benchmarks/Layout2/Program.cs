using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Layout2
{
    public unsafe class Benchmarks
    {
        private readonly int[] data = new int[2 * 1024 * 1024];

        [Params(15, 16, 17)]
        public int Delta;

        [Benchmark]
        public bool Calc()
        {
            fixed (int* dataPtr = data)
            {
                int* ptr = dataPtr;
                int d = Delta;
                bool res = false;
                for (int i = 0; i < 1024 * 1024; i++)
                {
                    res |= (ptr[0] < ptr[d]);
                    ptr++;
                }
                return res;
            }
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}