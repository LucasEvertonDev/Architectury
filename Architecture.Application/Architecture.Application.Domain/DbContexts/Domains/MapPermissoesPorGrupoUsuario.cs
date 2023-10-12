using Architecture.Application.Core.Structure.Attributes;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Domains;

[Cache(Key: "MapPermissoesPorGrupoUsuario", AbsoluteExpirationInMinutes: 2, SlidingExpirationInMinutes: 2)]
public class MapPermissoesPorGrupoUsuario : BaseEntity<MapPermissoesPorGrupoUsuario>
{
    public Guid GrupoUsuarioId { get; set; }
    public Guid PermissaoId { get; set; }
    public GrupoUsuario GrupoUsuario { get; set; }
    public Permissao Permissao { get; set; }

    public MapPermissoesPorGrupoUsuario CriarMapeamentoPermissaoPorGrupoUsuario(GrupoUsuario grupoUsuario, Permissao permissao)
    {
        Set(map => map.GrupoUsuario, grupoUsuario)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.MapPermissoesPorGrupoUsuario.GrupoUsuarioObrigatorio);

        Set(map => map.Permissao, permissao)
          .ValidateWhen()
          .IsNull()
          .AddFailure(Erros.MapPermissoesPorGrupoUsuario.PermissaoObrigatoria);

        return this;
    }
}
