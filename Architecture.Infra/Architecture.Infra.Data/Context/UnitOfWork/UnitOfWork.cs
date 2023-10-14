using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Architecture.Infra.Data.Context.UnitOfWork;

public class UnitOfWork<TDbContext> : IUnitWorkTransaction where TDbContext : DbContext
{
    public readonly TDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private Dictionary<Type, object> _repositories;
    public UnitOfWork(TDbContext applicationDbContext, IServiceProvider serviceProvider)
    {
        _context = applicationDbContext;
        _serviceProvider = serviceProvider;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : IEntity
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = _serviceProvider.GetService<IRepository<TEntity>>();
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public TRepository GetCustomRepository<TRepository>() where TRepository : IRepository
    {
        if (_repositories.ContainsKey(typeof(TRepository)))
        {
            return (TRepository)_repositories[typeof(TRepository)];
        }

        var repository = _serviceProvider.GetService<TRepository>();
        _repositories.Add(typeof(TRepository), repository);
        return repository;
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
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
