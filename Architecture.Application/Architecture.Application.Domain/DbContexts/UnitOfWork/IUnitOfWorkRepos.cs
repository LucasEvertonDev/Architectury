using Architecture.Application.Domain.DbContexts.Repositories.Base;
using Architecture.Application.Domain.DbContexts.Repositories.MapUserGroupRolesRepository;
using Architecture.Application.Domain.DbContexts.Repositories.PessoaRepository;

namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<Domains.Usuario> UsuarioRepository { get; }
    IRepository<Domains.Permissao> PermissaoRepository { get; }
    IPessoaRepository PessoasRepository { get; }
    IRepository<Domains.CredenciaisCliente> CredenciaisClientesRepository { get; }
    IMapPermissoesPorGrupoUsuarioRepository MapPermissoesPorGrupoUsuarioRepository { get; }
    IRepository<Domains.GrupoUsuario> GrupoUsuarioRepository { get; }
}
