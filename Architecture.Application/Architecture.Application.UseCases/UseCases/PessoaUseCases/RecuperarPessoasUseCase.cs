using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases;

public class RecuperarPessoasUseCase : BaseUseCase<IVoid, IEnumerable<Pessoa>>, IRecuperarPessoasUseCase
{
    private readonly ISearchPessoaRepository _searchRepository;

    public RecuperarPessoasUseCase(IServiceProvider serviceProvider,
        ISearchPessoaRepository searchRepository)
        : base(serviceProvider)
    {
        _searchRepository = searchRepository;
    }

    public override async Task<IEnumerable<Pessoa>> ExecuteAsync(IVoid param)
    {
        return await OnTransactionAsync(async () =>
        {
            var aux = await _searchRepository.GetPessoasQuery();

            return await _searchRepository.ToListAsync();
        });
    }
}
