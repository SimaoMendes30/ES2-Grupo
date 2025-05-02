namespace Backend.Repositories;

using Repositories.Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;

public sealed class TarefaRepository : ITarefaRepository
{
    private readonly IDbContextFactory<sgscDbContext> _factory;
    public TarefaRepository(IDbContextFactory<sgscDbContext> factory) => _factory = factory;

    public async Task<Tarefa> GetByIdAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Include(t => t.IdProjetos).AsNoTracking()
                                .FirstAsync(t => t.IdTarefa == id);
    }
    
    public async Task<IEnumerable<Tarefa>> GetByProjetoIdAsync(int projetoId)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Where(t => t.IdProjetos.Any(p => p.IdProjeto == projetoId))
                                .AsNoTracking().ToListAsync();
    }
    
    public async Task<IEnumerable<Tarefa>> GetByUtilizadorIdAsync(int utilizadorId)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Where(t => t.IdUtilizadors.Any(u => u.IdUtilizador == utilizadorId))
                                .AsNoTracking().ToListAsync();
    }
    
    public async Task<IEnumerable<Tarefa>> GetConcluidasEntreDatasAsync(int utilizadorId, DateTime inicio, DateTime fim)
    {
        var dataInicio = DateOnly.FromDateTime(inicio);
        var dataFim = DateOnly.FromDateTime(fim);

        await using var ctx = _factory.CreateDbContext();

        return await ctx.Tarefas
            .Where(t => t.IdUtilizadors.Any(u => u.IdUtilizador == utilizadorId) &&
                        t.DataFim.HasValue &&
                        t.DataFim.Value >= dataInicio &&
                        t.DataFim.Value <= dataFim)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Tarefa>> GetEmCursoAsync(int utilizadorId)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Where(t => t.IdUtilizadors.Any(u => u.IdUtilizador == utilizadorId) &&
                                            t.DataFim == null)
                                 .AsNoTracking().ToListAsync();
    }
    
    public async Task AddAsync(Tarefa tarefa)
    {
        await using var ctx = _factory.CreateDbContext();
        ctx.Tarefas.Add(tarefa);
        await ctx.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Tarefa tarefa)
    {
        await using var ctx = _factory.CreateDbContext();
        ctx.Tarefas.Update(tarefa);
        await ctx.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        var entity = await ctx.Tarefas.FindAsync(id);
        if (entity is null) return;
        ctx.Tarefas.Remove(entity);
        await ctx.SaveChangesAsync();
    }
}