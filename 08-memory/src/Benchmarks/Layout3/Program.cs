using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Layout3
{
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct MyStruct
    {
        [FieldOffset(0x04)] public ulong X0;
        [FieldOffset(0x0C)] public ulong X1;
        [FieldOffset(0x14)] public ulong X2;
        [FieldOffset(0x1C)] public ulong X3;
        [FieldOffset(0x24)] public ulong X4;
        [FieldOffset(0x2C)] public ulong X5;
        [FieldOffset(0x34)] public ulong X6;
        [FieldOffset(0x3C)] public ulong X7;
    }

    public unsafe class Benchmarks
    {
        private int N = 1000;

        public void Run(int offset)
        {
            var myStruct = new MyStruct();
            if ((long) &myStruct.X0 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X0++;
            }
            else if ((long) &myStruct.X1 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X1++;
            }
            else if ((long) &myStruct.X2 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X2++;
            }
            else if ((long) &myStruct.X3 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X3++;
            }
            else if ((long) &myStruct.X4 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X4++;
            }
            else if ((long) &myStruct.X5 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X5++;
            }
            else if ((long) &myStruct.X6 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X6++;
            }
            else if ((long) &myStruct.X7 % 64 == offset)
            {
                for (int i = 0; i < N; i++)
                    myStruct.X7++;
            }
        }

        [Benchmark(Baseline = true)]
        public void InsideCacheLine() => Run(4);

        [Benchmark]
        public void CacheSplit() => Run(60);
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}