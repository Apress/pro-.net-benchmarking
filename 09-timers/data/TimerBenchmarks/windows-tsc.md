
BenchmarkDotNet=v0.10.14, OS=Windows 10.0.15063.1155 (1703/CreatorsUpdate/Redstone2)
Intel Core i7-3615QM CPU 2.30GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=2241012 Hz, Resolution=446.2270 ns, Timer=TSC
.NET Core SDK=2.1.300
  [Host]        : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 1.1 : .NET Core 1.1.8 (CoreCLR 4.6.26328.01, CoreFX 4.6.24705.01), 64bit RyuJIT
  .NET Core 2.1 : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  Clr           : .NET Framework 4.6 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3062.0
  Mono          : Mono 5.12.0 (Visual Studio), 64bit 


                   Method |           Job | Runtime |     Toolchain | SystemTimerResolution |              Mean |         Error |        StdDev |
------------------------- |-------------- |-------- |-------------- |---------------------- |------------------:|--------------:|--------------:|
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |        298.270 ns |     0.5029 ns |     0.4704 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 | 15,609,506.561 ns | 2,929.1158 ns | 2,596.5846 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |          6.889 ns |     0.0174 ns |     0.0163 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 | 15,613,558.926 ns |   882.5687 ns |   689.0516 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |          2.592 ns |     0.0100 ns |     0.0093 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 | 15,613,962.461 ns |   816.0249 ns |   681.4177 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |         17.549 ns |     0.0488 ns |     0.0457 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                156250 |        452.894 ns |     0.6089 ns |     0.5696 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET 
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |        709.363 ns |     1.9075 ns |     1.7842 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |         18.220 ns |     0.0913 ns |     0.0854 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |        452.900 ns |     0.5633 ns |     0.5269 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |          2.590 ns |     0.0097 ns |     0.0091 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 | 15,610,899.958 ns | 1,191.6065 ns | 1,056.3280 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |         17.501 ns |     0.0393 ns |     0.0368 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                156250 |        452.800 ns |     0.5519 ns |     0.4892 ns |
       DateTimeNowLatency |           Clr |     Clr |       Default |                156250 |        272.797 ns |     0.6274 ns |     0.5869 ns |
    DateTimeNowResolution |           Clr |     Clr |       Default |                156250 | 15,613,641.736 ns | 1,223.7865 ns | 1,021.9171 ns |
    DateTimeUtcNowLatency |           Clr |     Clr |       Default |                156250 |          6.807 ns |     0.0252 ns |     0.0236 ns |
 DateTimeUtcNowResolution |           Clr |     Clr |       Default |                156250 | 15,613,313.573 ns | 1,404.1924 ns | 1,172.5641 ns |
         TickCountLatency |           Clr |     Clr |       Default |                156250 |          2.903 ns |     0.0102 ns |     0.0096 ns |
      TickCountResolution |           Clr |     Clr |       Default |                156250 | 15,613,127.208 ns | 1,017.7237 ns |   902.1855 ns |
         StopwatchLatency |           Clr |     Clr |       Default |                156250 |         18.378 ns |     0.1060 ns |     0.0992 ns |
      StopwatchResolution |           Clr |     Clr |       Default |                156250 |        452.727 ns |     0.6099 ns |     0.5407 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |                156250 |      1,023.950 ns |     1.4416 ns |     1.1255 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |                156250 | 15,595,502.976 ns | 2,148.8805 ns | 1,794.4124 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |                156250 |         19.484 ns |     0.0460 ns |     0.0430 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |                156250 | 15,593,192.545 ns | 2,059.2890 ns | 1,719.5994 ns |
         TickCountLatency |          Mono |    Mono |       Default |                156250 |         11.571 ns |     0.0251 ns |     0.0223 ns |
      TickCountResolution |          Mono |    Mono |       Default |                156250 | 15,595,630.745 ns | 2,073.5278 ns | 1,838.1282 ns |
         StopwatchLatency |          Mono |    Mono |       Default |                156250 |         23.819 ns |     0.0394 ns |     0.0329 ns |
      StopwatchResolution |          Mono |    Mono |       Default |                156250 |        452.496 ns |     0.9118 ns |     0.8529 ns |
       DateTimeNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |        301.713 ns |     1.1061 ns |     1.0346 ns |
    DateTimeNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |    500,055.716 ns |   160.0451 ns |   149.7063 ns |
    DateTimeUtcNowLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |          6.950 ns |     0.0314 ns |     0.0294 ns |
 DateTimeUtcNowResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |    500,146.849 ns |   138.3481 ns |   129.4109 ns |
         TickCountLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |          2.623 ns |     0.0164 ns |     0.0153 ns |
      TickCountResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 | 15,613,934.036 ns | 4,310.7867 ns | 3,821.3997 ns |
         StopwatchLatency | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |         17.736 ns |     0.0512 ns |     0.0479 ns |
      StopwatchResolution | .NET Core 1.1 |    Core | .NET Core 1.1 |                  5000 |        454.183 ns |     0.7326 ns |     0.6495 ns |
       DateTimeNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |        277.738 ns |     1.0137 ns |     0.9482 ns |
    DateTimeNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |        725.065 ns |     4.6819 ns |     4.3795 ns |
    DateTimeUtcNowLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |         18.409 ns |     0.0603 ns |     0.0564 ns |
 DateTimeUtcNowResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |        454.849 ns |     1.0345 ns |     0.9677 ns |
         TickCountLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |          2.619 ns |     0.0155 ns |     0.0145 ns |
      TickCountResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 | 15,611,615.534 ns | 4,089.2964 ns | 3,625.0544 ns |
         StopwatchLatency | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |         17.666 ns |     0.0693 ns |     0.0649 ns |
      StopwatchResolution | .NET Core 2.1 |    Core | .NET Core 2.1 |                  5000 |        454.207 ns |     1.0035 ns |     0.9387 ns |
       DateTimeNowLatency |           Clr |     Clr |       Default |                  5000 |        276.597 ns |     1.1095 ns |     1.0379 ns |
    DateTimeNowResolution |           Clr |     Clr |       Default |                  5000 |    500,059.057 ns |   152.2033 ns |   142.3711 ns |
    DateTimeUtcNowLatency |           Clr |     Clr |       Default |                  5000 |          6.885 ns |     0.0219 ns |     0.0204 ns |
 DateTimeUtcNowResolution |           Clr |     Clr |       Default |                  5000 |    500,171.385 ns |    78.1807 ns |    69.3052 ns |
         TickCountLatency |           Clr |     Clr |       Default |                  5000 |          2.946 ns |     0.0178 ns |     0.0167 ns |
      TickCountResolution |           Clr |     Clr |       Default |                  5000 | 15,614,278.454 ns | 4,552.6189 ns | 4,035.7777 ns |
         StopwatchLatency |           Clr |     Clr |       Default |                  5000 |         18.499 ns |     0.0871 ns |     0.0814 ns |
      StopwatchResolution |           Clr |     Clr |       Default |                  5000 |        454.396 ns |     1.5841 ns |     1.4042 ns |
       DateTimeNowLatency |          Mono |    Mono |       Default |                  5000 |      1,035.219 ns |     3.8740 ns |     3.6238 ns |
    DateTimeNowResolution |          Mono |    Mono |       Default |                  5000 |    500,101.929 ns |   124.3829 ns |   116.3478 ns |
    DateTimeUtcNowLatency |          Mono |    Mono |       Default |                  5000 |         19.766 ns |     0.0645 ns |     0.0603 ns |
 DateTimeUtcNowResolution |          Mono |    Mono |       Default |                  5000 |    500,084.767 ns |   112.4758 ns |   105.2100 ns |
         TickCountLatency |          Mono |    Mono |       Default |                  5000 |         11.740 ns |     0.0285 ns |     0.0238 ns |
      TickCountResolution |          Mono |    Mono |       Default |                  5000 | 15,595,784.717 ns | 3,707.6099 ns | 3,286.6993 ns |
         StopwatchLatency |          Mono |    Mono |       Default |                  5000 |         24.061 ns |     0.0907 ns |     0.0849 ns |
      StopwatchResolution |          Mono |    Mono |       Default |                  5000 |        454.633 ns |     1.4666 ns |     1.2247 ns |
