using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Usuarios.AtualizarUsuario;

public class AtualizarUsuarioCommandHandler : BaseCommandHandler, IRequestHandler<AtualizarUsuarioCommand, Result>
{
    public AtualizarUsuarioCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        return await OnTransactionAsync(async () =>
        {
            var usuario = await unitOfWork.UsuarioRepository.FirstOrDefaultTrackingAsync(u => u.Id.ToString() == request.Id);

            if (usuario == null)
            {
                return Result.Failure<AtualizarUsuarioUseCase>(Erros.Business.UsuarioInexistente);
            }

            var grupoUsuario = await unitOfWork.GrupoUsuarioRepository.FirstOrDefaultTrackingAsync(grupo => grupo.Id == new Guid(request.Body.GrupoUsuarioId));

            usuario.AtualizaUsuario(
                    username: request.Body.Username,
                    email: request.Body.Email,
                    nome: request.Body.Nome,
                    grupoUsuario: grupoUsuario
                );

            if (await UsernameCadastrado(usuario.Username))
            {
                Result.Failure<AtualizarUsuarioUseCase>(Erros.Business.UsernameExistente);
            }

            if (await EmailCadastrado(usuario.Email))
            {
                Result.Failure<AtualizarUsuarioUseCase>(Erros.Business.EmailExistente);
            }

            if (usuario.HasFailure() || HasFailure())
            {
                Result.Failure<AtualizarUsuarioUseCase>(usuario);
            }

            return Result.IncludeResult(await unitOfWork.UsuarioRepository.UpdateAsync(usuario));
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task<bool> EmailCadastrado(string email)
    {
        return await unitOfWork.UsuarioRepository.FirstOrDefaultAsync(usuario => usuario.Email == email) != null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task<bool> UsernameCadastrado(string userName)
    {
        return await unitOfWork.UsuarioRepository.FirstOrDefaultAsync(usuario => usuario.Username == userName) != null;
    }
}
