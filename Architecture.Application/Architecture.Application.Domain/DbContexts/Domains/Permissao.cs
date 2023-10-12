using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Permissao : BaseEntity<Permissao>
{
    public Permissao()
    {
        MapPermissoesPorGrupoUsuario = new List<MapPermissoesPorGrupoUsuario>();
    }

    public string Nome { get; set; }

    public ICollection<MapPermissoesPorGrupoUsuario> MapPermissoesPorGrupoUsuario { get; set; }

    public Permissao CriarPermissao(string nome)
    {
        Set(permissao => permissao.Nome, nome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Permissao.NomeObrigatorio);

        return this;
    }
}
