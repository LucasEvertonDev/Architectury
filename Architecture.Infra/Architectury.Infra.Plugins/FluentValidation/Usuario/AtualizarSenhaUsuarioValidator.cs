using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.Models.Usuarios;
using Architectury.Infra.Plugins.FluentValidation.Extensions;
using FluentValidation;
using static Architecture.Application.Domain.Constants.Erros;

namespace PLaboratory.Plugins.FluentValidation.User;

public class AtualizarSenhaUsuarioValidator : AbstractValidator<AtualizarSenhaUsuarioDto>
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
