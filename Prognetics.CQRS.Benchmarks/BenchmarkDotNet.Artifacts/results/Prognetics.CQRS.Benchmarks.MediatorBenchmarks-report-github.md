``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1219/21H2)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2


```
|                    Method |     Mean |    Error |   StdDev | Allocated |
|-------------------------- |---------:|---------:|---------:|----------:|
|      GenericQuery_Regular | 28.35 ms | 0.296 ms | 0.231 ms |   9.19 KB |
|   GenericQuery_Simplified | 29.72 ms | 0.490 ms | 0.870 ms |   5.85 KB |
|    GenericCommand_Regular | 29.81 ms | 0.412 ms | 0.344 ms |   8.77 KB |
| GenericCommand_Simplified | 30.35 ms | 0.600 ms | 1.020 ms |   5.83 KB |
