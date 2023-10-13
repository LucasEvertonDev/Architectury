using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Infra.Data.Context.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Context.Repositories;

public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
{
    public PessoaRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<dynamic> GetPessoasQuery()
    {
        var pessoaList =
            from pessoas in _applicationDbContext.Pessoas
            join endereco in _applicationDbContext.Enderecos on pessoas.Id equals endereco.PessoaId
            select new
            {
                Data = pessoas.DataNascimento,
                pessoas.Id,
                pessoas.Email,
                pessoas.Nome.PrimeiroNome,
                pessoas.Nome.Sobrenome,
                Endereco = new
                {
                    endereco.Cep,
                    endereco.Estado,
                    endereco.Cidade
                }
            }
        ;

        return await pessoaList.ToListAsync();
    }
}
