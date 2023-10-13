using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Infra.Data.Context.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Context.Repositories;

public class MapPermissoesPorGrupoUsuarioRepository : Repository<MapPermissoesPorGrupoUsuario>, IMapPermissoesPorGrupoUsuarioRepository
{
    public MapPermissoesPorGrupoUsuarioRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<List<Permissao>> GetRolesByGrupoUsuario(string GrupoUsuarioId)
    {
        return await AsQueriable()
            .Include(c => c.Permissao)
            .Include(c => c.GrupoUsuario)
            .Where(p => p.GrupoUsuarioId.ToString() == GrupoUsuarioId)
            .Select(a => a.Permissao)
            .ToListAsync();
    }
}
