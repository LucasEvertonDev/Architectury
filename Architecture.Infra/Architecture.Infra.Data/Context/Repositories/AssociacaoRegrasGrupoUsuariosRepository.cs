using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Infra.Data.Context.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Architecture.Infra.Data.Context.Repositories;

public class MapPermissoesPorGrupoUsuarioRepository : Repository<MapPermissoesPorGrupoUsuario>, ISearchMapPermissoesPorGrupoUsuarioRepository
{
    private readonly IMemoryCache _memoryCache;

    public MapPermissoesPorGrupoUsuarioRepository(IServiceProvider serviceProvider,
        IMemoryCache memoryCache) : base(serviceProvider)
    {
        _memoryCache = memoryCache;
    }

    public async Task<List<Permissao>> GetRolesByGrupoUsuario(string GrupoUsuarioId)
    {
        return await this.AsQueriable()
            .Include(c => c.Permissao)
            .Include(c => c.GrupoUsuario)
            .Where(p => p.GrupoUsuarioId.ToString() == GrupoUsuarioId)
            .Select(a => a.Permissao)
            .ToListAsync();
    }
}
