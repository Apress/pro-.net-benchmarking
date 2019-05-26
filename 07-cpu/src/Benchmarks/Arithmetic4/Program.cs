using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Arithmetic4
{
    [LegacyJitX86Job]
    [LegacyJitX64Job]
    [RyuJitX64Job]
    [MonoJob("Mono x64", @"C:\Program Files\Mono\bin\mono.exe")]
    [MonoJob("Mono x86", @"C:\Program Files (x86)\Mono\bin\mono.exe")]
    [IterationCount(50)]
    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        private uint x = 1, initialValue = uint.MaxValue;

        [Benchmark(OperationsPerInvoke = 16)]
        public void Simple()
        {
            x = initialValue;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
            x = x / 3;
        }

        [Benchmark(OperationsPerInvoke = 16)]
        public void BitHacks()
        {
            x = initialValue;
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
            x = (uint) ((x * (ulong) 0xAAAAAAAB) >> 33);
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}