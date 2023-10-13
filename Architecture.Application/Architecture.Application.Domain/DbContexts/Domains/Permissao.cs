using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.Enuns;

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

        Set(permissao => permissao.Situacao, (int)ESituacao.Ativo);
        return this;
    }

    public void SetId(Guid id)
    {
        this.Id = id;
    }
}
