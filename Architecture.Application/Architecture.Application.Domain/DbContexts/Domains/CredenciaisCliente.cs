
using Architecture.Application.Core.Structure.Attributes;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.Enuns;

namespace Architecture.Application.Domain.DbContexts.Domains;

[Cache(Key: "CredenciaisCliente", SlidingExpirationInMinutes: 10, AbsoluteExpirationInMinutes: 15)]
public class CredenciaisCliente : BaseEntityWithDates<CredenciaisCliente>
{
    public Guid Identificacao { get; private set; }

    public string Chave { get; private set; }

    public string Descricao { get; private set; }

    public CredenciaisCliente CriarCredenticiaisCliente(Guid identificacao, string chave, string descricao)
    {
        Set(cliente => cliente.Identificacao, identificacao)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.CredenciaisCliente.IdentificacaoObrigatoria);

        Set(cliente => cliente.Chave, chave)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.CredenciaisCliente.ChaveObrigatoria);

        Set(cliente => cliente.Descricao, descricao)
           .ValidateWhen()
           .IsNullOrEmpty()
           .AddFailure(Erros.CredenciaisCliente.ChaveObrigatoria);

        Set(cliente => cliente.Situacao, (int)ESituacao.Ativo);

        Set(cliente => cliente.CreateDate, DateTime.Now);


        return this;
    }

    public void SetId(Guid id)
    {
        this.Id = id;
    }
}
