using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Layout1
{
    public struct Struct7
    {
        public byte X0, X1, X2, X3, X4, X5, X6;
    }

    public struct Struct8
    {
        public byte X0, X1, X2, X3, X4, X5, X6, X7;
    }

    [LegacyJitX86Job, MonoJob]
    public class Benchmarks
    {
        public const int Size = 256;

        private int[] sum = new int[Size];
        private Struct7[] struct7 = new Struct7[Size];
        private Struct8[] struct8 = new Struct8[Size];

        [Benchmark(OperationsPerInvoke = Size, Baseline = true)]
        public void Run7()
        {
            for (var i = 0; i < sum.Length; i++)
            {
                Struct7 s = struct7[i];
                sum[i] = s.X0 + s.X1;
            }
        }

        [Benchmark(OperationsPerInvoke = Size)]
        public void Run8()
        {
            for (var i = 0; i < sum.Length; i++)
            {
                Struct8 s = struct8[i];
                sum[i] = s.X0 + s.X1;
            }
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}