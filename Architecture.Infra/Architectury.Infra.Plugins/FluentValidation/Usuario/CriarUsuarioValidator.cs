using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architectury.Infra.Plugins.FluentValidation.Extensions;
using FluentValidation;

namespace Architectury.Infra.Plugins.FluentValidation.Usuario;

public class CriarUsuarioValidator : AbstractValidator<CriarUsuarioModel>
{
    public CriarUsuarioValidator(IRepository<GrupoUsuario> searchUserGroupRepository)
    {

        RuleFor(c => c.Username).NotNullOrEmpty().WithError(Erros.Usuario.UsernameObrigatorio);
        RuleFor(c => c.Email).NotNullOrEmpty().WithError(Erros.Usuario.EmailObrigatorio);

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithError(Erros.Usuario.EmailInvalido);
        });

        RuleFor(c => c.Password).NotNullOrEmpty().WithError(Erros.Usuario.PasswordObrigatorio);

        When(c => !string.IsNullOrWhiteSpace(c.Password), () =>
        {
            RuleFor(c => c.Password.Length).GreaterThanOrEqualTo(6).WithError(Erros.Usuario.PasswordLenght);
        });

        RuleFor(c => c.GrupoUsuarioId).NotNullOrEmpty().WithError(Erros.Usuario.GrupoUsuarioInvalido);

        RuleFor(x => x.GrupoUsuarioId).MustAsync(async (userGroup, cancelation) =>
        {
            if (!string.IsNullOrEmpty(userGroup))
            {
                return !(await searchUserGroupRepository.FirstOrDefaultAsync(u => u.Id.ToString() == userGroup) == null);
            }
            return true;
        }).WithError(Erros.Usuario.GrupoUsuarioInvalido);
    }
}
