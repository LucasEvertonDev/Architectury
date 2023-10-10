using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases;

public class RecuperarPessoasUseCase : BaseUseCase, IRecuperarPessoasUseCase
{

    private readonly ISearchPessoaRepository _searchRepository;

    public RecuperarPessoasUseCase(IServiceProvider serviceProvider,
        ISearchPessoaRepository searchRepository)
        : base(serviceProvider)
    {
        _searchRepository = searchRepository;
    }

    public override async Task<Result> ExecuteAsync()
    {
        return await OnTransactionAsync(async () =>
        {
            var aux = await _searchRepository.GetPessoasQuery();

            Result.Data = await _searchRepository.ToListAsync();
        });
    }
}
