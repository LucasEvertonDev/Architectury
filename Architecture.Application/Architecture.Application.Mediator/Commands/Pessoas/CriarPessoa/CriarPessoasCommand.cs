using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Endereco;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Pessoas.CriarPessoa;

public class CriarPessoasCommand : IRequest<Result>, IValidationAsync
{
    public string PrimeiroNome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public DateTime? DataNascimento { get; set; }
    public EnderecoModel Endereco { get; set; }
}
