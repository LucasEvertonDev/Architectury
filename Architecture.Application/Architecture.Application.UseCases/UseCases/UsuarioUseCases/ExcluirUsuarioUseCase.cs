using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases;

public class ExcluirUsuarioUseCase : BaseUseCase<ExcluirUsuarioDto>, IExcluirUsuarioUseCase
{
    private readonly ISearchRepository<Usuario> _userSearchRepository;
    private readonly IDeleteRepository<Usuario> _deleteUserRepository;

    public ExcluirUsuarioUseCase(IServiceProvider serviceProvider,
        ISearchRepository<Usuario> userSearchRepository,
        IDeleteRepository<Usuario> deleteUserRepository
    ) : base(serviceProvider)
    {
        _userSearchRepository = userSearchRepository;
        _deleteUserRepository = deleteUserRepository;
    }

    public override async Task<Result> ExecuteAsync(ExcluirUsuarioDto param)
    {
        return await OnTransactionAsync(async () =>
        {
            var usuario = await _userSearchRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id);

            if (usuario == null)
            {
                return Result.Failure<ExcluirUsuarioUseCase>(Erros.Business.UsuarioInexistente);
            }

            await _deleteUserRepository.DeleteLogicAsync(usuario);

            return Result;
        });
    }
}
