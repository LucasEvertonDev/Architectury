using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Enuns;
using Architecture.Infra.Data.Context.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Infra.Data.Context.Configuration;

public class GrupoUsuarioConfiguration : BaseEntityConfiguration<GrupoUsuario>
{
    public override void Configure(EntityTypeBuilder<GrupoUsuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("GrupoUsuarios");

        builder.Property(u => u.Nome).HasMaxLength(20).IsRequired();

        builder.Property(u => u.Descricao).HasMaxLength(50);

        DefaultData(builder);
    }

    public void DefaultData(EntityTypeBuilder<GrupoUsuario> builder)
    {
        var grupoUsuarioAdmin = new GrupoUsuario()
               .CriarGrupoUsuario(
                   nome: "Admin",
                   descricao: "Administrador do sistema"
               );

        grupoUsuarioAdmin.SetId(new Guid("F97E565D-08AF-4281-BC11-C0206EAE06FA"));

        var grupoUsuarioCustomer = new GrupoUsuario()
            .CriarGrupoUsuario(
                   nome: "Customer",
                   descricao: "Usuario do sistema"
               );

        grupoUsuarioCustomer.SetId(new Guid("2c2ab8a3-3665-42ef-b4ef-bbec05ac02a5"));

        builder.HasData(grupoUsuarioAdmin, grupoUsuarioCustomer);
    }

}
