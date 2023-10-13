using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;

namespace Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;

public interface IMapPermissoesPorGrupoUsuarioRepository : IRepository<MapPermissoesPorGrupoUsuario>
{
    Task<List<Permissao>> GetRolesByGrupoUsuario(string GrupoUsuarioId);
}
