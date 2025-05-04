using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Task;
using Backend.Domain.Patterns.Specifications;
using Backend.Features.Task.Interfaces;

namespace Backend.Features.Task;

public sealed class TaskRepository : ITaskRepository
{
    private readonly IDbContextFactory<SgscDbContext> _factory;
    private readonly ILogger<TaskRepository> _logger;

    public TaskRepository(IDbContextFactory<SgscDbContext> factory, ILogger<TaskRepository> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task<TaskEntity?> GetByIdAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Tarefas
            .Include(t => t.ResponsavelNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.IdTarefa == id);
    }

    public async Task<IEnumerable<TaskEntity>> FilteredListAsync(TaskFilterDto filter)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new TaskByFilterSpec(filter);

        return await ctx.Tarefas
            .Where(spec.ToExpression())
            .Include(t => t.ResponsavelNavigation)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PagedResult<TaskEntity>> FilteredPagedAsync(TaskFilterDto filter, int page, int pageSize)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new TaskByFilterSpec(filter);

        var query = ctx.Tarefas
            .Where(spec.ToExpression())
            .Include(t => t.ResponsavelNavigation)
            .AsNoTracking();

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TaskEntity>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async System.Threading.Tasks.Task AddAsync(TaskEntity task)
    {
        Validate(task);

        await using var ctx = _factory.CreateDbContext();
        ctx.Tarefas.Add(task);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateAsync(TaskEntity task)
    {
        Validate(task);

        await using var ctx = _factory.CreateDbContext();
        ctx.Tarefas.Update(task);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        var entity = await ctx.Tarefas.FindAsync(id);
        if (entity is null) return;

        entity.IsDeleted = true;
        await ctx.SaveChangesAsync();
    }
    
    public async Task<List<int>> GetTarefaIdsByUtilizadorAsync(int userId)
    {
        await using var ctx = _factory.CreateDbContext();

        return await ctx.Utilizadors
            .Where(u => u.IdUtilizador == userId)
            .SelectMany(u => u.IdTarefas.Select(t => t.IdTarefa))
            .Distinct()
            .ToListAsync();
    }
    
    public async Task<List<TaskEntity>> GetByProjetoIdAsync(int projetoId)
    {
        await using var ctx = _factory.CreateDbContext();

        return await ctx.Tarefas
            .Where(t => t.IdProjetos.Any(p => p.IdProjeto == projetoId))
            .Include(t => t.ResponsavelNavigation)
            .AsNoTracking()
            .ToListAsync();
    }

    
    private void Validate(TaskEntity task)
    {
        var allowedStatuses = new[] { "Em curso", "Finalizada" };
        if (!allowedStatuses.Contains(task.Estado))
            throw new ValidationException("Estado inválido. Deve ser: Em curso ou Finalizada.");

        if (string.IsNullOrWhiteSpace(task.Titulo))
            throw new ValidationException("O título da tarefa é obrigatório.");

        if (task.PrecoHora.HasValue && task.PrecoHora < 0)
            throw new ValidationException("O preço/hora não pode ser negativo.");
    }
}