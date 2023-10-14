using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases;

public class ExcluirUsuarioUseCase : BaseUseCase<ExcluirUsuarioDto>, IExcluirUsuarioUseCase
{

    public ExcluirUsuarioUseCase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result> ExecuteAsync(ExcluirUsuarioDto param)
    {
        return await OnTransactionAsync(async () =>
        {
            var usuario = await unitOfWork.UsuarioRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id);

            if (usuario == null)
            {
                return Result.Failure<ExcluirUsuarioUseCase>(Erros.Business.UsuarioInexistente);
            }

            await unitOfWork.UsuarioRepository.DeleteLogicAsync(usuario);

            return Result;
        });
    }
}
