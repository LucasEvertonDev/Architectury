namespace Architecture.Application.Core.Structure.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}
