using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Controllers;

[Route("api/v1/pessoas")]
[ApiController]
public class PessoaController : BaseController
{
    private readonly ICriarPessoaUseCase _criarPessoaUseCase;
    private readonly IRecuperarPessoasUseCase _recuperarPessoasUseCase;

    public PessoaController(ICriarPessoaUseCase criarPessoaUseCase,
        IRecuperarPessoasUseCase recuperarPessoasUseCase)
    {
        _criarPessoaUseCase = criarPessoaUseCase;
        _recuperarPessoasUseCase = recuperarPessoasUseCase;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = await _recuperarPessoasUseCase.ExecuteAsync();

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CriarPessoaModel criarPessoaModel)
    {
        var result = await _criarPessoaUseCase.ExecuteAsync(criarPessoaModel);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(result.Data);
    }
}
