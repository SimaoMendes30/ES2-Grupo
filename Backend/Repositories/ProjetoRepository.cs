namespace Backend.Repositories;

using Repositories.Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;

public sealed class ProjetoRepository : IProjetoRepository
{
    private readonly IDbContextFactory<sgscDbContext> _factory;
    public ProjetoRepository(IDbContextFactory<sgscDbContext> factory) => _factory = factory;

    public async Task<Projeto> GetByIdAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Projetos.Include(p => p.Membros).ThenInclude(m => m.IdUtilizadorNavigation)
            .Include(p => p.IdTarefas)
            .AsNoTracking().FirstAsync(p => p.IdProjeto == id);
    }
    
    public async Task<IEnumerable<Projeto>> GetByUtilizadorIdAsync(int utilizadorId)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Projetos.Where(p => p.IdUtilizador == utilizadorId)
            .Include(p => p.Membros)
            .AsNoTracking().ToListAsync();
    }
    
    public async Task AddAsync(Projeto projeto)
    {
        await using var ctx = _factory.CreateDbContext();
        ctx.Projetos.Add(projeto);
        await ctx.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Projeto projeto)
    {
        await using var ctx = _factory.CreateDbContext();
        ctx.Projetos.Update(projeto);
        await ctx.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        var entity = await ctx.Projetos.FindAsync(id);
        if (entity is null) return;
        ctx.Projetos.Remove(entity);
        await ctx.SaveChangesAsync();
    }
}