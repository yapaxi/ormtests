``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-3632QM CPU 2.20GHz (Ivy Bridge), ProcessorCount=8
Frequency=2143579 Hz, Resolution=466.5095 ns, Timer=TSC
dotnet cli version=2.0.0-preview2-006497
  [Host]     : .NET Core 4.6.00001.0, 64bit RyuJIT
  Job-ETVRLL : .NET Core 4.6.00001.0, 64bit RyuJIT

InvocationCount=50  LaunchCount=1  TargetCount=10  
UnrollFactor=1  

```
 |                       Method |     Mean |      Error |    StdDev |
 |----------------------------- |---------:|-----------:|----------:|
 |       linq2db_groupby20times | 14.50 ms |  0.0871 ms | 0.0518 ms |
 |        efCore_groupby20times | 37.37 ms |  0.7708 ms | 0.5098 ms |
 | efCoreNoTrack_groupby20times | 42.75 ms | 11.4881 ms | 7.5986 ms |
