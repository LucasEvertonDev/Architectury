using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases;

public class RecuperarPessoasUseCase : BaseUseCase, IRecuperarPessoasUseCase
{
    public RecuperarPessoasUseCase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result> ExecuteAsync()
    {
        return await OnTransactionAsync(async () =>
        {
            var aux = await UnitOfWork.GetCustomRepository<IPessoaRepository>()
                .GetPessoasQuery();

            return Result.IncludeResult(
                await UnitOfWork.GetCustomRepository<IPessoaRepository>()
                    .ToListAsync()
                );
        });
    }
}
