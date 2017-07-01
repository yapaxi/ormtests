``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-3632QM CPU 2.20GHz (Ivy Bridge), ProcessorCount=8
Frequency=2143579 Hz, Resolution=466.5095 ns, Timer=TSC
dotnet cli version=2.0.0-preview2-006497
  [Host]     : .NET Core 4.6.00001.0, 64bit RyuJIT
  Job-RYEWSH : .NET Core 4.6.00001.0, 64bit RyuJIT

InvocationCount=50  LaunchCount=1  TargetCount=10  
UnrollFactor=1  

```
 |                                       Method |     Mean |     Error |    StdDev |
 |--------------------------------------------- |---------:|----------:|----------:|
 |       linq2db_no_includes_concurrent20_burst | 12.43 ms | 0.4460 ms | 0.2950 ms |
 |        efCore_no_includes_concurrent20_burst | 20.13 ms | 1.3736 ms | 0.9085 ms |
 | efCoreNoTrack_no_includes_concurrent20_burst | 19.88 ms | 0.8373 ms | 0.5538 ms |
