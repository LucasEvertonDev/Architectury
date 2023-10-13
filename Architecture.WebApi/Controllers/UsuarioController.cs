using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;
using Architecture.WebApi.Structure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Controllers;

[Route("api/v1/usuarios")]
public class UsuarioController : BaseController
{
    private readonly ICriarUsuarioUseCase _createUserService;
    private readonly IAtualizarUsuarioUseCase _updateUserService;
    private readonly IExcluirUsuarioUseCase _deleteUserService;
    private readonly IBuscarUsuariosUseCase _searchServices;
    private readonly IAtualizarSenhaUseCase _updatePasswodService;

    public UsuarioController(ICriarUsuarioUseCase createUserService,
        IAtualizarUsuarioUseCase updateUserService,
        IExcluirUsuarioUseCase deleteUserService,
        IBuscarUsuariosUseCase searchServices,
        IAtualizarSenhaUseCase updatePasswordService)
    {
        _createUserService = createUserService;
        _updateUserService = updateUserService;
        _deleteUserService = deleteUserService;
        _searchServices = searchServices;
        _updatePasswodService = updatePasswordService;
    }

    [HttpGetParams<RecuperarUsuariosDto>, Authorize]
    [ProducesResponseType(typeof(ResponseDto<PagedResult<UsuariosRecuperadosModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(RecuperarUsuariosDto seacrhUserDto)
    {
        var result = await _searchServices.ExecuteAsync(seacrhUserDto);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new ResponseDto<PagedResult<UsuariosRecuperadosModel>>()
        {
            Content = result.GetValue<PagedResult<UsuariosRecuperadosModel>>()
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto<UsuarioCriadoModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post([FromBody] CriarUsuarioModel createUserModel)
    {
        var result = await _createUserService.ExecuteAsync(createUserModel);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new ResponseDto<UsuarioCriadoModel>()
        {
            Content = result.GetValue<UsuarioCriadoModel>()
        });
    }

    [HttpPut("{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto<AtualizarUsuarioDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Put(AtualizarUsuarioDto updateUserModel)
    {
        var result = await _updateUserService.ExecuteAsync(updateUserModel);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new ResponseDto<AtualizarUsuarioModel>()
        {
            Content = result.GetValue<AtualizarUsuarioModel>()
        });
    }

    [HttpPut("updatepassword/{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdatePassword(AtualizarSenhaUsuarioDto passwordDto)
    {
        var result = await _updatePasswodService.ExecuteAsync(passwordDto);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new ResponseDto()
        {
            Success = true
        });
    }

    [HttpDelete("{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(ExcluirUsuarioDto deleteUserDto)
    {
        var result = await _deleteUserService.ExecuteAsync(deleteUserDto);

        if (result.HasFailures())
        {
            return BadRequestFailure(result);
        }

        return Ok(new ResponseDto()
        {
            Success = true
        });
    }
}
