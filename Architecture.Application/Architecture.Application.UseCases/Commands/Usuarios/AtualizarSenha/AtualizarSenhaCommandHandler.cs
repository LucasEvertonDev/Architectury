using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Usuarios.AtualizarSenha;

public class AtualizarSenhaCommandHandler : BaseCommandHandler, IRequestHandler<AtualizarSenhaCommand, Result>
{
    private readonly IPasswordHash _passwordHash;

    public AtualizarSenhaCommandHandler(IServiceProvider serviceProvider, IPasswordHash passwordHash) : base(serviceProvider)
    {
        _passwordHash = passwordHash;
    }

    public async Task<Result> Handle(AtualizarSenhaCommand request, CancellationToken cancellationToken)
    {
        return await OnTransactionAsync(async () =>
        {
            var usuario = await unitOfWork.UsuarioRepository.FirstOrDefaultAsync(u => u.Id.ToString() == request.Id);

            if (usuario == null)
            {
                return Result.Failure<AtualizarSenhaUseCase>(Erros.Business.UsuarioInexistente);
            }

            string passwordHash = _passwordHash.GeneratePasswordHash();

            usuario.AtualizaSenhaUsuario(
                    password: _passwordHash.EncryptPassword(request.Body.Password, passwordHash),
                    passwordHash: passwordHash
                );

            return Result.IncludeResult(
                new UsuarioAtualizadoModel().FromEntity(await unitOfWork.UsuarioRepository.UpdateAsync(usuario)));
        });
    }
}
