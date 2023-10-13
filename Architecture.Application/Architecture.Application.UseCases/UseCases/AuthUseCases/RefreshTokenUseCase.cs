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
    private readonly ITokenService _tokenService;

    public RefreshTokenUseCase(IServiceProvider serviceProvider,
        ITokenService tokenService
    )
        : base(serviceProvider)
    {
        _tokenService = tokenService;
    }

    public override async Task<Result> ExecuteAsync(RefreshTokenDto refreshTokenDto)
    {
        return await OnTransactionAsync(async (transaction) =>
        {
            if (await CredenciasClienteInvalidas(refreshTokenDto))
            {
                return Result.Failure<LoginUseCase>(Erros.Business.CrendenciaisClienteInvalida);
            }

            var user = await transaction.GetRepository<IRepository<Usuario>>()
                .FirstOrDefaultAsync(user => user.Id.ToString() == _identity.GetUserClaim(JWTUserClaims.UserId));

            if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
            {
                return Result.Failure<RefreshTokenUseCase>(Erros.Business.RefreshTokenInvalido);
            }

            user.RegistraUltimoAcesso();

            var roles = await transaction.GetRepository<IMapPermissoesPorGrupoUsuarioRepository>()
                .GetRolesByGrupoUsuario(user.GrupoUsuarioId.ToString());

            var (tokem, data) = await _tokenService.GenerateToken(user, refreshTokenDto.ClientId, roles);

            await transaction.GetRepository<IRepository<Usuario>>()
                .UpdateAsync(user);

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
                    await transaction.GetRepository<IRepository<CredenciaisCliente>>()
                     .GetListFromCacheAsync(a => a.Identificacao == new Guid(param.ClientId)
                            && a.Chave == param.ClientSecret)
                ).Any();
    }
}

