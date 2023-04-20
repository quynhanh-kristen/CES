using Microsoft.EntityFrameworkCore;

namespace RoutePlanning.Infrastructure.Database;
public sealed class RoutePlanningDatabaseContext : DbContext
{
    public RoutePlanningDatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoutePlanningDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
