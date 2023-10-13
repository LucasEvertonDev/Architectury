using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.Domain.Plugins.JWT;
using Architecture.Application.UseCases.UseCases.AuthUseCases.Interfaces;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.AuthUseCases;

public class LoginUseCase : BaseUseCase<LoginDto>, ILoginUseCase
{
    private readonly IPasswordHash _passwordHash;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IServiceProvider serviceProvider,
        IPasswordHash passwordHash,
        ITokenService tokenService) : base(serviceProvider)
    {
        _passwordHash = passwordHash;
        _tokenService = tokenService;
    }

    public override async Task<Result> ExecuteAsync(LoginDto param)
    {
        return await OnTransactionAsync(async () =>
        {
            if (await CredenciasClienteInvalidas(param))
            {
                return Result.Failure<LoginUseCase>(Erros.Business.CrendenciaisClienteInvalida);
            }

            var user = await _unitOfWork.UsuarioRepository.FirstOrDefaultAsync(a => a.Username == param.Body.Username);

            if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
            {
                return Result.Failure<LoginUseCase>(Erros.Business.UsernamePasswordInvalidos);
            }

            if (!_passwordHash.PasswordIsEquals(param.Body.Password, user?.PasswordHash, user?.Password))
            {
                return Result.Failure<LoginUseCase>(Erros.Business.UsernamePasswordInvalidos);
            }

            user.RegistraUltimoAcesso();

            var roles = await _unitOfWork.MapPermissoesPorGrupoUsuarioRepository.GetRolesByGrupoUsuario(user.GrupoUsuarioId.ToString());

            var (tokem, data) = await _tokenService.GenerateToken(user, param.ClientId, roles);

            await _unitOfWork.UsuarioRepository.UpdateAsync(user);

            return Result.IncludeResult(new TokenModel
            {
                TokenJWT = tokem,
                DataExpiracao = data.ToLocalTime()
            });
        });
    }

    private async Task<bool> CredenciasClienteInvalidas(LoginDto param)
    {
        return !(
                    await _unitOfWork.CredenciaisClientesRepository.
                        GetListFromCacheAsync(a => a.Identificacao == new Guid(param.ClientId)
                            && a.Chave == param.ClientSecret)
                ).Any();
    }
}

