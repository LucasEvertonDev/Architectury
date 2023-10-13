using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.UseCases.UseCases.AuthUseCases.Interfaces;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;
using Architecture.Application.Domain.Models.Auth;

namespace Architecture.WebApi.Controllers;

[Route("api/v1/auth")]
public class AuthController : BaseController
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly ICriarUsuarioUseCase _createUserService;
    private readonly IRefreshTokenUseCase _refreshTokenService;

    public AuthController(ICriarUsuarioUseCase createUserService,
         ILoginUseCase loginservice,
         IRefreshTokenUseCase refreshTokenService)
    {
        _loginUseCase = loginservice;
        _createUserService = createUserService;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Login(LoginDto loginModel)
    {
        var result = await _loginUseCase.ExecuteAsync(loginModel);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new ResponseDto<TokenModel>()
        {
            Content = result.Data
        });
    }

    [HttpPost("refreshtoken"), Authorize]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var result = await _refreshTokenService.ExecuteAsync(refreshTokenDto);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new ResponseDto<TokenModel>()
        {
            Content = result.Data
        });
    }

    [HttpPost("flowlogin")]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> FlowLogin(LoginInfo loginInfo)
    {
        var authorization = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(loginInfo.Authorization.Split("Basic ")[1].ToString())).Split(":");
        var result =  await _loginUseCase.ExecuteAsync(new LoginDto
        {
            Body = new LoginModel
            {
                Username = loginInfo.username,
                Password = loginInfo.password,
            },
            ClientId = authorization[0],
            ClientSecret = authorization[1]
        });

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new
        {
            token_type = "bearer",
            access_token = result.Data.TokenJWT
        });
    }
}

