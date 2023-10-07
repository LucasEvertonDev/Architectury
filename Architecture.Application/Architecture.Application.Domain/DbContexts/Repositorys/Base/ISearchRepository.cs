using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using System.Linq.Expressions;

namespace Architecture.Application.Domain.DbContexts.Repositorys.Base;

public interface ISearchRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> AsQueriable();

    Task<IEnumerable<TEntity>> ToListAsync();

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate);

    Task<PagedResult<TEntity>> ToListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetListFromCacheAsync(Func<TEntity, bool> predicate);
}
