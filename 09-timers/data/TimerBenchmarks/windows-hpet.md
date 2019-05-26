
BenchmarkDotNet=v0.10.14, OS=Windows 10.0.15063.1387 (1703/CreatorsUpdate/Redstone2)
Intel Core i7-3615QM CPU 2.30GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=14318180 Hz, Resolution=69.8413 ns, Timer=HPET
.NET Core SDK=2.1.300
  [Host]        : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 1.1 : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 2.1 : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  Clr           : .NET Framework 4.6 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3190.0
  Mono          : Mono 5.12.0 (Visual Studio), 64bit 


                   Method |           Job | Runtime |     Toolchain | SystemTimerResolution |              Mean |           Error |          StdDev |
------------------------- |-------------- |-------- |-------------- |---------------------- |------------------:|----------------:|----------------:|
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |        275.682 ns |       4.4616 ns |       3.9551 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |  8,098,731.559 ns | 159,215.7667 ns | 247,879.7672 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |          6.841 ns |       0.0253 ns |       0.0237 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |  8,275,248.743 ns |  25,179.7726 ns |  19,658.7085 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |          2.583 ns |       0.0109 ns |       0.0102 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 | 15,595,564.247 ns | 106,217.2132 ns |  99,355.6374 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |        911.562 ns |       1.7724 ns |       1.6579 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |      1,824.740 ns |       4.6981 ns |       3.9231 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |      1,240.241 ns |      24.9844 ns |      41.0500 ns |
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |      2,467.088 ns |      22.8278 ns |      20.2362 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |        909.943 ns |       6.2904 ns |       5.5763 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |      1,812.407 ns |       7.2156 ns |       6.7495 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |          2.590 ns |       0.0158 ns |       0.0148 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 | 15,593,101.251 ns | 119,943.1957 ns | 112,194.9288 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |        912.354 ns |       1.0651 ns |       0.9963 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |      1,803.705 ns |       4.5740 ns |       4.2785 ns |
       DateTimeNowLatency |           Clr |     Clr |       Default |                156250 |        263.508 ns |       0.7696 ns |       0.6822 ns |
    DateTimeNowResolution |           Clr |     Clr |       Default |                156250 |  7,966,078.379 ns | 158,534.1449 ns | 273,463.6106 ns |
    DateTimeUtcNowLatency |           Clr |     Clr |       Default |                156250 |         15.156 ns |       0.0519 ns |       0.0485 ns |
 DateTimeUtcNowResolution |           Clr |     Clr |       Default |                156250 |  8,280,103.145 ns |   4,199.2368 ns |   3,036.3232 ns |
         TickCountLatency |           Clr |     Clr |       Default |                156250 |          2.888 ns |       0.0110 ns |       0.0103 ns |
      TickCountResolution |           Clr |     Clr |       Default |                156250 | 15,594,405.100 ns | 124,336.5278 ns | 116,304.4539 ns |
         StopwatchLatency |           Clr |     Clr |       Default |                156250 |        910.837 ns |       1.7152 ns |       1.6044 ns |
      StopwatchResolution |           Clr |     Clr |       Default |                156250 |      1,824.916 ns |       3.2477 ns |       3.0379 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |                156250 |      1,603.676 ns |       3.9266 ns |       3.4808 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |                156250 |  8,111,724.498 ns | 157,032.3463 ns | 225,211.0987 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |                156250 |         19.845 ns |       0.0564 ns |       0.0528 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |                156250 |  8,286,140.144 ns |   8,059.5191 ns |   6,292.3419 ns |
         TickCountLatency |          Mono |    Mono |       Default |                156250 |         11.598 ns |       0.0505 ns |       0.0472 ns |
      TickCountResolution |          Mono |    Mono |       Default |                156250 | 15,575,217.355 ns | 128,373.8304 ns | 120,080.9489 ns |
         StopwatchLatency |          Mono |    Mono |       Default |                156250 |        909.682 ns |       1.4679 ns |       1.3013 ns |
      StopwatchResolution |          Mono |    Mono |       Default |                156250 |      1,821.657 ns |       2.2908 ns |       2.1428 ns |
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |        278.097 ns |       0.9767 ns |       0.9136 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |    500,092.814 ns |     163.8927 ns |     153.3053 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |          6.810 ns |       0.0241 ns |       0.0201 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |    500,124.456 ns |     119.9626 ns |     112.2131 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |          2.650 ns |       0.0098 ns |       0.0082 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 | 15,611,946.494 ns |   4,800.4204 ns |   4,255.4472 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |        924.055 ns |       1.3964 ns |       1.3062 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |      1,849.363 ns |       4.4774 ns |       4.1881 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |      1,260.540 ns |       7.4505 ns |       6.9692 ns |
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |      2,527.053 ns |      14.9331 ns |      13.9684 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |        921.688 ns |       2.0815 ns |       1.8452 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |      1,851.120 ns |       3.9097 ns |       3.6571 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |          2.635 ns |       0.0064 ns |       0.0050 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 | 15,610,915.267 ns |   4,210.5554 ns |   3,732.5473 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |        923.254 ns |       2.4343 ns |       2.2771 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |      1,847.890 ns |       3.6738 ns |       3.4365 ns |
       DateTimeNowLatency |           Clr |     Clr |       Default |                  5000 |        276.626 ns |       0.8155 ns |       0.7628 ns |
    DateTimeNowResolution |           Clr |     Clr |       Default |                  5000 |    500,125.754 ns |     146.8873 ns |     137.3984 ns |
    DateTimeUtcNowLatency |           Clr |     Clr |       Default |                  5000 |         15.512 ns |       0.0487 ns |       0.0455 ns |
 DateTimeUtcNowResolution |           Clr |     Clr |       Default |                  5000 |    500,038.539 ns |     154.3340 ns |     144.3641 ns |
         TickCountLatency |           Clr |     Clr |       Default |                  5000 |          2.964 ns |       0.0127 ns |       0.0119 ns |
      TickCountResolution |           Clr |     Clr |       Default |                  5000 | 15,612,332.063 ns |   4,825.8004 ns |   4,277.9460 ns |
         StopwatchLatency |           Clr |     Clr |       Default |                  5000 |        925.117 ns |       1.7788 ns |       1.6639 ns |
      StopwatchResolution |           Clr |     Clr |       Default |                  5000 |      1,852.074 ns |       3.0060 ns |       2.6647 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |                  5000 |      1,633.337 ns |       1.8229 ns |       1.5222 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |                  5000 |    500,045.554 ns |     150.9511 ns |     141.1998 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |                  5000 |         19.827 ns |       0.0637 ns |       0.0596 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |                  5000 |    500,096.379 ns |     163.6039 ns |     153.0352 ns |
         TickCountLatency |          Mono |    Mono |       Default |                  5000 |         11.936 ns |       0.0324 ns |       0.0288 ns |
      TickCountResolution |          Mono |    Mono |       Default |                  5000 | 15,593,272.424 ns |   4,197.4254 ns |   3,720.9080 ns |
         StopwatchLatency |          Mono |    Mono |       Default |                  5000 |        923.972 ns |       2.3883 ns |       2.2340 ns |
      StopwatchResolution |          Mono |    Mono |       Default |                  5000 |      1,847.299 ns |       5.2654 ns |       4.9253 ns |
