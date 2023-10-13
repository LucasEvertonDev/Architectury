using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases;

public class AtualizarUsuarioUseCase : BaseUseCase<AtualizarUsuarioDto>, IAtualizarUsuarioUseCase
{
    private readonly IUpdateRepository<Usuario> _updateUserRepository;
    private readonly ISearchRepository<Usuario> _searchUserRepository;
    private readonly ISearchRepository<GrupoUsuario> _searchUserGroupRepository;

    public AtualizarUsuarioUseCase(IServiceProvider serviceProvider,
        IUpdateRepository<Usuario> updateUserRepository,
        ISearchRepository<Usuario> searchUserRepository,
        ISearchRepository<GrupoUsuario> searchUserGroupRepository
    ) : base(serviceProvider)
    {
        _updateUserRepository = updateUserRepository;
        _searchUserRepository = searchUserRepository;
        _searchUserGroupRepository = searchUserGroupRepository;
    }

    public override async Task<Result> ExecuteAsync(AtualizarUsuarioDto param)
    {
        return await OnTransactionAsync(async () =>
        {
            var usuario = await _searchUserRepository.FirstOrDefaultTrackingAsync(u => u.Id.ToString() == param.Id);

            if (usuario == null)
            {
                return Result.Failure<AtualizarUsuarioUseCase>(Erros.Business.UsuarioInexistente);
            }

            var grupoUsuario = await _searchUserGroupRepository.FirstOrDefaultTrackingAsync(grupo => grupo.Id == new Guid(param.Body.GrupoUsuarioId));

            usuario.AtualizaUsuario(
                    username: param.Body.Username,
                    email: param.Body.Email,
                    nome: param.Body.Nome,
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

            return Result.IncludeResult(await _updateUserRepository.UpdateAsync(usuario));
        });
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task<bool> EmailCadastrado(string email)
    {
        return await _searchUserRepository.FirstOrDefaultAsync(usuario => usuario.Email == email) != null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task<bool> UsernameCadastrado(string userName)
    {
        return await _searchUserRepository.FirstOrDefaultAsync(usuario => usuario.Username == userName) != null;
    }
}
