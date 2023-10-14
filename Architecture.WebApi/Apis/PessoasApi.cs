using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;
using Architecture.WebApi.Structure.Extensions;
using Architecture.WebApi.Structure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Apis;

public static class PessoasApi
{
    public static void AddPessoasApi(this IEndpointRouteBuilder app)
    {
        var pessoasApi = app.MapGroup("api/v1/pessoas/").AddEndpointFilter<ValidationFilter>().WithTags("Pessoas").WithOpenApi();

        pessoasApi.MapGet("", 
            async ([FromServices] IRecuperarPessoasUseCase recuperarPessoasUseCase) =>
            {
                var result = await recuperarPessoasUseCase.ExecuteAsync();

                if (result.HasFailures())
                {
                    return result.BadRequestFailure();
                }
                    
                return Results.Ok(result.GetValue<IEnumerable<Pessoa>>());

            }).AllowAnonymous();


        pessoasApi.MapPost("", 
            async ([FromServices] ICriarPessoaUseCase criarPessoasUseCase, [FromBody] CriarPessoaModel criarPessoaModel) =>
            {
                var result = await criarPessoasUseCase.ExecuteAsync(criarPessoaModel);

                if (result.HasFailures())
                {
                    return result.BadRequestFailure();
                }
 
                return Results.Ok(result.GetValue<PessoaCriadaModel>());

            }).AllowAnonymous();
    }
}
