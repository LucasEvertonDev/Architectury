using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Repositorys.Base;
public interface IUpdateRepository<T> where T : IEntity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    Task<T> UpdateAsync(T domain);
}
