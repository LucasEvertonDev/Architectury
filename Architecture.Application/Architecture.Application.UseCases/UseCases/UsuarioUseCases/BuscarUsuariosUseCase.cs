using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Usuarios;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;

namespace Architecture.Application.UseCases.UseCases.UsuarioUseCases;

public class BuscarUsuariosUseCase : BaseUseCase<RecuperarUsuariosDto>, IBuscarUsuariosUseCase
{

    public BuscarUsuariosUseCase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    public override async Task<Result> ExecuteAsync(RecuperarUsuariosDto param)
    {
        return await OnTransactionAsync(async (transaction) =>
        {
            var pagedResult = await transaction.GetRepository<Usuario>()
                .ToListAsync(
                    pageNumber: param.PageNumber,
                    pageSize: param.PageSize,
                    predicate: u => string.IsNullOrEmpty(param.Nome) || u.Nome.Contains(param.Nome)
                );

            return Result.IncludeResult(new UsuariosRecuperadosModel().FromEntity(pagedResult));
        });
    }
}
