using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Intrinsics3
{
    public struct MyVector4
    {
        public float X, Y, Z, W;

        public MyVector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MyVector4 operator *(MyVector4 left, MyVector4 right)
            => new MyVector4(
                left.X * right.X,
                left.Y * right.Y,
                left.Z * right.Z,
                left.W * right.W);
    }

    [LegacyJitX64Job]
    [RyuJitX64Job]
    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        private Vector4 vectorA, vectorB, vectorC;
        private MyVector4 myVectorA, myVectorB, myVectorC;

        [Benchmark]
        public void MyMul() => myVectorC = myVectorA * myVectorB;

        [Benchmark]
        public void SystemMul() => vectorC = vectorA * vectorB;
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}