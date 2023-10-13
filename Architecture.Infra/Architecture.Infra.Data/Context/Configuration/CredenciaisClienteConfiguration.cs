using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Infra.Data.Context.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Infra.Data.Context.Configuration;

public class CredenciaisClienteConfiguration : BaseEntityWithDatesConfiguration<CredenciaisCliente>
{
    public override void Configure(EntityTypeBuilder<CredenciaisCliente> builder)
    {
        base.Configure(builder);

        builder.ToTable("CredenciaisCliente");

        builder.Property(u => u.Identificacao).IsRequired();

        builder.Property(u => u.Chave).HasMaxLength(300).IsRequired();

        builder.Property(u => u.Descricao).HasMaxLength(300);

       DefaultData(builder);
    }

    public void DefaultData(EntityTypeBuilder<CredenciaisCliente> builder)
    {
        var cliente = new CredenciaisCliente()
            .CriarCredenticiaisCliente(
                identificacao: new Guid("7064bbbf-5d11-4782-9009-95e5a6fd6822"),
                chave: "dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910",
                descricao: "Cliente padrão da aplicação"
            );

        cliente.SetId(new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"));

        builder.HasData(cliente);
    }
}
