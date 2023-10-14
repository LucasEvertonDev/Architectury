using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;
using Architecture.Tests.Structure;
using Architecture.Tests.Structure.Attribute;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Tests.UseCases.Usuario;

[TestCaseOrderer("Architecture.Tests.Structure.Filters.PriorityOrderer", "Architecture.Tests")]
public class UsuarioUseCaseTest : BaseTest
{
    private readonly ICriarUsuarioUseCase _criarUsuarioUseCase;
    private readonly IExcluirUsuarioUseCase _excluirUsuarioUseCase;

    private static string _usuarioTesteId { get; set; }

    public UsuarioUseCaseTest()
    {
        _criarUsuarioUseCase = _serviceProvider.GetService<ICriarUsuarioUseCase>();

        _excluirUsuarioUseCase = _serviceProvider.GetService<IExcluirUsuarioUseCase>();
    }

    [Fact, TestPriority(1)]
    public async Task ValidateSucess()
    {
        var result = await _criarUsuarioUseCase.ExecuteAsync(new CriarUsuarioModel()
        {
            Email = "teste@teste.com" + new Random().Next(0, 10000),
            Password = "123456",
            Name = "Lucas Everton Santos de Oliveira",
            GrupoUsuarioId = "F97E565D-08AF-4281-BC11-C0206EAE06FA",
            Username = "lcseverton" + new Random().Next(0, 10000)
        });

        // Não pode haver falha
        result.HasFailures().Should().BeFalse();

        result.GetValue<UsuarioCriadoModel>().Id.Should().NotBeNull();

        _usuarioTesteId = result.GetValue<UsuarioCriadoModel>().Id;
    }

    [Fact, TestPriority(3)]
    public async Task ValidateUserAlreadyExists()
    {
        _usuarioTesteId.Should().NotBeNull();

        var result = await _excluirUsuarioUseCase.ExecuteAsync(new ExcluirUsuarioDto()
        {
            Id = _usuarioTesteId
        });

        result.HasFailures().Should().BeFalse();
    }

    [Fact, TestPriority(2)]
    public async Task ValidateDeleteUser()
    {
        var result = await _criarUsuarioUseCase.ExecuteAsync(new CriarUsuarioModel()
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
