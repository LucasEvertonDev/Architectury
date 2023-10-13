using Architecture.Application.Core.Structure;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Architecture.Infra.Data.Context;
using Architecture.Infra.Data.Context.Repositories;
using Architecture.Infra.Data.Context.Repositories.Base;
using Architecture.Infra.Data.Context.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Infra.Data;

public static class BootstrapModule
{
    public static void RegisterInfraData(this IServiceCollection services, AppSettings configuration)
    {
        services.AddDbContext<ArchitectureDbContext>(options =>
           options.UseSqlServer(configuration.ConnectionStrings.SqlConnection,
           b => b.MigrationsAssembly(typeof(ArchitectureDbContext).Assembly.FullName)));

        services.AddScoped<IUnitWorkTransaction, UnitOfWork<ArchitectureDbContext>>();

        services.AddRepository<Pessoa>();

        services.AddRepository<Usuario>();

        services.AddRepository<CredenciaisCliente>();

        services.AddRepository<MapPermissoesPorGrupoUsuario>();

        services.AddRepository<Permissao>();

        services.AddRepository<GrupoUsuario>();

        services.AddRepository<Endereco>();

        services.AddScoped<IPessoaRepository, PessoaRepository>();

        services.AddScoped<IMapPermissoesPorGrupoUsuarioRepository, MapPermissoesPorGrupoUsuarioRepository>();

    }

    public static void AddRepository<TEntity>(this IServiceCollection services) where TEntity : BaseEntity<TEntity>
    {
        services.AddScoped<IRepository<TEntity>, Repository<TEntity>>();
    }
}
