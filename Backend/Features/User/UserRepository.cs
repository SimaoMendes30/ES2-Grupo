using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.User;
using Backend.Domain.Patterns.Specifications;
using Backend.Features.User.Interfaces;

namespace Backend.Features.User;

public sealed class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<SgscDbContext> _factory;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(IDbContextFactory<SgscDbContext> factory, ILogger<UserRepository> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task<UserEntity?> GetByIdAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Utilizador
            .Include(u => u.Membros)
            .Include(u => u.Projetos)
            .Include(u => u.Tarefas)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.IdUtilizador == id);
    }

    public async Task<UserEntity?> GetByUsernameAsync(string username)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Utilizador
            .Include(u => u.Membros)
            .Include(u => u.Projetos)
            .Include(u => u.Tarefas)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<UserEntity>> FilteredListAsync(UserFilterDto filter)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new UserByFilterSpec(filter);

        return await ctx.Utilizador
            .Where(spec.ToExpression())
            .Include(u => u.Membros)
            .Include(u => u.Projetos)
            .Include(u => u.Tarefas)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PagedResult<UserEntity>> FilteredPagedAsync(UserFilterDto filter, int page, int pageSize)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new UserByFilterSpec(filter);

        var query = ctx.Utilizador
            .Where(spec.ToExpression())
            .Include(u => u.Membros)
            .Include(u => u.Projetos)
            .Include(u => u.Tarefas)
            .AsNoTracking();

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<UserEntity>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async System.Threading.Tasks.Task AddAsync(UserEntity user)
    {
        Validate(user);

        await using var ctx = _factory.CreateDbContext();
        ctx.Utilizador.Add(user);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateAsync(UserEntity user)
    {
        Validate(user);

        await using var ctx = _factory.CreateDbContext();
        ctx.Utilizador.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        var entity = await ctx.Utilizador.FindAsync(id);
        if (entity is null) return;

        entity.IsDeleted = true;
        await ctx.SaveChangesAsync();
    }

    private void Validate(UserEntity user)
    {
        if (string.IsNullOrWhiteSpace(user.Nome))
            throw new ValidationException("O nome do utilizador é obrigatório.");

        if (string.IsNullOrWhiteSpace(user.Username))
            throw new ValidationException("O username é obrigatório.");

        if (string.IsNullOrWhiteSpace(user.Password))
            throw new ValidationException("A password é obrigatória.");
    }
}
