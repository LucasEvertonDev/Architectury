using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.UseCases.IUseCases;

namespace Architecture.WebApi.Controllers;

[Route("api/v1/auth")]
public class AuthController : BaseController
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly ICreateUserService _createUserService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthController(ICreateUserService createUserService,
         ILoginService loginservice,
         IRefreshTokenService refreshTokenService)
    {
        _loginUseCase = loginservice;
        _createUserService = createUserService;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Login(LoginDto loginModel)
    {
        await _loginUseCase.ExecuteAsync(loginModel);

        return Ok(new ResponseDto<TokenModel>()
        {
            Content = _loginUseCase.TokenRetorno
        });
    }

    [HttpPost("refreshtoken"), Authorize]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        await _refreshTokenService.ExecuteAsync(refreshTokenDto);

        return Ok(new ResponseDto<TokenModel>()
        {
            Content = _refreshTokenService.TokenRetorno
        });
    }

    [HttpPost("flowlogin")]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> FlowLogin(LoginInfo loginInfo)
    {
        var authorization = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(loginInfo.Authorization.Split("Basic ")[1].ToString())).Split(":");
        await _loginUseCase.ExecuteAsync(new LoginDto
        {
            Body = new LoginModel
            {
                Username = loginInfo.username,
                Password = loginInfo.password,
            },
            ClientId = authorization[0],
            ClientSecret = authorization[1]
        });

        return Ok(new
        {
            token_type = "bearer",
            access_token = _loginUseCase.TokenRetorno.TokenJWT
        });
    }
}

