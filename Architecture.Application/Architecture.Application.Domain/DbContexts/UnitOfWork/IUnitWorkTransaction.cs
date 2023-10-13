namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface IUnitWorkTransaction : ITransaction
{
    Task<TRetorno> OpenConnectionAsync<TRetorno>(Func<ITransaction, Task<TRetorno>> func);
    Task OpenConnectionAsync(Func<Task> func);
}
