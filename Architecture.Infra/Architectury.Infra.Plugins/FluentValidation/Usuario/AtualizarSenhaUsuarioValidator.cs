using Architecture.Application.Domain.Constants;
using Architecture.Application.Mediator.Commands.Usuarios.AtualizarSenha;
using Architectury.Infra.Plugins.FluentValidation.Structure.Extensions;
using FluentValidation;

namespace Architectury.Infra.Plugins.FluentValidation.Usuario;

public class AtualizarSenhaUsuarioValidator : AbstractValidator<AtualizarSenhaCommand>
{
    public AtualizarSenhaUsuarioValidator()
    {
        RuleFor(c => c.Body.Password).NotNullOrEmpty().WithError(Erros.Usuario.PasswordObrigatorio);

        When(c => !string.IsNullOrWhiteSpace(c.Body.Password), () =>
        {
            RuleFor(c => c.Body.Password.Length).GreaterThanOrEqualTo(6).WithError(Erros.Usuario.PasswordLenght);
        });
    }
}
