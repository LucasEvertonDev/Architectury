using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Usuarios.CriarUsuario;

public class CriarUsuarioCommandHandler : BaseCommandHandler, IRequestHandler<CriarUsuarioCommand, Result>
{
    private readonly IPasswordHash _passwordHash;

    public CriarUsuarioCommandHandler(IServiceProvider serviceProvider,
        IPasswordHash passwordHash) : base(serviceProvider)
    {
        _passwordHash = passwordHash;
    }

    public async Task<Result> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        return await OnTransactionAsync(async () =>
        {
            var passwordHash = _passwordHash.GeneratePasswordHash();

            var grupoUsuario = await unitOfWork.GrupoUsuarioRepository.FirstOrDefaultTrackingAsync(grupo => grupo.Id == new Guid(request.GrupoUsuarioId));

            var user = new Usuario()
                .CriarUsuario(
                    username: request.Username,
                    password: _passwordHash.EncryptPassword(request.Password, passwordHash),
                    passwordHash: passwordHash,
                    grupoUsuario: grupoUsuario,
                    nome: request.Name,
                    email: request.Email
                );

            if (await UsernameCadastrado(user.Username))
            {
                Result.Failure<CriarUsuarioUseCase>(Erros.Business.UsernameExistente);
            }

            if (await EmailCadastrado(user.Email))
            {
                Result.Failure<CriarUsuarioUseCase>(Erros.Business.EmailExistente);
            }

            if (user.HasFailure() || Result.HasFailures())
            {
                return Result.Failure<CriarUsuarioUseCase>(user);
            }

            user = await unitOfWork.UsuarioRepository.CreateAsync(user);

            return Result.IncludeResult(
                    new UsuarioCriadoModel().FromEntity(user));
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