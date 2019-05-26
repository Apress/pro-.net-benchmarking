
BenchmarkDotNet=v0.10.14, OS=ubuntu 16.04
Intel Core i7-3615QM CPU 2.30GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
  [Host]        : Mono 5.12.0.226 (tarball Thu), 64bit 
  .NET Core 1.1 : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 2.1 : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  Mono          : Mono 5.12.0.226 (tarball Thu), 64bit 


                   Method |           Job | Runtime |     Toolchain |             Mean |         Error |        StdDev |           Median |
------------------------- |-------------- |-------- |-------------- |-----------------:|--------------:|--------------:|-----------------:|
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |     4,406.307 ns |    20.4778 ns |    19.1550 ns |     4,403.050 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |     8,792.822 ns |    45.1974 ns |    40.0663 ns |     8,794.587 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |        28.187 ns |     0.1361 ns |     0.1206 ns |        28.179 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |     1,006.281 ns |     1.8856 ns |     1.7638 ns |     1,005.954 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |         8.558 ns |     0.0515 ns |     0.0457 ns |         8.556 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 | 3,993,377.162 ns |   226.3236 ns |   200.6299 ns | 3,993,340.843 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |        35.680 ns |     0.3239 ns |     0.2704 ns |        35.737 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |        73.988 ns |     0.3316 ns |     0.2940 ns |        73.986 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |       311.319 ns |     1.3471 ns |     1.2600 ns |       310.811 ns |
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |       626.867 ns |     3.4563 ns |     3.2330 ns |       626.764 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |        26.984 ns |     0.1556 ns |     0.1456 ns |        26.984 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |        98.510 ns |     0.1625 ns |     0.1440 ns |        98.464 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |         9.277 ns |     0.1540 ns |     0.1440 ns |         9.230 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 | 3,991,308.071 ns |   260.5272 ns |   188.3782 ns | 3,991,327.411 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |        35.393 ns |     0.1371 ns |     0.1216 ns |        35.396 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |        71.639 ns |     0.3938 ns |     0.3491 ns |        71.521 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |     2,229.783 ns |    25.5417 ns |    23.8917 ns |     2,225.625 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |     5,113.644 ns |   243.1098 ns |   716.8148 ns |     4,832.748 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |        28.630 ns |     0.1425 ns |     0.1333 ns |        28.605 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |     1,001.850 ns |     4.0260 ns |     3.7660 ns |     1,002.417 ns |
         TickCountLatency |          Mono |    Mono |       Default |        18.990 ns |     0.1372 ns |     0.1284 ns |        18.942 ns |
      TickCountResolution |          Mono |    Mono |       Default | 3,987,846.401 ns | 1,178.4844 ns | 1,044.6956 ns | 3,987,736.523 ns |
         StopwatchLatency |          Mono |    Mono |       Default |        27.872 ns |     0.6073 ns |     1.1700 ns |        27.849 ns |
      StopwatchResolution |          Mono |    Mono |       Default |        98.264 ns |     0.1954 ns |     0.1828 ns |        98.247 ns |
