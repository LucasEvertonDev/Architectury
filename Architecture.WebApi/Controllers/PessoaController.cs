using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Controllers
{
    [Route("api/v1/pessoas")]
    [ApiController]
    public class PessoaController : Controller
    {
        private readonly ICriarPessoaUseCase _criarPessoaUseCase;

        public PessoaController(ICriarPessoaUseCase criarPessoaUseCase)
        {
            _criarPessoaUseCase = criarPessoaUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CriarPessoaModel criarPessoaModel)
        {
            return Ok(await _criarPessoaUseCase.ExecuteAsync(criarPessoaModel));
        }
    }
}
