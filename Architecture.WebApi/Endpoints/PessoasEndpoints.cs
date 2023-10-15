using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;
using Architecture.WebApi.Structure.Extensions;
using Architecture.WebApi.Structure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Endpoints;

public static class PessoasEndpoints
{
    public static IEndpointRouteBuilder AddPessoasEndpoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var pessoaEndpoints = app.MapGroup(route).AddEndpointFilter<ValidationFilter>().WithTags(tag).WithOpenApi();

        pessoaEndpoints.MapGet("",
            async ([FromServices] IRecuperarPessoasUseCase recuperarPessoasUseCase) =>
            {
                var result = await recuperarPessoasUseCase.ExecuteAsync();

                return result.GetResponse<IEnumerable<Pessoa>>();

            }).AllowAnonymous();


        pessoaEndpoints.MapPost("",
            async ([FromServices] ICriarPessoaUseCase criarPessoasUseCase, [FromBody] CriarPessoaModel criarPessoaModel) =>
            {
                var result = await criarPessoasUseCase.ExecuteAsync(criarPessoaModel);

                return result.GetResponse<PessoaCriadaModel>();

            }).AllowAnonymous();

        return app;
    }
}
