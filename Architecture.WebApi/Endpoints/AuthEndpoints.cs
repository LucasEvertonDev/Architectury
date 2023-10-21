using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Auth;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Mediator.Commands.Auth.Login;
using Architecture.Application.Mediator.Commands.Auth.RefreshToken;
using Architecture.WebApi.Structure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Endpoints;

public static class AuthEndpoints
{ 
    public static IEndpointRouteBuilder AddAuthEndpoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var authEndpoints = app.MapGroup(route).WithTags(tag);

        authEndpoints.MapPost("login", [AllowAnonymous]
            async ([FromServices] IMediator mediator, [AsParameters] LoginCommand loginCommand) =>
                await mediator.Send<Result>(loginCommand).Result.GetResponse<TokenModel>()
            )
            .Response<ResponseDto<TokenModel>>();

        authEndpoints.MapPost("refreshtoken",
            async ([FromServices] IMediator mediator, [AsParameters] RefreshTokenCommand refreshTokenCommand) =>
                await mediator.Send<Result>(refreshTokenCommand).Result.GetResponse<TokenModel>()
            )
            .Authorization().Response<ResponseDto<TokenModel>>();


        authEndpoints.MapPost("flowlogin",
            async ([FromServices] IMediator mediator, HttpRequest request) =>
            {
                var form = await request.ReadFormAsync();

                var authorization = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(request.Headers["Authorization"].ToString().Split("Basic ")[1].ToString())).Split(":");

                var result = await mediator.Send(new LoginCommand
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

