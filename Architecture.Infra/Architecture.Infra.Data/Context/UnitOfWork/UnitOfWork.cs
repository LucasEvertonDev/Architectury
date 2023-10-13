﻿using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.DbContexts.Repositorys.MapUserGroupRolesRepository;
using Architecture.Application.Domain.DbContexts.Repositorys.PessoaRepository;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Architecture.Infra.Data.Context.Repositories;
using Architecture.Infra.Data.Context.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infra.Data.Context.UnitOfWork;

public class UnitOfWork<TDbContext> : IUnitWorkTransaction where TDbContext : DbContext
{
    public readonly TDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private IRepository<Usuario> _usuarioRepository;
    private IRepository<Permissao> _permissaoRepository;
    private IPessoaRepository _pessoaRepository;
    private IRepository<CredenciaisCliente> _credenciaisClienteRepository;
    private IMapPermissoesPorGrupoUsuarioRepository _mapPermissoesPorGrupoUsuarioRepository;
    private IRepository<GrupoUsuario> _grupoUsuarioRepository;

    public UnitOfWork(TDbContext applicationDbContext,
            IServiceProvider serviceProvider
        )
    {
        _context = applicationDbContext;
        _serviceProvider = serviceProvider;
    }


    public IRepository<Usuario> UsuarioRepository
    { 
        get
        {
            if (_usuarioRepository == null)
            {
                _usuarioRepository = new Repository<Usuario>(_serviceProvider);
            }
            return _usuarioRepository;
        } 
    }

    public IRepository<Permissao> PermissaoRepository
    {
        get
        {
            if (_permissaoRepository == null)
            {
                _permissaoRepository = new Repository<Permissao>(_serviceProvider);
            }
            return _permissaoRepository;
        }
    }

    public IPessoaRepository PessoasRepository
    {
        get
        {
            if (_pessoaRepository == null)
            {
                _pessoaRepository = new PessoaRepository(_serviceProvider);
            }
            return _pessoaRepository;
        }
    }

    public IRepository<CredenciaisCliente> CredenciaisClientesRepository
    {
        get
        {
            if (_credenciaisClienteRepository == null)
            {
                _credenciaisClienteRepository = new Repository<CredenciaisCliente>(_serviceProvider);
            }
            return _credenciaisClienteRepository;
        }
    }

    public IMapPermissoesPorGrupoUsuarioRepository MapPermissoesPorGrupoUsuarioRepository
    {
        get
        {
            if (_mapPermissoesPorGrupoUsuarioRepository == null)
            {
                _mapPermissoesPorGrupoUsuarioRepository = new MapPermissoesPorGrupoUsuarioRepository(_serviceProvider);
            }
            return _mapPermissoesPorGrupoUsuarioRepository;
        }
    }

    public IRepository<GrupoUsuario> GrupoUsuarioRepository
    {
        get
        {
            if (_grupoUsuarioRepository == null)
            {
                _grupoUsuarioRepository = new Repository<GrupoUsuario>(_serviceProvider);
            }
            return _grupoUsuarioRepository;
        }
    }

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
