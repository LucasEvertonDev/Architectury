using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Infra.Data.Context.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Infra.Data.Context.Configuration;

public class UsuarioConfiguration : BaseEntityConfiguration<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("Usuarios");

        builder.Property(u => u.Username).HasMaxLength(20).IsRequired();

        builder.Property(u => u.Password).HasMaxLength(300).IsRequired();

        builder.Property(u => u.PasswordHash).HasMaxLength(300).IsRequired();

        builder.Property(u => u.Nome).HasMaxLength(50).IsRequired();

        builder.Property(u => u.Email).HasMaxLength(50).IsRequired();

        builder.Property(u => u.UltimoAcesso);

        builder.HasOne(u => u.GrupoUsuario).WithMany(e => e.Usuarios).HasForeignKey(a => a.GrupoUsuarioId).IsRequired();
    }
}

