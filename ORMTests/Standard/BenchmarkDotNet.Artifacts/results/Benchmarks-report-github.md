``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 1 (10.0.14393)
Processor=Intel Core i5-4690 CPU 3.50GHz (Haswell), ProcessorCount=4
Frequency=3410081 Hz, Resolution=293.2482 ns, Timer=TSC
dotnet cli version=2.0.0-preview2-006497
  [Host]     : .NET Core 4.6.00001.0, 64bit RyuJIT
  Job-OWHBZQ : .NET Core 4.6.00001.0, 64bit RyuJIT

InvocationCount=300  LaunchCount=1  TargetCount=10  
UnrollFactor=1  

```
 |        Method |     Mean |     Error |    StdDev |
 |-------------- |---------:|----------:|----------:|
 |       linq2db | 9.605 ms | 0.1561 ms | 0.1032 ms |
 |        efCore | 9.113 ms | 0.8954 ms | 0.5923 ms |
 | efCoreNoTrack | 7.013 ms | 0.1082 ms | 0.0715 ms |
