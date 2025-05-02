namespace Backend.Repositories;

using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public sealed class UtilizadorRepository : IUtilizadorRepository
{
    private readonly IDbContextFactory<sgscDbContext> _contextFactory;
    private readonly ILogger<UtilizadorRepository> _logger;

    public UtilizadorRepository(IDbContextFactory<sgscDbContext> contextFactory,
        ILogger<UtilizadorRepository> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public async Task<Utilizador> GetByIdAsync(int id)
    {
        await using var ctx = _contextFactory.CreateDbContext();
        return await ctx.Utilizadors.AsNoTracking().FirstOrDefaultAsync(u => u.IdUtilizador == id);
    }

    public async Task<IEnumerable<Utilizador>> GetAllAsync()
    {
        await using var ctx = _contextFactory.CreateDbContext();
        return await ctx.Utilizadors.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Utilizador utilizador)
    {
        await using var ctx = _contextFactory.CreateDbContext();
        ctx.Utilizadors.Add(utilizador);
        await ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Utilizador utilizador)
    {
        await using var ctx = _contextFactory.CreateDbContext();
        ctx.Utilizadors.Update(utilizador);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var ctx = _contextFactory.CreateDbContext();
        var entity = await ctx.Utilizadors.FindAsync(id);
        if (entity is null) return;
        ctx.Utilizadors.Remove(entity);
        await ctx.SaveChangesAsync();
    }

    public async Task<Utilizador> GetByUsernameAsync(string username)
    {
        await using var ctx = _contextFactory.CreateDbContext();
        return await ctx.Utilizadors.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
    }
}