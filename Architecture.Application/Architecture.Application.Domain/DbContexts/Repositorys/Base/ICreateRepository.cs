using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Repositorys.Base;

public interface ICreateRepository<T> where T : IEntity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    Task<T> CreateAsync(T domain);
}
