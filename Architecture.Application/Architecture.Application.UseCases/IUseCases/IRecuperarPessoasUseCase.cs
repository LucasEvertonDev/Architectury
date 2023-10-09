using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.DbContexts.Domains;

namespace Architecture.Application.UseCases.IUseCases;

public interface IRecuperarPessoasUseCase
{
    IEnumerable<Pessoa> Retorno { get; set; }

    Task ExecuteAsync();
}