namespace BuildQuery.Benchmark

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Columns
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Diagnosers
open BenchmarkDotNet.Environments
open BenchmarkDotNet.Exporters
open BenchmarkDotNet.Jobs
open BenchmarkDotNet.Loggers
open DataDefinition

module Benchmark =
  let private jobs =
    seq {
      for server in [|true;false|] do
      for force in [|true;false|] do
        let mutable jobId = if server then "Server" else "Workstation"
        if force then jobId <- jobId + "Force" 
        Job.MediumRun
          .WithId(jobId)
          .WithRuntime(CoreRuntime.Core31)
          .WithMaxRelativeError(0.00002)
          .WithGcServer(server)
          .WithGcForce(force)
    }
  let mutable private _config =
    ManualConfig.CreateEmpty()
      .AddLogger(ConsoleLogger.Default)
      .AddDiagnoser(MemoryDiagnoser.Default)
      .AddExporter(HtmlExporter.Default, MarkdownExporter.Default)
      .AddColumnProvider(DefaultColumnProviders.Instance)
  do for job in jobs do _config <- _config.AddJob job

  let config = _config
  
  type Benchmark () =
    let mutable context: DataDefinitionContext = null
    
    [<GlobalSetup>]
    member _.Setup() = context <- new DataDefinitionContext()
    
    [<Benchmark(Baseline = true)>]
    member _.BuildCSharp() = for _ in [1..1000] do BuildQuery.CSharp.QueryBuilder.Build (context, 100) |> ignore
    
    [<Benchmark>]
    member _.BuildFSharp() = for _ in [1..1000] do BuildQuery.FSharp.QueryBuilder.Build (context, 100) |> ignore

