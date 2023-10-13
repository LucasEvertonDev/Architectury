using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Usuarios;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases
{
    public interface ICriarUsuarioUseCase
    {
        Task<Result> ExecuteAsync(CriarUsuarioModel param);
    }
}