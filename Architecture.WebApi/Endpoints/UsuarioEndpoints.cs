using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;
using Architecture.WebApi.Structure.Extensions;
using Architecture.WebApi.Structure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Endpoints;

public static class UsuariosEndpoints
{
    public static IEndpointRouteBuilder AddUsuariosEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var usuariosEndpoint = app.MapGroup(route).WithTags(tag);

        usuariosEndpoint.MapGet(Params.GetRoute<RecuperarUsuariosDto>(),
            async ([FromServices] IBuscarUsuariosUseCase buscarUsuariosUseCase, [AsParameters] RecuperarUsuariosDto recuperarUsuariosDto) =>
                {
                    var result = await buscarUsuariosUseCase.ExecuteAsync(recuperarUsuariosDto);

                    return result.GetResponse<PagedResult<UsuariosRecuperadosModel>>();

                }).Authorization().Response<ResponseDto<PagedResult<UsuariosRecuperadosModel>>>();

        usuariosEndpoint.MapPost("",
            async ([FromServices] ICriarUsuarioUseCase criarUsuarioUseCase, [FromBody] CriarUsuarioModel createUserModel) =>
            {
                var result = await criarUsuarioUseCase.ExecuteAsync(createUserModel);

                return result.GetResponse<UsuarioCriadoModel>();

            }).Authorization().Response<ResponseDto<UsuarioCriadoModel>>();

        usuariosEndpoint.MapPut("{id}",
            async ([FromServices] IAtualizarUsuarioUseCase atualizarUsuarioUseCase, [AsParameters] AtualizarUsuarioDto updateUserModel) =>
            {
                var result = await atualizarUsuarioUseCase.ExecuteAsync(updateUserModel);

                return result.GetResponse<AtualizarUsuarioModel>();

            }).Authorization().Response<ResponseDto<AtualizarUsuarioModel>>();

        usuariosEndpoint.MapPut("updatepassword/{id}",
            async ([FromServices] IAtualizarSenhaUseCase atualizarSenhaUseCase, [AsParameters] AtualizarSenhaUsuarioDto atualizarSenhaUsuarioDto) =>
            {
                var result = await atualizarSenhaUseCase.ExecuteAsync(atualizarSenhaUsuarioDto);

                return result.GetResponse<ResponseDto>();

            }).Authorization().Response<ResponseDto>();

        usuariosEndpoint.MapDelete("{id}",
           async ([FromServices] IExcluirUsuarioUseCase excluirUsuarioUseCase, [AsParameters] ExcluirUsuarioDto excluirUsuarioDto) =>
           {
               var result = await excluirUsuarioUseCase.ExecuteAsync(excluirUsuarioDto);

               return result.GetResponse<ResponseDto>();

           }).Authorization().Response<ResponseDto>();

        return app;
    }
}