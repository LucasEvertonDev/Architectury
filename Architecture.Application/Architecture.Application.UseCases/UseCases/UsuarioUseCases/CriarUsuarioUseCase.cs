using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases;

public class CriarUsuarioUseCase : BaseUseCase<CriarUsuarioModel>, ICriarUsuarioUseCase
{
    private readonly IPasswordHash _passwordHash;
    private readonly IRepository<Usuario> _createRepository;

    public CriarUsuarioUseCase(IServiceProvider serviceProvider,
        IPasswordHash passwordHash,
        IRepository<Usuario> createRepository) : base(serviceProvider)
    {
        _createRepository = createRepository;
        _passwordHash = passwordHash;
    }

    public override async Task<Result> ExecuteAsync(CriarUsuarioModel param)
    {
        return await OnTransactionAsync(async () =>
        {
            var passwordHash = _passwordHash.GeneratePasswordHash();

            var grupoUsuario = await unitOfWork.GrupoUsuarioRepository.FirstOrDefaultTrackingAsync(grupo => grupo.Id == new Guid(param.GrupoUsuarioId));

            var user = new Usuario()
                .CriarUsuario(
                    username: param.Username,
                    password: _passwordHash.EncryptPassword(param.Password, passwordHash),
                    passwordHash: passwordHash,
                    grupoUsuario: grupoUsuario,
                    nome: param.Name,
                    email: param.Email
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

            user = await _createRepository.CreateAsync(user);

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
