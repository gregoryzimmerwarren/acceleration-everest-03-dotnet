using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class WarrenEverestDotnetDbContext : DbContext
{
    public WarrenEverestDotnetDbContext(DbContextOptions<WarrenEverestDotnetDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Infrastructure.Data"));
    }
}