using Microsoft.EntityFrameworkCore;

namespace OtelSample;

public class OtelContext : DbContext
{
    public OtelContext(DbContextOptions<OtelContext> options) : base(options)
    {

    }
    public DbSet<Employee> Employees {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OtelContext).Assembly);
    }
}
