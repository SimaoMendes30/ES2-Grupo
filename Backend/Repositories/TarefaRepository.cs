using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public sealed class TarefaRepository : ITarefaRepository
{
    private readonly IDbContextFactory<sgscDbContext> _factory;
    public TarefaRepository(IDbContextFactory<sgscDbContext> factory) => _factory = factory;

    public async Task<Tarefa> GetByIdAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Include(t => t.IdProjetos).FirstAsync(t => t.IdTarefa == id);
    }

    public async Task<IEnumerable<Tarefa>> GetByProjetoIdAsync(int projetoId)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Where(t => t.IdProjetos.Any(p => p.IdProjeto == projetoId) && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> GetByUtilizadorIdAsync(int utilizadorId)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Where(t => t.IdUtilizadors.Any(u => u.IdUtilizador == utilizadorId) && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> GetConcluidasEntreDatasAsync(int utilizadorId, DateTime inicio, DateTime fim)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Where(t => t.IdUtilizadors.Any(u => u.IdUtilizador == utilizadorId) &&
                                            t.DataFim.HasValue && t.DataFim >= inicio && t.DataFim <= fim &&
                                            !t.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> GetEmCursoAsync(int utilizadorId)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas.Where(t => t.IdUtilizadors.Any(u => u.IdUtilizador == utilizadorId) &&
                                            t.DataFim == null && !t.IsDeleted).ToListAsync();
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
        var tarefa = await ctx.Tarefas.FindAsync(id);
        if (tarefa is null) return;
        tarefa.IsDeleted = true;
        await ctx.SaveChangesAsync();
    }
}