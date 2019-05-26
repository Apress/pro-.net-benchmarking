
BenchmarkDotNet=v0.10.14, OS=ubuntu 16.04
Intel Core i7-3615QM CPU 2.30GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
  [Host]        : Mono 5.12.0.226 (tarball Thu), 64bit 
  .NET Core 1.1 : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 2.1 : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  Mono          : Mono 5.12.0.226 (tarball Thu), 64bit 


                   Method |           Job | Runtime |     Toolchain |             Mean |         Error |        StdDev |           Median |
------------------------- |-------------- |-------- |-------------- |-----------------:|--------------:|--------------:|-----------------:|
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |     7,424.765 ns |    11.0401 ns |     9.7868 ns |     7,425.165 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |    11,366.113 ns |   226.6661 ns |   457.8767 ns |    11,101.138 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |       885.261 ns |     1.6074 ns |     1.4249 ns |       885.373 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |     1,876.355 ns |     6.9613 ns |     6.5116 ns |     1,875.732 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |         8.672 ns |     0.1019 ns |     0.0953 ns |         8.662 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 | 3,993,367.797 ns |   207.7013 ns |   173.4400 ns | 3,993,412.539 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |       917.446 ns |     2.3948 ns |     2.2401 ns |       917.353 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |     1,837.305 ns |     6.0708 ns |     5.3816 ns |     1,835.660 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,380.300 ns |     7.3152 ns |     6.4847 ns |     1,378.852 ns |
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |     2,759.422 ns |    10.8101 ns |    10.1117 ns |     2,759.041 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |       904.157 ns |     3.6044 ns |     3.1952 ns |       904.005 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,808.561 ns |    11.2925 ns |    10.0105 ns |     1,808.208 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |         9.254 ns |     0.0636 ns |     0.0564 ns |         9.263 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 | 3,991,236.314 ns | 1,112.1872 ns |   928.7266 ns | 3,991,632.321 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,176.840 ns |     0.3666 ns |     0.3061 ns |     1,176.862 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,842.105 ns |     5.2726 ns |     4.9320 ns |     1,842.186 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |     3,371.841 ns |     5.8693 ns |     4.9011 ns |     3,372.633 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |     6,776.164 ns |    25.3477 ns |    22.4701 ns |     6,773.652 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |       890.925 ns |     2.6671 ns |     2.4948 ns |       890.415 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |     1,898.711 ns |     5.2329 ns |     4.6388 ns |     1,898.909 ns |
         TickCountLatency |          Mono |    Mono |       Default |        12.677 ns |     0.0842 ns |     0.0746 ns |        12.694 ns |
      TickCountResolution |          Mono |    Mono |       Default | 3,987,755.251 ns | 1,148.9886 ns | 1,018.5483 ns | 3,987,605.195 ns |
         StopwatchLatency |          Mono |    Mono |       Default |       969.903 ns |    72.3839 ns |   114.8085 ns |       908.435 ns |
      StopwatchResolution |          Mono |    Mono |       Default |     1,811.968 ns |     6.8445 ns |     6.0675 ns |     1,812.709 ns |
