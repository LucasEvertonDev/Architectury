using Architecture.Application.Core.Structure;
using Architecture.Application.Core.Structure.UnitOfWork;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
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


        services.AddScoped<IUnitOfWork, UnitOfWork<ArchitectureDbContext>>();

        services.AddRepository<Pessoa>();

        services.AddRepository<Usuario>();

        services.AddRepository<CredenciaisCliente>();

        services.AddRepository<MapPermissoesPorGrupoUsuario>();

        services.AddRepository<Permissao>();

        services.AddRepository<GrupoUsuario>();

        services.AddScoped<ISearchPessoaRepository, PessoaRepository>();

        services.AddScoped<ISearchMapPermissoesPorGrupoUsuarioRepository, MapPermissoesPorGrupoUsuarioRepository>();

        services.AddRepository<Endereco>();
    }

    public static void AddRepository<TEntity>(this IServiceCollection services) where TEntity : BaseEntity<TEntity>
    {
        services.AddScoped<ICreateRepository<TEntity>, Repository<TEntity>>();
        services.AddScoped<ISearchRepository<TEntity>, Repository<TEntity>>();
        services.AddScoped<IDeleteRepository<TEntity>, Repository<TEntity>>();
        services.AddScoped<IUpdateRepository<TEntity>, Repository<TEntity>>();
    }
}
