using Architecture.Application.Core.Structure;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Plugins.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Architectury.Infra.Plugins.TokenJWT;

public class TokenService : ITokenService
{
    private readonly AppSettings _appSettings;

    public TokenService(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public Task<(string, DateTime)> GenerateToken(Usuario usuario, string clientId, List<Permissao> permissoes)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JWTUserClaims.Name, usuario.Nome),
                new Claim(JWTUserClaims.Email, usuario.Email),
                new Claim(JWTUserClaims.UserId, usuario.Id.ToString()),
                new Claim(JWTUserClaims.ClientId, clientId),
            }),
            Expires = DateTime.UtcNow.AddMinutes(_appSettings.Jwt.ExpireInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        foreach (var permissao in permissoes)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, permissao?.Nome));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Task.FromResult((tokenHandler.WriteToken(token), DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireInMinutes)));
    }
}
