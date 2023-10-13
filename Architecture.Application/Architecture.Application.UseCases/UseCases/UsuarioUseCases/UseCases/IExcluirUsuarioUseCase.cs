using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Usuarios;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases
{
    public interface IExcluirUsuarioUseCase
    {
        Task<Result> ExecuteAsync(ExcluirUsuarioDto param);
    }
}