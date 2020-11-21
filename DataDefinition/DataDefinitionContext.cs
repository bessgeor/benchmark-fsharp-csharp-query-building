using DataDefinition.Models;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataDefinition
{
    public class DataDefinitionContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }
        public DbSet<DependentEntity> Dependants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            LinqToDBForEFTools.Initialize();
            var connStr = "Host=localhost;Port=5432;Database=stub_db;Username=stub_user;Password=stub_password";
            base.OnConfiguring(optionsBuilder.UseNpgsql(connStr));
        }
    }
}