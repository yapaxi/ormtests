``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-3632QM CPU 2.20GHz (Ivy Bridge), ProcessorCount=8
Frequency=2143579 Hz, Resolution=466.5095 ns, Timer=TSC
dotnet cli version=2.0.0-preview2-006497
  [Host]     : .NET Core 4.6.00001.0, 64bit RyuJIT
  Job-ZGWBUB : .NET Core 4.6.00001.0, 64bit RyuJIT

InvocationCount=50  LaunchCount=1  TargetCount=10  
UnrollFactor=1  

```
 |             Method |     Mean |     Error |    StdDev |
 |------------------- |---------:|----------:|----------:|
 |       linq2db_join | 15.84 ms | 0.6915 ms | 0.4574 ms |
 |        efCore_join | 27.30 ms | 1.4365 ms | 0.9502 ms |
 | efCoreNoTrack_join | 27.78 ms | 0.2877 ms | 0.1903 ms |
