using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;

namespace Arithmetic2
{
    [Config(typeof(Config))]
    public class Benchmarks
    {
        private int positive = 1, negative = -1;
        
        [Benchmark]
        public int Positive()
        {
            return Math.Abs(positive);
        }
        
        [Benchmark]
        public int Negative()
        {
            return Math.Abs(negative);
        }
    }

    public class Config : ManualConfig
    {
        public Config()
        {
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp20).WithId(".NETCore20"));
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp21).WithId(".NETCore21"));
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}