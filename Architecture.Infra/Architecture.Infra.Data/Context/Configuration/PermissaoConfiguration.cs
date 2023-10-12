using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Enuns;
using Architecture.Infra.Data.Context.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Infra.Data.Context.Configuration;

public class PermissaoConfiguration : BaseEntityConfiguration<Permissao>
{
    public override void Configure(EntityTypeBuilder<Permissao> builder)
    {
        base.Configure(builder);

        builder.ToTable("Permissoes");

        builder.Property(u => u.Nome).HasMaxLength(30).IsRequired();
    }
}
