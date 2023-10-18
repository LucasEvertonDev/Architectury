using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositories.Base;

namespace Architecture.Application.Domain.DbContexts.Repositories.MapUserGroupRolesRepository;

public interface IMapPermissoesPorGrupoUsuarioRepository : IRepository<MapPermissoesPorGrupoUsuario>
{
    Task<List<Permissao>> GetRolesByGrupoUsuario(string GrupoUsuarioId);
}
