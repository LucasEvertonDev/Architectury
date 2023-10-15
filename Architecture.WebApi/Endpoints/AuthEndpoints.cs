using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.UseCases.UseCases.AuthUseCases.Interfaces;
using Architecture.WebApi.Structure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Endpoints;

public static class AuthEndpoints
{ 
    public static IEndpointRouteBuilder AddAuthEndpoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var authEndpoints = app.MapGroup(route).WithTags(tag);

        authEndpoints.MapPost("login", [AllowAnonymous]
        async ([FromServices] ILoginUseCase loginUseCase, [AsParameters] LoginDto loginModel) =>
            {
                var result = await loginUseCase.ExecuteAsync(loginModel);

                return result.GetResponse<TokenModel>();

            }).Response<ResponseDto<TokenModel>>();


        authEndpoints.MapPost("refreshtoken",
            async ([FromServices] IRefreshTokenUseCase refreshTokenUseCase, [AsParameters] RefreshTokenDto refreshTokenDto) =>
            {
                var result = await refreshTokenUseCase.ExecuteAsync(refreshTokenDto);

                return result.GetResponse<TokenModel>();

            }).Authorization().Response<ResponseDto<TokenModel>>();


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

                return Results.Ok(new TokenFlowModel
                {
                    token_type = "bearer",
                    access_token = result.GetValue<TokenModel>().TokenJWT
                });

            }).AllowAnonymous().Response<TokenFlowModel>();

        return app;
    }
}

