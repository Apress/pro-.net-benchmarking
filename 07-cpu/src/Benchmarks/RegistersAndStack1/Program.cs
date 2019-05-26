using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RegistersAndStack1
{
    public struct Struct3
    {
        public byte X0, X1, X2;
    }

    public struct Struct8
    {
        public byte X0, X1, X2, X3, X4, X5, X6, X7;
    }

    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        public const int Size = 256;

        private int[] sum = new int[Size];
        private Struct3[] struct3 = new Struct3[Size];
        private Struct8[] struct8 = new Struct8[Size];

        [Benchmark(OperationsPerInvoke = Size, Baseline = true)]
        public void Run3()
        {
            for (var i = 0; i < sum.Length; i++)
            {
                Struct3 s = struct3[i];
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