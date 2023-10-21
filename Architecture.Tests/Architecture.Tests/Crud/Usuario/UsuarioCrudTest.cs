using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.Mediator.Commands.Usuarios.CriarUsuario;
using Architecture.Application.Mediator.Commands.Usuarios.ExcluirUsuario;
using Architecture.Tests.Structure;
using Architecture.Tests.Structure.Attribute;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Tests.Crud.Usuario;

[TestCaseOrderer("Architecture.Tests.Structure.Filters.PriorityOrderer", "Architecture.Tests")]
public class UsuarioCrudTest : BaseTest
{
    private readonly IMediator _mediator;

    private static string _usuarioTesteId { get; set; }

    public UsuarioCrudTest()
    {
        _mediator = _serviceProvider.GetService<IMediator>();
    }

    [Fact, TestPriority(1)]
    public async Task ValidateSucess()
    {
        var result = await _mediator.Send<Result>(new CriarUsuarioCommand()
        {
            Email = "teste@teste.com" + new Random().Next(0, 10000),
            Password = "123456",
            Name = "Lucas Everton Santos de Oliveira",
            GrupoUsuarioId = "F97E565D-08AF-4281-BC11-C0206EAE06FA",
            Username = "lcseverton" + new Random().Next(0, 10000)
        });

        // Não pode haver falha
        result.HasFailures().Should().BeFalse();

        result.GetContent<UsuarioCriadoModel>().Id.Should().NotBeNull();

        _usuarioTesteId = result.GetContent<UsuarioCriadoModel>().Id;
    }

    [Fact, TestPriority(3)]
    public async Task ValidateUserAlreadyExists()
    {
        _usuarioTesteId.Should().NotBeNull();

        var result = await _mediator.Send<Result>(new ExcluirUsuarioCommand()
        {
            Id = _usuarioTesteId
        });

        result.HasFailures().Should().BeFalse();
    }

    [Fact, TestPriority(2)]
    public async Task ValidateDeleteUser()
    {
        var result = await _mediator.Send<Result>(new CriarUsuarioCommand()
        {
            Email = "lcseverton@gmail.com" + new Random().Next(0, 10000),
            Password = "123456",
            Name = "Lucas Everton Santos de Oliveira",
            GrupoUsuarioId = "F97E565D-08AF-4281-BC11-C0206EAE06FA",
            Username = "lcseverton"
        });

        result.HasFailures().Should().BeTrue();
    }
}
