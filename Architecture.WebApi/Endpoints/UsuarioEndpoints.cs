using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.Mediator.Commands.Usuarios.AtualizarSenha;
using Architecture.Application.Mediator.Commands.Usuarios.AtualizarUsuario;
using Architecture.Application.Mediator.Commands.Usuarios.CriarUsuario;
using Architecture.Application.Mediator.Commands.Usuarios.ExcluirUsuario;
using Architecture.Application.Mediator.Queries.Pessoas;
using Architecture.Application.Mediator.Queries.Usuarios.RecuperarUsuarios;
using Architecture.WebApi.Structure.Extensions;
using Architecture.WebApi.Structure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Endpoints;

public static class UsuariosEndpoints
{
    public static IEndpointRouteBuilder AddUsuariosEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var usuariosEndpoint = app.MapGroup(route).WithTags(tag);

        usuariosEndpoint.MapGet(Params.GetRoute<RecuperarUsuariosDto>(),
            async ([FromServices] IMediator mediator, [AsParameters] RecuperarUsuariosQuery recuperarUsuariosQuery) =>
                 await mediator.Send<Result>(recuperarUsuariosQuery).Result.GetResponse()
            )
            .Authorization().Response<ResponseDto<PagedResult<UsuariosRecuperadosModel>>>();

        usuariosEndpoint.MapPost("",
            async ([FromServices] IMediator mediator, [FromBody] CriarUsuarioCommand criarUsuarioCommand) =>
                await mediator.Send<Result>(criarUsuarioCommand).Result.GetResponse()
            )
            .AllowAnonymous().Response<ResponseDto<UsuarioCriadoModel>>();

        usuariosEndpoint.MapPut("{id}",
            async ([FromServices] IMediator mediator, [AsParameters] AtualizarUsuarioCommand atualizarUsuarioCommand) =>
                await mediator.Send<Result>(atualizarUsuarioCommand).Result.GetResponse()
            )
            .Authorization().Response<ResponseDto<AtualizarUsuarioModel>>();

        usuariosEndpoint.MapPut("updatepassword/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] AtualizarSenhaCommand atualizarSenhaCommand) =>
                await mediator.Send<Result>(atualizarSenhaCommand).Result.GetResponse()
            )
            .Authorization().Response<ResponseDto>();

        usuariosEndpoint.MapDelete("{id}",
           async ([FromServices] IMediator mediator, [AsParameters] ExcluirUsuarioCommand excluirUsuarioCommand) =>
               await mediator.Send<Result>(excluirUsuarioCommand).Result.GetResponse()
            )
            .Authorization().Response<ResponseDto>();

        return app;
    }
}