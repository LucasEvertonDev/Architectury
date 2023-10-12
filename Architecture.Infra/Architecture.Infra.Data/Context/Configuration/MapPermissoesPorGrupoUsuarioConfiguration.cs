using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Enuns;
using Architecture.Infra.Data.Context.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Infra.Data.Context.Configuration;

public class MapPermissoesPorGrupoUsuarioConfiguration : BaseEntityConfiguration<MapPermissoesPorGrupoUsuario>
{
    public override void Configure(EntityTypeBuilder<MapPermissoesPorGrupoUsuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("MapPermissoesPorGrupoUsuario");

        builder.HasOne(m => m.GrupoUsuario)
             .WithMany(GrupoUsuario => GrupoUsuario.MapPermissoesPorGrupoUsuario)
             .HasForeignKey(m => m.GrupoUsuarioId);

        builder.HasOne(m => m.Permissao)
            .WithMany(role => role.MapPermissoesPorGrupoUsuario)
            .HasForeignKey(m => m.PermissaoId);

        DefaultData(builder);
    }

    public void DefaultData(EntityTypeBuilder<MapPermissoesPorGrupoUsuario> builder)
    {
        //var grupoUsuarioAdmin = new GrupoUsuario()
        //    .CriarGrupoUsuario(
        //        nome: "Admin",
        //        descricao: "Administrador do sistema"
        //    );

        //var permissao = new Permissao()
        //    .CriarPermissao(
        //        nome: "CHANGE_STUDENTS"
        //    );

        //var mapPermissoesGrupo = new MapPermissoesPorGrupoUsuario()
        //    .CriarMapeamentoPermissaoPorGrupoUsuario(
        //        grupoUsuario: grupoUsuarioAdmin,
        //        permissao: permissao
        //    );

        //builder.HasData(mapPermissoesGrupo);
    }
}
