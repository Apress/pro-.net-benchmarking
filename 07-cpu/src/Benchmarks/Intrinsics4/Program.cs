using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Intrinsics4
{
    public static unsafe class CompareHelper
    {
        // Assuming x.Length == y.Length
        public static bool NotEqualManual(int[] x, int[] y)
        {
            for (int i = 0; i < x.Length; i++)
                if (x[i] == y[i])
                    return false;
            return true;
        }

        // Assuming x.Length == y.Length; x.Length % 4 == 0
        public static bool NotEqualSse41(int[] x, int[] y)
        {
            fixed (int* xp = &x[0])
            fixed (int* yp = &y[0])
            {
                for (int i = 0; i < x.Length; i += 4)
                {
                    Vector128<int> xVector = Sse2.LoadVector128(xp + i);
                    Vector128<int> yVector = Sse2.LoadVector128(yp + i);
                    Vector128<int> mask = Sse2.CompareEqual(xVector, yVector);
                    if (!Sse41.TestAllZeros(mask, mask))
                        return false;
                }
            }

            return true;
        }

        // Assuming x.Length == y.Length; x.Length % 8 == 0
        public static bool NotEqualAvx2(int[] x, int[] y)
        {
            fixed (int* xp = &x[0])
            fixed (int* yp = &y[0])
            {
                for (int i = 0; i < x.Length; i += 8)
                {
                    Vector256<int> xVector = Avx.LoadVector256(xp + i);
                    Vector256<int> yVector = Avx.LoadVector256(yp + i);
                    Vector256<int> mask = Avx2.CompareEqual(xVector, yVector);
                    if (!Avx.TestZ(mask, mask))
                        return false;
                }
            }

            return true;
        }
    }

    [DisassemblyDiagnoser(printSource: true)]
    public class Benchmarks
    {
        private const int n = 100000;
        private int[] x = new int[n];
        private int[] y = new int[n];

        [GlobalSetup]
        public void Setup()
        {
            Array.Fill(x, 1);
            Array.Fill(y, 2);
        }

        [Benchmark(Baseline = true)]
        public bool Manual() => CompareHelper.NotEqualManual(x, y);

        [Benchmark]
        public bool Sse41() => CompareHelper.NotEqualSse41(x, y);

        [Benchmark]
        public bool Avx2() => CompareHelper.NotEqualAvx2(x, y);
    }

    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}