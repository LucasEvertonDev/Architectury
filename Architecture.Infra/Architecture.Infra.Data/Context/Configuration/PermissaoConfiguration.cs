using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Enuns;
using Architecture.Infra.Data.Context.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Architecture.Infra.Data.Context.Configuration;

public class PermissaoConfiguration : BaseEntityConfiguration<Permissao>
{
    public override void Configure(EntityTypeBuilder<Permissao> builder)
    {
        base.Configure(builder);

        builder.ToTable("Permissoes");

        builder.Property(u => u.Nome).HasMaxLength(30).IsRequired();

        DefaultData(builder);
    }

    public void DefaultData(EntityTypeBuilder<Permissao> builder)
    {
        var permissao = new Permissao()
            .CriarPermissao(
                nome: "CHANGE_STUDENTS"
            );

        permissao.SetId(new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"));

        builder.HasData(permissao);
    }
}
