using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using System.Linq.Expressions;

namespace Architecture.Application.Domain.DbContexts.Repositorys.Base;
public interface IDeleteRepository<T> where T : IEntity
{

}
