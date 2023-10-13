using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;

namespace Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;

public interface IPessoaRepository : IRepository<Pessoa>
{
    Task<dynamic> GetPessoasQuery();
}
