using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Layout4
{
    public class Benchmarks
    {
        private readonly byte[] data = new byte[32 * 1024 * 1024];
        private readonly int baseOffset;

        public Benchmarks()
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr addrOfPinnedObject = handle.AddrOfPinnedObject();
            long address = addrOfPinnedObject.ToInt64();
            const int align = 4 * 1024; // 4KB
            baseOffset = (int) (align - address % align);
        }

        [Params(0, 1)]
        public int SrcOffset;

        [Params(
            -65, -64, -63, -34, -33, -32, -31, -3, -2, -1,
            0, 1, 2, 30, 31, 32, 33, 34, 63, 64, 65, 66)]
        public int StrideOffset;

        [Benchmark]
        public void ArrayCopy() => Array.Copy(
            sourceArray: data,
            sourceIndex: baseOffset + SrcOffset,
            destinationArray: data,
            destinationIndex: baseOffset + SrcOffset +
                              24 * 1024 + // 24 KB 
                              StrideOffset,
            length: 16 * 1024 // 16 KB
        );
    }


    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}