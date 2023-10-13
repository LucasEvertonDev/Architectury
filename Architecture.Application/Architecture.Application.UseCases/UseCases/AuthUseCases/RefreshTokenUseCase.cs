using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Structure.Extensions;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Plugins.JWT;
using Architecture.Application.UseCases.UseCases.AuthUseCases.Interfaces;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.AuthUseCases;

public class RefreshTokenUseCase : BaseUseCase<RefreshTokenDto>, IRefreshTokenUseCase
{
    private readonly ISearchRepository<Usuario> _searchUserRepository;
    private readonly ISearchMapPermissoesPorGrupoUsuarioRepository _mapGrupoUsuarioSearchRepository;
    private readonly ITokenService _tokenService;
    private readonly IUpdateRepository<Usuario> _updateUserRepository;
    private readonly ISearchRepository<CredenciaisCliente> _searchClientCredentials;

    public RefreshTokenUseCase(IServiceProvider serviceProvider,
        ISearchRepository<Usuario> searchUserRepository,
        IUpdateRepository<Usuario> updateUserRepository,
        ISearchRepository<CredenciaisCliente> searchClientCredentials,
        ISearchMapPermissoesPorGrupoUsuarioRepository mapGrupoUsuarioSearchRepository,
        ITokenService tokenService
    )
        : base(serviceProvider)
    {
        _searchUserRepository = searchUserRepository;
        _mapGrupoUsuarioSearchRepository = mapGrupoUsuarioSearchRepository;
        _tokenService = tokenService;
        _updateUserRepository = updateUserRepository;
        _searchClientCredentials = searchClientCredentials;
    }

    public override async Task<Result> ExecuteAsync(RefreshTokenDto refreshTokenDto)
    {
        return await OnTransactionAsync(async () =>
        {
            if (await CredenciasClienteInvalidas(refreshTokenDto))
            {
                return Result.Failure<LoginUseCase>(Erros.Business.CrendenciaisClienteInvalida);
            }

            var user = await _searchUserRepository.FirstOrDefaultAsync(user => user.Id.ToString() == _identity.GetUserClaim(JWTUserClaims.UserId));

            if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
            {
                return Result.Failure<RefreshTokenUseCase>(Erros.Business.RefreshTokenInvalido);
            }

            user.RegistraUltimoAcesso();

            var roles = await _mapGrupoUsuarioSearchRepository.GetRolesByGrupoUsuario(user.GrupoUsuarioId.ToString());

            var (tokem, data) = await _tokenService.GenerateToken(user, refreshTokenDto.ClientId, roles);

            await _updateUserRepository.UpdateAsync(user);

            return Result.IncludeResult(new TokenModel
            {
                TokenJWT = tokem,
                DataExpiracao = data.ToLocalTime()
            });
        });
    }

    private async Task<bool> CredenciasClienteInvalidas(RefreshTokenDto param)
    {
        return !(
                    await _searchClientCredentials.
                        GetListFromCacheAsync(a => a.Identificacao == new Guid(param.ClientId)
                            && a.Chave == param.ClientSecret)
                ).Any();
    }
}

