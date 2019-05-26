using BenchmarkDotNet.Attributes;

namespace TimerBenchmarks
{
    public class UnixConfig : ConfigBase
    {
    }

    [Config(typeof(UnixConfig))]
    public class UnixBenchmarks : Benchmarks
    {
    }
}