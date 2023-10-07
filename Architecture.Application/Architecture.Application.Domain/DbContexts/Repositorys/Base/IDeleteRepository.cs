using Architecture.Application.Domain.DbContexts.Domains.Base;
using System.Linq.Expressions;

namespace Architecture.Application.Domain.DbContexts.Repositorys.Base;
public interface IDeleteRepository<T> where T : IEntity
{
    Task DeleteAsync(Expression<Func<T, bool>> predicate);
    Task DeleteLogicAsync(Expression<Func<T, bool>> predicate);
}
