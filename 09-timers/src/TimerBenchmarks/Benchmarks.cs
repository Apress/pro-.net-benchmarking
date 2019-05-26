using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace TimerBenchmarks
{
    public class ConfigBase : ManualConfig
    {
        public ConfigBase()
        {
            Add(DefaultConfig.Instance);
            Add(MarkdownExporter.Console);
            Add(Job.Mono);
            Add(Job.Default.With(Runtime.Core)
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp11))
                .WithId(".NET Core 1.1"));
            Add(Job.Default.With(Runtime.Core)
                .With(CsProjCoreToolchain.From(NetCoreAppSettings.NetCoreApp21))
                .WithId(".NET Core 2.1"));
        }
    }
    
    public class Benchmarks
    {                
        [Benchmark]
        public long DateTimeNowLatency() => DateTime.Now.Ticks;

        [Benchmark]
        public long DateTimeNowResolution()
        {
            long lastTicks = DateTime.Now.Ticks;
            while (DateTime.Now.Ticks == lastTicks)
            {
            }

            return lastTicks;
        }

        [Benchmark]
        public long DateTimeUtcNowLatency() => DateTime.UtcNow.Ticks;

        [Benchmark]
        public long DateTimeUtcNowResolution()
        {
            long lastTicks = DateTime.UtcNow.Ticks;
            while (DateTime.UtcNow.Ticks == lastTicks)
            {
            }

            return lastTicks;
        }

        [Benchmark]
        public long TickCountLatency() => Environment.TickCount;

        [Benchmark]
        public long TickCountResolution()
        {
            long lastTimestamp = Environment.TickCount;
            while (Environment.TickCount == lastTimestamp)
            {
            }

            return lastTimestamp;
        }

        [Benchmark]
        public long StopwatchLatency() => Stopwatch.GetTimestamp();

        [Benchmark]
        public long StopwatchResolution()
        {
            long lastTimestamp = Stopwatch.GetTimestamp();
            while (Stopwatch.GetTimestamp() == lastTimestamp)
            {
            }

            return lastTimestamp;
        }
    }
}