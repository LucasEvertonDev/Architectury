using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases;

public class BuscarUsuariosUseCase : BaseUseCase<RecuperarUsuariosDto>, IBuscarUsuariosUseCase
{
    private readonly ISearchRepository<Usuario> _searchUserRepository;

    public BuscarUsuariosUseCase(IServiceProvider serviceProvider,
        ISearchRepository<Usuario> searchUserRepository) : base(serviceProvider)
    {
        _searchUserRepository = searchUserRepository;
    }
    public override async Task<Result> ExecuteAsync(RecuperarUsuariosDto param)
    {
        return await OnTransactionAsync(async () =>
        {
            var pagedResult = await _searchUserRepository.ToListAsync(
                pageNumber: param.PageNumber,
                pageSize: param.PageSize,
                predicate: u => string.IsNullOrEmpty(param.Nome) || u.Nome.Contains(param.Nome)
            );

            return Result.IncludeResult(pagedResult);
        });
    }
}
