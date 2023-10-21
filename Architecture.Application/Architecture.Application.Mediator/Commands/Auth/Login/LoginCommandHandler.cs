using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.Domain.Plugins.JWT;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Auth.Login;

public class LoginCommandHandler : BaseCommandHandler, IRequestHandler<LoginCommand, Result>
{
    private readonly IPasswordHash _passwordHash;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IServiceProvider serviceProvider,
        IPasswordHash passwordHash,
        ITokenService tokenService) : base(serviceProvider)
    {
        _passwordHash = passwordHash;
        _tokenService = tokenService;
    }

    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await OnTransactionAsync(async () =>
        {
            if (await CredenciasClienteInvalidas(unitOfWork, request))
            {
                return Result.Failure<LoginCommandHandler>(Erros.Business.CrendenciaisClienteInvalida);
            }

            var user = await unitOfWork.UsuarioRepository.FirstOrDefaultAsync(a => a.Username == request.Body.Username);

            if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
            {
                return Result.Failure<LoginCommandHandler>(Erros.Business.UsernamePasswordInvalidos);
            }

            if (!_passwordHash.PasswordIsEquals(request.Body.Password, user?.PasswordHash, user?.Password))
            {
                return Result.Failure<LoginCommandHandler>(Erros.Business.UsernamePasswordInvalidos);
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

    private async Task<bool> CredenciasClienteInvalidas(IUnitOfWork unitOfWork, LoginCommand param)
    {
        return !(
                    await unitOfWork.CredenciaisClientesRepository.
                        GetListFromCacheAsync(a => a.Identificacao == new Guid(param.ClientId)
                            && a.Chave == param.ClientSecret)
                ).Any();
    }
}
