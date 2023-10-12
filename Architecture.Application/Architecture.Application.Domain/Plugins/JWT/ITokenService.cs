using Architecture.Application.Domain.DbContexts.Domains;

namespace Architecture.Application.Domain.Plugins.JWT;

public interface ITokenService
{
    Task<(string, DateTime)> GenerateToken(Usuario usuario, string clientId, List<Permissao> permissoes);
}