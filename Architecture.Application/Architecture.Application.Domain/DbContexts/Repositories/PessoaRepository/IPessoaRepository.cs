using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositories.Base;

namespace Architecture.Application.Domain.DbContexts.Repositories.PessoaRepository;

public interface IPessoaRepository : IRepository<Pessoa>
{
    Task<dynamic> GetPessoasQuery();
}
