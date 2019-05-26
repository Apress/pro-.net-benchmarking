using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Gc4
{
Kpublic class Benchmarks
{
    public class ClassWithoutFinalizer
    {
    }

    public class ClassWithFinalizer
    {
        ~ClassWithFinalizer()
        {
        }
    }

    [Benchmark(Baseline = true)]
    public object WithoutFinalizer()
    {
        return new ClassWithoutFinalizer();
    }

    [Benchmark]
    public object WithFinalizer()
    {
        return new ClassWithFinalizer();
    }
}

    class Program
    {
        static void Main() => BenchmarkRunner.Run<Benchmarks>();
    }
}