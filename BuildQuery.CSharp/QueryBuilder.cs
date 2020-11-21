using System.Linq;
using DataDefinition;
using LinqToDB;
using LinqToDB.EntityFrameworkCore;

namespace BuildQuery.CSharp
{
  public static class QueryBuilder
  {
    public static IQueryable<int> Build(DataDefinitionContext context, int closure)
    {
      return
        context
          .Entities.ToLinqToDB()
          .Where(x => x.IsActive)
          .Where(x => x.SomeCount < closure)
          .Where(x => x.SomeCount / 2 < closure)
          .Where(x => x.SomeCount / 3 > closure)
          .Where(x => x.SomeCount / 4 < closure)
          .Where(x => x.SomeCount / 5 > closure)
          .Where(x => x.SomeCount / 6 < closure)
          .Where(x => x.SomeCount / 7 > closure)
          .Where(x => x.SomeCount / 8 < closure)
          .Where(x => x.SomeCount / 9 > closure)
          .Join(context.Dependants.ToLinqToDB(), x => x.Id, x => x.EntityId, (e, dep) => dep)
          .Where(dep => dep.AnotherCount > 56)
          .Join(
            context.Dependants.ToLinqToDB(),
            SqlJoinType.Inner,
            (th, oth) => th.EntityId == oth.EntityId && th.Id != oth.Id,
            (th, oth) => new {th.Id, My = th.AnotherCount, Other = oth.AnotherCount})
          .GroupBy(x => x.Id, x => x.Other - x.My)
          .Select(x => x.Max())
          .AsCte("whatever");
    }
  }
}