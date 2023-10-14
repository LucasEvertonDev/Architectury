using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;
using Architecture.WebApi.Structure.Extensions;
using Architecture.WebApi.Structure.Filters;
using Architecture.WebApi.Structure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Apis;

public static class UsuariosApi
{
    public static void AddUsuariosApi(this IEndpointRouteBuilder app)
    {
        var usuariosApi = app.MapGroup("api/v1/usuarios/").AddEndpointFilter<ValidationFilter>().WithTags("Usuarios").WithOpenApi();

        usuariosApi.MapGet(Params.GetRoute<RecuperarUsuariosDto>(),
            async ([FromServices] IBuscarUsuariosUseCase buscarUsuariosUseCase, [AsParameters] RecuperarUsuariosDto recuperarUsuariosDto) =>
                {
                    var result = await buscarUsuariosUseCase.ExecuteAsync(recuperarUsuariosDto);

                    return result.GetResponse<PagedResult<UsuariosRecuperadosModel>>();

                }).RequireAuthorization();

        usuariosApi.MapPost("", 
            async ([FromServices] ICriarUsuarioUseCase criarUsuarioUseCase, [FromBody] CriarUsuarioModel createUserModel) =>
            {
                var result = await criarUsuarioUseCase.ExecuteAsync(createUserModel);

                return result.GetResponse<UsuarioCriadoModel>();

            }).RequireAuthorization();

        usuariosApi.MapPut("{id}",
            async ([FromServices] IAtualizarUsuarioUseCase atualizarUsuarioUseCase, [AsParameters] AtualizarUsuarioDto updateUserModel) =>
            {
                var result = await atualizarUsuarioUseCase.ExecuteAsync(updateUserModel);

                return result.GetResponse<AtualizarUsuarioModel>();

            }).RequireAuthorization();

        usuariosApi.MapPut("updatepassword/{id}",
            async ([FromServices] IAtualizarSenhaUseCase atualizarSenhaUseCase, [AsParameters] AtualizarSenhaUsuarioDto atualizarSenhaUsuarioDto) =>
            {
                var result = await atualizarSenhaUseCase.ExecuteAsync(atualizarSenhaUsuarioDto);

                return result.GetResponse<ResponseDto>();

            }).RequireAuthorization();

        usuariosApi.MapDelete("{id}",
           async ([FromServices] IExcluirUsuarioUseCase excluirUsuarioUseCase, [AsParameters] ExcluirUsuarioDto excluirUsuarioDto) =>
           {
               var result = await excluirUsuarioUseCase.ExecuteAsync(excluirUsuarioDto);

               return result.GetResponse<ResponseDto>();

           }).RequireAuthorization();
    }
}