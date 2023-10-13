using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Infra.Data.Structure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Context;

public class ArchitectureDbContext : BaseDbContext<ArchitectureDbContext>
{
    public ArchitectureDbContext(DbContextOptions<ArchitectureDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
    {
        Database.Migrate();
    }

    public DbSet<Pessoa> Pessoas { get; set; }

    public DbSet<Endereco> Enderecos { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Permissao> Permissoes { get; set; }

    public DbSet<MapPermissoesPorGrupoUsuario> MapPermissoesPorGruposUsuarios { get; set; }

    public DbSet<GrupoUsuario> GruposUsuarios { get; set; }

    public DbSet<CredenciaisCliente> CredenciaisClientes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Apply configurations automatic 
        builder.ApplyConfigurationsFromAssembly(typeof(ArchitectureDbContext).Assembly);
    }
}
