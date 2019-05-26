
BenchmarkDotNet=v0.10.14, OS=macOS High Sierra 10.13.4 (17E199) [Darwin 17.5.0]
Intel Core i7-3615QM CPU 2.30GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
  [Host]        : Mono 5.12.0.226 (2018-02/9824e260f56 Mon), 64bit 
  .NET Core 1.1 : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 2.1 : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  Mono          : Mono 5.12.0.226 (2018-02/9824e260f56 Mon), 64bit 


                   Method |           Job | Runtime |     Toolchain |          Mean |       Error |      StdDev |
------------------------- |-------------- |-------- |-------------- |--------------:|------------:|------------:|
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |   1,208.77 ns |  18.1103 ns |  16.9404 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |   2,416.73 ns |  43.6370 ns |  40.8181 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |      36.21 ns |   0.4139 ns |   0.3456 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |   1,003.55 ns |   0.3257 ns |   0.2888 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |      31.11 ns |   0.4834 ns |   0.4522 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 | 999,436.95 ns | 308.6337 ns | 288.6962 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |      34.83 ns |   0.4929 ns |   0.4610 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |      75.55 ns |   1.1720 ns |   1.0963 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |     201.68 ns |   2.6580 ns |   2.4862 ns |
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |   1,003.98 ns |   0.4290 ns |   0.3803 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |      39.74 ns |   0.4944 ns |   0.4624 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |   1,003.52 ns |   0.3916 ns |   0.3663 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |      75.53 ns |   1.0763 ns |   1.0067 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 | 998,748.31 ns | 241.2401 ns | 225.6561 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |      90.14 ns |   1.5844 ns |   1.4820 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |   1,005.29 ns |   0.5837 ns |   0.5460 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |     411.27 ns |   6.1090 ns |   5.7143 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |   1,029.64 ns |  11.2423 ns |  10.5160 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |      38.56 ns |   0.6951 ns |   0.6502 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |   1,001.56 ns |   0.6661 ns |   0.6231 ns |
         TickCountLatency |          Mono |    Mono |       Default |      36.77 ns |   0.5658 ns |   0.5293 ns |
      TickCountResolution |          Mono |    Mono |       Default | 998,447.23 ns | 165.6399 ns | 146.8355 ns |
         StopwatchLatency |          Mono |    Mono |       Default |      36.31 ns |   0.4660 ns |   0.4359 ns |
      StopwatchResolution |          Mono |    Mono |       Default |      99.96 ns |   0.5818 ns |   0.5442 ns |
