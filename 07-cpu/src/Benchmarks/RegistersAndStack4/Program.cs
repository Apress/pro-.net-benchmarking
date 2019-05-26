using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RegistersAndStack4
{
    public class X {}
    
    [DisassemblyDiagnoser, LegacyJitX86Job]
    public class Benchmarks
    {
        private const int N = 100001;
        
        [Benchmark(Baseline = true)]
        public double Foo()
        {
            double a = 1, b = 1;
            for (int i = 0; i < N; i++)
                a = a + b;
            return a;
        }
        
        [Benchmark]
        public double Bar()
        {   
            double a = 1, b = 1;
            new X(); new X(); new X();
            for (int i = 0; i < N; i++)
                a = a + b;
            return a;
        }
    }

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}