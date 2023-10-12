using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class GrupoUsuario : BaseEntity<GrupoUsuario>
{
    public GrupoUsuario()
    {
        Usuarios = new List<Usuario>();

        MapPermissoesPorGrupoUsuario = new List<MapPermissoesPorGrupoUsuario>();
    }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }

    public ICollection<MapPermissoesPorGrupoUsuario> MapPermissoesPorGrupoUsuario { get; set; }

    public GrupoUsuario CriarGrupoUsuario(string nome, string descricao)
    {
        Set(grupo => grupo.Nome, nome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.GrupoUsuario.NomeObrigatorio);

        Set(grupo => grupo.Descricao, nome)
           .ValidateWhen()
           .IsNullOrEmpty()
           .AddFailure(Erros.GrupoUsuario.DescricaoObrigatoria);

        return this;
    }

    public void VinculaUsuarioAoGrupoUsuario(Usuario usuario)
    {
        this.Usuarios.Add(usuario);
    }

    public void VinculaGrupoUsuarioPermissao(MapPermissoesPorGrupoUsuario MapPermissoesPorGrupoUsuario)
    {
        this.MapPermissoesPorGrupoUsuario.Add(MapPermissoesPorGrupoUsuario);
    }
}
