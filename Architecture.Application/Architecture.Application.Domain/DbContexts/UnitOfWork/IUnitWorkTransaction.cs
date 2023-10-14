namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface IUnitWorkTransaction : IUnitOfWork
{
    Task<TRetorno> OpenConnectionAsync<TRetorno>(Func<Task<TRetorno>> func);
    Task OpenConnectionAsync(Func<Task> func);
}
