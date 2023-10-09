using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Infra.Data.Structure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Infra.Data.Context.Repositories;

public class Repository<TEntity> : Repository<ArchitectureDbContext, TEntity> where TEntity : BaseEntity<TEntity>
{
    public Repository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
