using Architecture.Application.Core.Notifications;
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
            //var aux = await _unitOfWork.PessoasRepository.GetPessoasQuery();

            //return Result.IncludeResult(await _unitOfWork.PessoasRepository.ToListAsync());
        });
    }
}
