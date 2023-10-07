namespace Architecture.Application.Core.Structure.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task<TRetorno> OpenConnectionAsync<TRetorno>(Func<Task<TRetorno>> func);
    Task OpenConnectionAsync(Func<Task> func);
    Task RollbackAsync();
}
