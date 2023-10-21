namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface IUnitWorkTransaction : IUnitOfWork
{
    Task<TRetorno> OnTransactionAsync<TRetorno>(Func<Task<TRetorno>> func);
}
