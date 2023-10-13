using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using System.Linq.Expressions;

namespace Architecture.Application.Domain.DbContexts.Repositorys.Base;

public interface IRepository<TEntity> : IRepository where TEntity : IEntity
{
    Task<TEntity> CreateAsync(TEntity domain);

    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

    Task DeleteLogicAsync(Expression<Func<TEntity, bool>> predicate);

    Task DeleteLogicAsync(params TEntity[] entidadesParaExcluir);

    Task DeleteAsync(params TEntity[] entidadesParaExcluir);

    IQueryable<TEntity> AsQueriableTracking();

    IQueryable<TEntity> AsQueriable();

    Task<IEnumerable<TEntity>> ToListAsync();

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate);

    Task<PagedResult<TEntity>> ToListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetListFromCacheAsync(Func<TEntity, bool> predicate);

    Task<TEntity> FirstOrDefaultTrackingAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> UpdateAsync(TEntity domain);
}

public interface IRepository
{ }
