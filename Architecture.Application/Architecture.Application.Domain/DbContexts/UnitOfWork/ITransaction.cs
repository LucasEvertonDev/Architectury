using Architecture.Application.Domain.DbContexts.Repositorys.Base;

namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface ITransaction
{
    TRepository GetRepository<TRepository>() where TRepository : IRepository;
}
