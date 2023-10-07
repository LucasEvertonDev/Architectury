using Architecture.Application.Core.Structure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Context.UnitOfWork;

public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public UnitOfWork(TDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public async Task OpenConnectionAsync(Func<Task> func)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await func();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<TRetorno> OpenConnectionAsync<TRetorno>(Func<Task<TRetorno>> func)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            TRetorno retorno = await func();

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return retorno;
        }
        catch
        {
            await _context.DisposeAsync();

            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.DisposeAsync();
    }
}
