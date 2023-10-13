namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface IUnitWorkTransaction : IUnitOfWorkRepos
{
    Task<TRetorno> OpenConnectionAsync<TRetorno>(Func<Task<TRetorno>> func);
    Task OpenConnectionAsync(Func<Task> func);
}
