namespace Architecture.Application.Domain.DbContexts.UnitOfWork;

public interface IUnitOfWorkRepos
{
    Repositorys.Base.IRepository<Domains.Usuario> UsuarioRepository { get; }
    Repositorys.Base.IRepository<Domains.Permissao> PermissaoRepository { get; }
    Repositorys.PessoaRepository.IPessoaRepository PessoasRepository { get; }
    Repositorys.Base.IRepository<Domains.CredenciaisCliente> CredenciaisClientesRepository { get; }
    Repositorys.MapUserGroupRolesRepository.IMapPermissoesPorGrupoUsuarioRepository MapPermissoesPorGrupoUsuarioRepository { get; }
    Repositorys.Base.IRepository<Domains.GrupoUsuario> GrupoUsuarioRepository { get; }
}
