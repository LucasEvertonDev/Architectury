using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;

namespace Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;

public interface ISearchPessoaRepository : ISearchRepository<Pessoa>
{
    Task<dynamic> GetPessoasQuery();
}
