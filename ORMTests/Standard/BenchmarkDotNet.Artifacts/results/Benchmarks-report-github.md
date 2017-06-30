``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 1 (10.0.14393)
Processor=Intel Core i5-4690 CPU 3.50GHz (Haswell), ProcessorCount=4
Frequency=3410081 Hz, Resolution=293.2482 ns, Timer=TSC
dotnet cli version=2.0.0-preview2-006497
  [Host]     : .NET Core 4.6.00001.0, 64bit RyuJIT
  Job-MJTQQP : .NET Core 4.6.00001.0, 64bit RyuJIT

InvocationCount=50  LaunchCount=1  TargetCount=10  
UnrollFactor=1  

```
 |        Method |      Mean |     Error |    StdDev |
 |-------------- |----------:|----------:|----------:|
 |       linq2db | 25.915 ms | 0.2156 ms | 0.1127 ms |
 |        efCore | 12.746 ms | 0.2431 ms | 0.1608 ms |
 | efCoreNoTrack |  5.630 ms | 0.0874 ms | 0.0578 ms |
