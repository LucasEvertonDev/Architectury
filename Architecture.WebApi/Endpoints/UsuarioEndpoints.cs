using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.Mediator.Commands.Usuarios.AtualizarSenha;
using Architecture.Application.Mediator.Commands.Usuarios.AtualizarUsuario;
using Architecture.Application.Mediator.Commands.Usuarios.CriarUsuario;
using Architecture.Application.Mediator.Commands.Usuarios.ExcluirUsuario;
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
                {
                    var result = await mediator.Send(recuperarUsuariosQuery);

                    return result.GetResponse<PagedResult<UsuariosRecuperadosModel>>();

                }).Authorization().Response<ResponseDto<PagedResult<UsuariosRecuperadosModel>>>();

        usuariosEndpoint.MapPost("",
            async ([FromServices] IMediator mediator, [FromBody] CriarUsuarioCommand criarUsuarioCommand) =>
            {
                var result = await mediator.Send(criarUsuarioCommand);

                return result.GetResponse<UsuarioCriadoModel>();

            }).Authorization().Response<ResponseDto<UsuarioCriadoModel>>();

        usuariosEndpoint.MapPut("{id}",
            async ([FromServices] IMediator mediator, [AsParameters] AtualizarUsuarioCommand atualizarUsuarioCommand) =>
            {
                var result = await mediator.Send(atualizarUsuarioCommand);

                return result.GetResponse<AtualizarUsuarioModel>();

            }).Authorization().Response<ResponseDto<AtualizarUsuarioModel>>();

        usuariosEndpoint.MapPut("updatepassword/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] AtualizarSenhaCommand atualizarSenhaCommand) =>
            {
                var result = await mediator.Send(atualizarSenhaCommand);

                return result.GetResponse<ResponseDto>();

            }).Authorization().Response<ResponseDto>();

        usuariosEndpoint.MapDelete("{id}",
           async ([FromServices] IMediator mediator, [AsParameters] ExcluirUsuarioCommand excluirUsuarioCommand) =>
           {
               var result = await mediator.Send(excluirUsuarioCommand);

               return result.GetResponse<ResponseDto>();

           }).Authorization().Response<ResponseDto>();

        return app;
    }
}