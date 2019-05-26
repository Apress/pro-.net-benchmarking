using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace BranchPrediction4
{
    [Config(typeof(Config))]
    public class Benchmarks
    {
        private int[] a = new int[100001];

        [Params(
            "000000000000",
            "000000000001",
            "000001000001",
            "001001001001",
            "010101010101",
            "random"
            )]
        public string Pattern;

        [GlobalSetup]
        public void Setup()
        {
            var rnd = new Random(42);
            for (int i = 0; i < a.Length; i++)
                a[i] = Pattern == "random"
                    ? rnd.Next(2)
                    : Pattern[i % Pattern.Length] - '0';
        }

        [Benchmark(Baseline = true)]
        public int Run()
        {
            int counter = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] == 0)
                    counter++;
                else
                    counter--;
            return counter;
        }
    }

    public class Config : ManualConfig
    {
        public Config()
        {
            Set(new CustomOrderer());
        }
    }

    public class CustomOrderer : IOrderer
    {
        public IEnumerable<BenchmarkCase> GetExecutionOrder(BenchmarkCase[] benchmarksCase)
            => benchmarksCase;

        public IEnumerable<BenchmarkCase> GetSummaryOrder(BenchmarkCase[] benchmarksCase, Summary summary)
            => benchmarksCase.OrderBy(b => summary[b]?.ResultStatistics?.Mean ?? 0d);

        public string GetHighlightGroupKey(BenchmarkCase benchmarkCase)
            => "Main";

        public string GetLogicalGroupKey(IConfig config, BenchmarkCase[] allBenchmarksCases,
            BenchmarkCase benchmarkCase)
            => "Main";

        public IEnumerable<IGrouping<string, BenchmarkCase>> GetLogicalGroupOrder(
            IEnumerable<IGrouping<string, BenchmarkCase>> logicalGroups)
            => logicalGroups;

        public bool SeparateLogicalGroups => false;
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}