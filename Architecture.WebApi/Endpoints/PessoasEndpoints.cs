﻿using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.Mediator.Commands.Pessoas.CriarPessoa;
using Architecture.Application.Mediator.Queries.Pessoas;
using Architecture.WebApi.Structure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Endpoints;

public static class PessoasEndpoints
{
    public static IEndpointRouteBuilder AddPessoasEndpoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var pessoaEndpoints = app.MapGroup(route).WithTags(tag);

        pessoaEndpoints.MapGet("",
            async ([FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(new RecuperarPessoasQuery());

                return result.GetResponse<IEnumerable<Pessoa>>();

            }).AllowAnonymous().Response<IEnumerable<Pessoa>>();


        pessoaEndpoints.MapPost("",
            async ([FromServices] IMediator mediator, [FromBody] CriarPessoasCommand criarPessoasCommand) =>
            {
                var result = await mediator.Send(criarPessoasCommand);

                return result.GetResponse<PessoaCriadaModel>();

            }).AllowAnonymous().Response<PessoaCriadaModel>();

        return app;
    }
}
