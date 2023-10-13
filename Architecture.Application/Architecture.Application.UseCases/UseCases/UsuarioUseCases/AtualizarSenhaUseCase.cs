using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases;

public class AtualizarSenhaUseCase : BaseUseCase<AtualizarSenhaUsuarioDto>, IAtualizarSenhaUseCase
{
    private readonly IPasswordHash _passwordHash;

    public AtualizarSenhaUseCase(IServiceProvider serviceProvider, IPasswordHash passwordHash) : base(serviceProvider)
    {
        _passwordHash = passwordHash;
    }

    public override async Task<Result> ExecuteAsync(AtualizarSenhaUsuarioDto param)
    {
        return await OnTransactionAsync(async (transaction) =>
        {
            var usuario = await transaction.GetRepository<IRepository<Usuario>>()
                .FirstOrDefaultAsync(u => u.Id.ToString() == param.Id);

            if (usuario == null)
            {
                return Result.Failure<AtualizarSenhaUseCase>(Erros.Business.UsuarioInexistente);
            }

            string passwordHash = _passwordHash.GeneratePasswordHash();

            usuario.AtualizaSenhaUsuario(
                    password: _passwordHash.EncryptPassword(param.Body.Password, passwordHash),
                    passwordHash: passwordHash
                );

            return Result.IncludeResult(
                new UsuarioAtualizadoModel().FromEntity(
                    await transaction.GetRepository<IRepository<Usuario>>()
                        .UpdateAsync(usuario))
                );
        });
    }
}
