using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Architecture.Infra.Data.Context.Configuration;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();

        builder.Property(u => u.Situacao).IsRequired().HasDefaultValue(ESituacao.Ativo);


        builder.OwnsOne(x => x.Nome)
            .Property(x => x.PrimeiroNome)
            .HasColumnName("PrimeiroNome")
            .IsRequired();

        builder.OwnsOne(x => x.Nome)
            .Property(x => x.Sobrenome)
            .HasColumnName("Sobrenome")
            .IsRequired();
    }
}
