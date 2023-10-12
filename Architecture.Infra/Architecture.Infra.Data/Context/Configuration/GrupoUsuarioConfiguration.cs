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
    }
}
