using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.Domain.Plugins.JWT;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.AuthUseCases;

public class LoginUseCase : BaseUseCase<LoginDto>, ILoginUseCase
{
    private readonly ISearchRepository<Usuario> _userSearchRepository;
    private readonly IPasswordHash _passwordHash;
    private readonly ITokenService _tokenService;
    private readonly ISearchMapPermissoesPorGrupoUsuarioRepository _mapGrupoUsuarioSearchRepository;
    private readonly IUpdateRepository<Usuario> _updateUserRepository;
    private readonly ISearchRepository<CredenciaisCliente> _searchClientCredentials;

    public LoginUseCase(IServiceProvider serviceProvider,
        ISearchRepository<Usuario> userSearchRepository,
        IUpdateRepository<Usuario> updateUserRepository,
        IPasswordHash passwordHash,
        ITokenService tokenService,
        ISearchRepository<CredenciaisCliente> searchClientCredentials,
        ISearchMapPermissoesPorGrupoUsuarioRepository mapPermissoesPorGrupoUsuarioRepository) : base(serviceProvider)
    {
        _userSearchRepository = userSearchRepository;
        _passwordHash = passwordHash;
        _tokenService = tokenService;
        _mapGrupoUsuarioSearchRepository = mapPermissoesPorGrupoUsuarioRepository;
        _updateUserRepository = updateUserRepository;
        _searchClientCredentials = searchClientCredentials;
    }

    public override async Task<Result> ExecuteAsync(LoginDto param)
    {
        return await OnTransactionAsync(async () =>
        {
            if (await CredenciasClienteInvalidas(param))
            {
                Result.Failure<LoginUseCase>(Erros.Business.CrendenciaisClienteInvalida);
                return;
            }

            var user = await _userSearchRepository.FirstOrDefaultAsync(a => a.Username == param.Body.Username);

            if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
            {
                Result.Failure<LoginUseCase>(Erros.Business.UsernamePasswordInvalidos);
                return;
            }

            if (!_passwordHash.PasswordIsEquals(param.Body.Password, user?.PasswordHash, user?.Password))
            {
                Result.Failure<LoginUseCase>(Erros.Business.UsernamePasswordInvalidos);
                return;
            }

            user.RegistraUltimoAcesso();

            var roles = await _mapGrupoUsuarioSearchRepository.GetRolesByGrupoUsuario(user.GrupoUsuarioId.ToString());

            var (tokem, data) = await _tokenService.GenerateToken(user, param.ClientId, roles);

            await _updateUserRepository.UpdateAsync(user);

            Result.Data = new TokenModel
            {
                TokenJWT = tokem,
                DataExpiracao = data.ToLocalTime()
            };
        });
    }

    private async Task<bool> CredenciasClienteInvalidas(LoginDto param)
    {
        return !(
                    await _searchClientCredentials.
                        GetListFromCacheAsync(a => a.Identificacao == new Guid(param.ClientId)
                            && a.Chave == param.ClientSecret)
                ).Any();
    }
}

