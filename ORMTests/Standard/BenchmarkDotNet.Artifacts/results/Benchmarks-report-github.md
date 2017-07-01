``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-3632QM CPU 2.20GHz (Ivy Bridge), ProcessorCount=8
Frequency=2143579 Hz, Resolution=466.5095 ns, Timer=TSC
dotnet cli version=2.0.0-preview2-006497
  [Host]     : .NET Core 4.6.00001.0, 64bit RyuJIT
  Job-HFGITP : .NET Core 4.6.00001.0, 64bit RyuJIT

InvocationCount=50  LaunchCount=1  TargetCount=10  
UnrollFactor=1  

```
 |        Method |     Mean |     Error |    StdDev |
 |-------------- |---------:|----------:|----------:|
 |       linq2db | 35.55 ms | 3.5691 ms | 2.3607 ms |
 |        efCore | 24.54 ms | 3.2134 ms | 2.1254 ms |
 | efCoreNoTrack | 10.18 ms | 0.6578 ms | 0.4351 ms |
