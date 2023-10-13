using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using System.Linq.Expressions;

namespace Architecture.Application.Domain.DbContexts.Repositorys.Base;

public interface ISearchRepository<TEntity> where TEntity : IEntity
{
 
}
