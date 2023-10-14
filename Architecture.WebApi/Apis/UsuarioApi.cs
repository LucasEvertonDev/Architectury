using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;
using Architecture.WebApi.Structure.Attributes;
using Architecture.WebApi.Structure.Extensions;
using Architecture.WebApi.Structure.Filters;
using Architecture.WebApi.Structure.Helpers;
using Microsoft.AspNetCore.Builder;
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

                    if (result.HasFailures())
                    {
                        return result.BadRequestFailure();
                    }

                    return Results.Ok(new ResponseDto<PagedResult<UsuariosRecuperadosModel>>()
                    {
                        Content = result.GetValue<PagedResult<UsuariosRecuperadosModel>>()
                    });

                }).RequireAuthorization();

        usuariosApi.MapPost("", 
            async ([FromServices] ICriarUsuarioUseCase criarUsuarioUseCase, [FromBody] CriarUsuarioModel createUserModel) =>
            {
                var result = await criarUsuarioUseCase.ExecuteAsync(createUserModel);

                if (result.HasFailures())
                {
                    return result.BadRequestFailure();
                }

                return Results.Ok(new ResponseDto<UsuarioCriadoModel>()
                {
                    Content = result.GetValue<UsuarioCriadoModel>()
                });

            }).RequireAuthorization();

        usuariosApi.MapPut("{id}",
            async ([FromServices] IAtualizarUsuarioUseCase atualizarUsuarioUseCase, [AsParameters] AtualizarUsuarioDto updateUserModel) =>
            {
                var result = await atualizarUsuarioUseCase.ExecuteAsync(updateUserModel);

                if (result.HasFailures())
                {
                    return result.BadRequestFailure();
                }

                return Results.Ok(new ResponseDto<AtualizarUsuarioModel>()
                {
                    Content = result.GetValue<AtualizarUsuarioModel>()
                });

            }).RequireAuthorization();

        usuariosApi.MapPut("updatepassword/{id}",
            async ([FromServices] IAtualizarSenhaUseCase atualizarSenhaUseCase, [AsParameters] AtualizarSenhaUsuarioDto atualizarSenhaUsuarioDto) =>
            {
                var result = await atualizarSenhaUseCase.ExecuteAsync(atualizarSenhaUsuarioDto);

                if (result.HasFailures())
                {
                    return result.BadRequestFailure();
                }

                return Results.Ok(new ResponseDto()
                {
                    Success = true
                });

            }).RequireAuthorization();

        usuariosApi.MapDelete("{id}",
           async ([FromServices] IExcluirUsuarioUseCase excluirUsuarioUseCase, [AsParameters] ExcluirUsuarioDto excluirUsuarioDto) =>
           {
               var result = await excluirUsuarioUseCase.ExecuteAsync(excluirUsuarioDto);

               if (result.HasFailures())
               {
                   return result.BadRequestFailure();
               }

               return Results.Ok(new ResponseDto()
               {
                   Success = true
               });

           }).RequireAuthorization();
    }
}