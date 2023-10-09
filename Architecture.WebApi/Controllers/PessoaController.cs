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
            await _recuperarPessoasUseCase.ExecuteAsync();

            return Ok(_recuperarPessoasUseCase.Retorno);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CriarPessoaModel criarPessoaModel)
        {
            await _criarPessoaUseCase.ExecuteAsync(criarPessoaModel);

            return Ok(_criarPessoaUseCase.Retorno);
        }
    }
}
