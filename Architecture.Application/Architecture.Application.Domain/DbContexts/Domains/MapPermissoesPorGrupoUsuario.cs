using Architecture.Application.Core.Structure.Attributes;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.Enuns;

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
        Set(GrupoUsuario => grupoUsuario)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.MapPermissoesPorGrupoUsuario.GrupoUsuarioObrigatorio);

        Set(Permissao => permissao)
          .ValidateWhen()
          .IsNull()
          .AddFailure(Erros.MapPermissoesPorGrupoUsuario.PermissaoObrigatoria);

        return this;
    }

    public MapPermissoesPorGrupoUsuario CriarMapeamentoParaCarga(Guid grupoUsuario, Guid permissao)
    {
        Set(GrupoUsuarioId => grupoUsuario)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.MapPermissoesPorGrupoUsuario.GrupoUsuarioObrigatorio);

        Set(PermissaoId => permissao)
          .ValidateWhen()
          .IsNull()
          .AddFailure(Erros.MapPermissoesPorGrupoUsuario.PermissaoObrigatoria);

        Set(Situacao => (int)ESituacao.Ativo);

        return this;
    }

    public void SetId(Guid id)
    {
        this.Id = id;
    }
}
