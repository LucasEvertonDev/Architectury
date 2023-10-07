using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Enuns;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Architecture.Infra.Data.Context.Configuration;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();

        builder.Property(u => u.Situacao).IsRequired().HasDefaultValue(ESituacao.Ativo);

        builder
            .HasOne(e => e.Pessoa)
            .WithOne(e => e.Endereco)
            .HasForeignKey<Endereco>(e => e.PessoaId)
            .IsRequired();
    }
}
