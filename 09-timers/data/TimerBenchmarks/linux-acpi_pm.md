
BenchmarkDotNet=v0.10.14, OS=ubuntu 16.04
Intel Core i7-3615QM CPU 2.30GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
  [Host]        : Mono 5.12.0.226 (tarball Thu), 64bit 
  .NET Core 1.1 : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 2.1 : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  Mono          : Mono 5.12.0.226 (tarball Thu), 64bit 


                   Method |           Job | Runtime |     Toolchain |             Mean |         Error |        StdDev |
------------------------- |-------------- |-------- |-------------- |-----------------:|--------------:|--------------:|
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |     7,484.508 ns |     5.6561 ns |     5.0139 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |    14,980.451 ns |    16.5862 ns |    14.7032 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |       966.382 ns |     5.4788 ns |     4.8568 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |     1,966.105 ns |    13.6896 ns |    12.8053 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |         8.645 ns |     0.0866 ns |     0.0676 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 | 3,991,758.543 ns |   442.2251 ns |   392.0210 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |       995.559 ns |     3.1680 ns |     2.8084 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |     2,013.279 ns |    10.3623 ns |     9.6929 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,510.190 ns |     9.5584 ns |     8.9409 ns |
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |     3,012.181 ns |     9.9136 ns |     8.7881 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |       987.059 ns |     3.8343 ns |     3.5866 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,982.126 ns |     8.4900 ns |     7.9415 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |         8.978 ns |     0.0923 ns |     0.0818 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 | 3,989,244.037 ns |    27.7520 ns |    16.5148 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,293.596 ns |     1.1175 ns |     1.0453 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |     1,996.952 ns |     7.7285 ns |     6.8512 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |     3,405.321 ns |    32.1131 ns |    25.0718 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |     6,955.598 ns |   231.2700 ns |   237.4973 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |       972.404 ns |     4.3546 ns |     3.8602 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |     2,004.025 ns |     8.9740 ns |     8.3943 ns |
         TickCountLatency |          Mono |    Mono |       Default |        12.707 ns |     0.1067 ns |     0.0945 ns |
      TickCountResolution |          Mono |    Mono |       Default | 3,986,321.390 ns | 1,227.4404 ns | 1,088.0938 ns |
         StopwatchLatency |          Mono |    Mono |       Default |       990.344 ns |     4.2855 ns |     4.0087 ns |
      StopwatchResolution |          Mono |    Mono |       Default |     1,976.009 ns |    11.0292 ns |    10.3167 ns |
