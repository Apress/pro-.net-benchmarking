using System.Globalization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;

namespace Arithmetic3
{
    [Config(typeof(Config))]
    public class Benchmarks
    {
        private double value = -8.98846567431158E+307;
        
        [Benchmark]
        public string ConvertToString()
        {
            return value.ToString(CultureInfo.InvariantCulture);
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