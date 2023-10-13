using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Context.UnitOfWork;

public class UnitOfWork<TDbContext> : IUnitWorkTransaction where TDbContext : DbContext
{
    public readonly TDbContext _context;
    private readonly IRepository<Usuario> _usuarioRepository;
    private readonly IRepository<Permissao> _permissaoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IRepository<CredenciaisCliente> _credenciaisClienteRepository;
    private readonly IMapPermissoesPorGrupoUsuarioRepository _mapPermissoesPorGrupoUsuarioRepository;
    private readonly IRepository<GrupoUsuario> _grupoUsuarioRepository;

    public UnitOfWork(TDbContext applicationDbContext,
            IRepository<Usuario> usuarioRepository,
            IRepository<Permissao> permissaoRepository,
            IPessoaRepository pessoaRepository,
            IRepository<CredenciaisCliente> credenciaisClienteRepository,
            IMapPermissoesPorGrupoUsuarioRepository mapPermissoesPorGrupoUsuarioRepository,
            IRepository<GrupoUsuario> grupoUsuarioRepository
        )
    {
        _context = applicationDbContext;
        _usuarioRepository = usuarioRepository;
        _permissaoRepository = permissaoRepository;
        _pessoaRepository = pessoaRepository;
        _credenciaisClienteRepository = credenciaisClienteRepository;
        _mapPermissoesPorGrupoUsuarioRepository = mapPermissoesPorGrupoUsuarioRepository;
        _grupoUsuarioRepository = grupoUsuarioRepository;
    }

    public IRepository<Usuario> UsuarioRepository => _usuarioRepository;

    public IRepository<Permissao> PermissaoRepository => _permissaoRepository;

    public IPessoaRepository PessoasRepository => _pessoaRepository;

    public IRepository<CredenciaisCliente> CredenciaisClientesRepository => _credenciaisClienteRepository;

    public IMapPermissoesPorGrupoUsuarioRepository MapPermissoesPorGrupoUsuarioRepository => _mapPermissoesPorGrupoUsuarioRepository;

    public IRepository<GrupoUsuario> GrupoUsuarioRepository => _grupoUsuarioRepository;

    public async Task OpenConnectionAsync(Func<Task> func)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await func();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<TRetorno> OpenConnectionAsync<TRetorno>(Func<Task<TRetorno>> func)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            TRetorno retorno = await func();

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return retorno;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
