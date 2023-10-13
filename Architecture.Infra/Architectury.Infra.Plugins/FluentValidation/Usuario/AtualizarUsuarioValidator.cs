using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architectury.Infra.Plugins.FluentValidation.Extensions;
using FluentValidation;

namespace PLaboratory.Plugins.FluentValidation.User;

public class AtualizarUsuarioValidator : AbstractValidator<AtualizarUsuarioDto>
{
    public AtualizarUsuarioValidator(ISearchRepository<GrupoUsuario> searchUserGroupRepository)
    {
        RuleFor(c => c.Body.Username).NotNullOrEmpty().WithError(Erros.Usuario.UsernameObrigatorio);
        RuleFor(c => c.Body.Email).NotNullOrEmpty().WithError(Erros.Usuario.EmailObrigatorio);

        When(c => !string.IsNullOrWhiteSpace(c.Body.Email), () =>
        {
            RuleFor(c => c.Body.Email).EmailAddress().WithError(Erros.Usuario.EmailInvalido);
        });

        RuleFor(c => c.Body.GrupoUsuarioId).NotNullOrEmpty().WithError(Erros.Usuario.GrupoUsuarioInvalido);

        RuleFor(x => x.Body.GrupoUsuarioId).MustAsync(async (userGroup, cancelation) =>
        {
            if (!string.IsNullOrEmpty(userGroup))
            {
                return !((await searchUserGroupRepository.FirstOrDefaultAsync(u => u.Id.ToString() == userGroup)) == null);
            }
            return true;
        }).WithError(Erros.Usuario.GrupoUsuarioInvalido);
    }
}
