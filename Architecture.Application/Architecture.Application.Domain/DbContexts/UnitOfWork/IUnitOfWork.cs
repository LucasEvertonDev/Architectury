using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;

namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface IUnitOfWork
{
    TRepository GetCustomRepository<TRepository>() where TRepository : IRepository;
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity;
}
