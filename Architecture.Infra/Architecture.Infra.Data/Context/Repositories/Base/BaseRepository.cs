﻿using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Infra.Data.Structure.Repository;

namespace Architecture.Infra.Data.Context.Repositories.Base;

public class Repository<TEntity> : Repository<ArchitectureDbContext, TEntity> where TEntity : BaseEntity<TEntity>
{
    public Repository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
