using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases;

public class RecuperarPessoasUseCase : BaseUseCase, IRecuperarPessoasUseCase
{
    public IEnumerable<Pessoa> Retorno { get; set; }

    private readonly ISearchPessoaRepository _searchRepository;

    public RecuperarPessoasUseCase(IServiceProvider serviceProvider,
        ISearchPessoaRepository searchRepository)
        : base(serviceProvider)
    {
        _searchRepository = searchRepository;
    }

    public override async Task ExecuteAsync()
    {
        await OnTransactionAsync(async () =>
        {
            var aux = await _searchRepository.GetPessoasQuery();

            Retorno = await _searchRepository.ToListAsync();
        });
    }
}
