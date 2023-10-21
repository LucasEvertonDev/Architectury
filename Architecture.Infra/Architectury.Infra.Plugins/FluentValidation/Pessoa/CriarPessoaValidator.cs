using Architecture.Application.Mediator.Commands.Pessoas.CriarPessoa;
using FluentValidation;

namespace Architectury.Infra.Plugins.FluentValidation.Pessoa;

public class CriarPessoaValidator : AbstractValidator<CriarPessoasCommand>
{
    //public CriarPessoaValidator()
    //{
    //    RuleFor(c => c.PrimeiroNome).NotNullOrEmpty();
    //    RuleFor(c => c.Sobrenome).NotNullOrEmpty();
    //    RuleFor(c => c.Email).NotNullOrEmpty();
    //    RuleFor(c => c.Endereco).NotNullOrEmpty();

    //    When(c => c.Endereco != null, () =>
    //    {
    //        RuleFor(c => c.Endereco.Cep).NotNullOrEmpty();
    //        RuleFor(c => c.Endereco.Cidade).NotNullOrEmpty();
    //        RuleFor(c => c.Endereco.Estado).NotNullOrEmpty();
    //    });
    //}
}
