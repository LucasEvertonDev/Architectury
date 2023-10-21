using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Pessoa;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Pessoas.CriarPessoa;

public class CriarPessoasCommandHandler : BaseCommandHandler, IRequestHandler<CriarPessoasCommand, Result>
{
    public CriarPessoasCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    public async Task<Result> Handle(CriarPessoasCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            Result.Failure<CriarPessoasCommandHandler>(Erros.Pessoa.PessoaNula);
        }

        var pessoa = new Pessoa().CriarPessoa(
            primeiroNome: request.PrimeiroNome,
            sobrenome: request.Sobrenome,
            email: request.Email,
            dataNascimento: request.DataNascimento,
            enderecoModel: request.Endereco
        );

        if (true)
        {
            Result.Failure<Pessoa>((pessoa) => pessoa.Endereco.Logradouro.Nome, new FailureModel("Teste1", "Teste1"));

            Result.Failure<Pessoa>((pessoa) => pessoa.Enderecos[1].Cidade, new FailureModel("Teste2", "Teste2"));

            for (int i = 0; i < 2; i++)
            {
                Result.Failure<Pessoa>((pessoa) => pessoa.Enderecos[i].Estado, new FailureModel("Estado", $"TesteList{i}"));
            }
        }

        if (pessoa.HasFailure())
        {
            return Result.Failure<CriarPessoasCommandHandler>(pessoa);
        }

        var pessoaCriada = await unitOfWork.PessoasRepository.CreateAsync(pessoa);

        return Result.SetContent(new PessoaCriadaModel()
        {
            Message = "Filé demais"
        });
    }
}
