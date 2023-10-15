using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.UseCases.UseCases.AuthUseCases.Interfaces;
using Architecture.WebApi.Structure.Extensions;
using Architecture.WebApi.Structure.Filters;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Endpoints;

public static class AuthEndpoints
{ 
    public static IEndpointRouteBuilder AddAuthEndpoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var authEndpoints = app.MapGroup(route).AddEndpointFilter<ValidationFilter>().WithTags(tag).WithOpenApi();

        authEndpoints.MapPost("login", [AllowAnonymous]
        async ([FromServices] ILoginUseCase loginUseCase, [AsParameters] LoginDto loginModel) =>
            {
                var result = await loginUseCase.ExecuteAsync(loginModel);

                return result.GetResponse<TokenModel>();

            }).Produces<ResponseDto<ErrorModel>>(StatusCodes.Status400BadRequest).Produces<ResponseDto<TokenModel>>(StatusCodes.Status200OK);


        authEndpoints.MapPost("refreshtoken",
            async ([FromServices] IRefreshTokenUseCase refreshTokenUseCase, [AsParameters] RefreshTokenDto refreshTokenDto) =>
            {
                var result = await refreshTokenUseCase.ExecuteAsync(refreshTokenDto);

                return result.GetResponse<TokenModel>();

            }).RequireAuthorization(); // passa o nome da policy que criou ou instancia para validar função


        authEndpoints.MapPost("flowlogin",
            async ([FromServices] ILoginUseCase loginUseCase, HttpRequest request) =>
            {
                var form = await request.ReadFormAsync();

                var authorization = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(request.Headers["Authorization"].ToString().Split("Basic ")[1].ToString())).Split(":");
                var result = await loginUseCase.ExecuteAsync(new LoginDto
                {
                    Body = new LoginModel { Password = form["password"], Username = form["username"] },
                    ClientId = authorization[0],
                    ClientSecret = authorization[1]
                });

                if (result.HasFailures())
                {
                    return result.BadRequestFailure();
                }

                return Results.Ok(new
                {
                    token_type = "bearer",
                    access_token = result.GetValue<TokenModel>().TokenJWT
                });

            }).AllowAnonymous();

        return app;
    }
}

