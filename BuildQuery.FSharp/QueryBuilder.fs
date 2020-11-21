namespace BuildQuery.FSharp

open System.Linq
open DataDefinition
open LinqToDB
open LinqToDB.EntityFrameworkCore

module QueryBuilder =
  let Build (context: DataDefinitionContext, closure) =
    context
      .Entities.ToLinqToDB()
      .Where(fun x -> x.IsActive)
      .Where(fun x -> x.SomeCount < closure)
      .Where(fun x -> x.SomeCount / 2 < closure)
      .Where(fun x -> x.SomeCount / 3 > closure)
      .Where(fun x -> x.SomeCount / 4 < closure)
      .Where(fun x -> x.SomeCount / 5 > closure)
      .Where(fun x -> x.SomeCount / 6 < closure)
      .Where(fun x -> x.SomeCount / 7 > closure)
      .Where(fun x -> x.SomeCount / 8 < closure)
      .Where(fun x -> x.SomeCount / 9 > closure)
      .Join(context.Dependants.ToLinqToDB(), (fun x -> x.Id), (fun x -> x.EntityId), fun e dep -> dep)
      .Where(fun dep -> dep.AnotherCount > 56)
      .Join(
        context.Dependants.ToLinqToDB(),
        SqlJoinType.Inner,
        (fun th oth -> th.EntityId = oth.EntityId && th.Id <> oth.Id),
        fun th oth -> th.Id, th.AnotherCount, oth.AnotherCount
      )
      .GroupBy(
        (fun l2dbWontTranslateOtherwise -> l2dbWontTranslateOtherwise.Item1),
        fun l2dbWontTranslateOtherwise -> l2dbWontTranslateOtherwise.Item3 - l2dbWontTranslateOtherwise.Item2
      )
      .Select(fun x -> x.Max())
      .AsCte("whatever")
    
  
