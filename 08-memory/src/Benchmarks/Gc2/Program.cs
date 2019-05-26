using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Gc2
{
    [Config(typeof(Config))]
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.Mono
                    .With(new[] {new EnvironmentVariable("MONO_GC_PARAMS", "nursery-size=1m")})
                    .WithId("Nursery=1MB"));
                Add(Job.Mono
                    .With(new[] {new EnvironmentVariable("MONO_GC_PARAMS", "nursery-size=4m")})
                    .WithId("Nursery=4MB"));
            }
        }

      
        [Benchmark]
        public byte[] Heap()
        {
            return new byte[10 * 1024];
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}