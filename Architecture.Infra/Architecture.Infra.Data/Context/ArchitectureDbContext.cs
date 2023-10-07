using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Infra.Data.Structure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Context;

public class ArchitectureDbContext : BaseDbContext<ArchitectureDbContext>
{
    public ArchitectureDbContext(DbContextOptions<ArchitectureDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Pessoa> Pessoas { get; set; }

    public DbSet<Endereco> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Apply configurations automatic 
        builder.ApplyConfigurationsFromAssembly(typeof(ArchitectureDbContext).Assembly);
    }
}
