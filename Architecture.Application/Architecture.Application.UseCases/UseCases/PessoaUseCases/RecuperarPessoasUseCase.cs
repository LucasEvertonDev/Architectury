using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;

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

            return Result.IncludeResult(await _searchRepository.ToListAsync());
        });
    }
}
