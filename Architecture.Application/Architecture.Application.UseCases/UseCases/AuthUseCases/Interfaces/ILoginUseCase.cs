using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Auth;

namespace Architecture.Application.UseCases.UseCases.AuthUseCases.Interfaces
{
    public interface ILoginUseCase
    {
        Task<Result> ExecuteAsync(LoginDto param);
    }
}