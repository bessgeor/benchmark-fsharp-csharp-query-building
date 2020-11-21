// Learn more about F# at http://fsharp.org

open System
open BenchmarkDotNet.Running
open BuildQuery.Benchmark.Benchmark

[<EntryPoint>]
let main argv =
  BenchmarkRunner.Run(typeof<Benchmark>, config) |> ignore
  0
