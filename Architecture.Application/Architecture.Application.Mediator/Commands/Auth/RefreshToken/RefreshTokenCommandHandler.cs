using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Plugins.JWT;
using Architecture.Application.Core.Structure.Extensions;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Auth.RefreshToken;

public class RefreshTokenCommandHandler : BaseCommandHandler, IRequestHandler<RefreshTokenCommand, Result>
{
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(IServiceProvider serviceProvider,
        ITokenService tokenService) : base(serviceProvider)
    {
        _tokenService = tokenService;
    }

    public async Task<Result> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await OnTransactionAsync(async () =>
        {
            if (await CredenciasClienteInvalidas(request))
            {
                return Result.Failure<RefreshTokenCommandHandler>(Erros.Business.CrendenciaisClienteInvalida);
            }

            var user = await unitOfWork.UsuarioRepository.FirstOrDefaultAsync(user => user.Id.ToString() == _identity.GetUserClaim(JWTUserClaims.UserId));

            if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
            {
                return Result.Failure<RefreshTokenCommandHandler>(Erros.Business.RefreshTokenInvalido);
            }

            user.RegistraUltimoAcesso();

            var roles = await unitOfWork.MapPermissoesPorGrupoUsuarioRepository.GetRolesByGrupoUsuario(user.GrupoUsuarioId.ToString());

            var (tokem, data) = await _tokenService.GenerateToken(user, request.ClientId, roles);

            await unitOfWork.UsuarioRepository.UpdateAsync(user);

            return Result.SetContent(new TokenModel
            {
                TokenJWT = tokem,
                DataExpiracao = data.ToLocalTime()
            });
        });
    }

    private async Task<bool> CredenciasClienteInvalidas(RefreshTokenCommand param)
    {
        return !(
                    await unitOfWork.CredenciaisClientesRepository.
                        GetListFromCacheAsync(a => a.Identificacao == new Guid(param.ClientId)
                            && a.Chave == param.ClientSecret)
                ).Any();
    }
}
