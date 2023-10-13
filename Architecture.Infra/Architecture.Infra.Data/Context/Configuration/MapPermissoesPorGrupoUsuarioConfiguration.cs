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
        var mapPermissoesGrupo = new MapPermissoesPorGrupoUsuario()
            .CriarMapeamentoParaCarga(
                grupoUsuario: new Guid("F97E565D-08AF-4281-BC11-C0206EAE06FA"),
                permissao: new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e")
            );

        mapPermissoesGrupo.SetId(new Guid("b94afe49-6630-4bf8-a19d-923af259f475"));

        builder.HasData(mapPermissoesGrupo);
    }
}
