Each method contains a 1000 iterations of IQueryable creation

```ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET Core SDK=3.1.301
  [Host]           : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT DEBUG
  Server           : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  ServerForce      : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Workstation      : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  WorkstationForce : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT

MaxRelativeError=2E-05  Runtime=.NET Core 3.1  IterationCount=15  
LaunchCount=2  WarmupCount=10  
```
|      Method |              Job | Force | Server |     Mean |    Error |   StdDev | Ratio | RatioSD |      Gen 0 |     Gen 1 | Gen 2 | Allocated |
|------------ |----------------- |------ |------- |---------:|---------:|---------:|------:|--------:|-----------:|----------:|------:|----------:|
| BuildCSharp |           Server | False |   True | 125.4 ms |  1.18 ms |  1.69 ms |  1.00 |    0.00 |          - |         - |     - |  42.26 MB |
| BuildFSharp |           Server | False |   True | 773.1 ms | 10.12 ms | 15.15 ms |  6.18 |    0.11 |  2000.0000 |         - |     - | 475.62 MB |
|             |                  |       |        |          |          |          |       |         |            |           |       |           |
| BuildCSharp |      ServerForce |  True |   True | 121.7 ms |  1.29 ms |  1.93 ms |  1.00 |    0.00 |          - |         - |     - |   42.4 MB |
| BuildFSharp |      ServerForce |  True |   True | 773.8 ms | 10.61 ms | 15.88 ms |  6.36 |    0.14 |  2000.0000 |         - |     - | 475.61 MB |
|             |                  |       |        |          |          |          |       |         |            |           |       |           |
| BuildCSharp |      Workstation | False |  False | 124.7 ms |  2.30 ms |  3.38 ms |  1.00 |    0.00 |  5000.0000 |         - |     - |  42.27 MB |
| BuildFSharp |      Workstation | False |  False | 791.5 ms |  6.59 ms |  9.66 ms |  6.35 |    0.13 | 59000.0000 |         - |     - |  475.7 MB |
|             |                  |       |        |          |          |          |       |         |            |           |       |           |
| BuildCSharp | WorkstationForce |  True |  False | 127.3 ms |  1.27 ms |  1.90 ms |  1.00 |    0.00 |  5000.0000 |         - |     - |  42.37 MB |
| BuildFSharp | WorkstationForce |  True |  False | 781.4 ms |  6.88 ms | 10.29 ms |  6.14 |    0.08 | 59000.0000 | 1000.0000 |     - | 475.37 MB |
