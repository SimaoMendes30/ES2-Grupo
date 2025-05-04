using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Project;
using Backend.Domain.Patterns.Specifications;
using Backend.Features.Project.Interfaces;

namespace Backend.Features.Project;

public sealed class ProjectRepository : IProjectRepository
{
    private readonly IDbContextFactory<SgscDbContext> _factory;
    private readonly ILogger<ProjectRepository> _logger;

    public ProjectRepository(IDbContextFactory<SgscDbContext> factory, ILogger<ProjectRepository> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task<ProjectEntity?> GetByIdAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Projetos
            .Include(p => p.ResponsavelNavigation)
            .FirstOrDefaultAsync(p => p.IdProjeto == id);
    }

    public async System.Threading.Tasks.Task AddAsync(ProjectEntity project)
    {
        Validate(project);

        await using var ctx = _factory.CreateDbContext();
        ctx.Projetos.Add(project);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateAsync(ProjectEntity project)
    {
        Validate(project);

        await using var ctx = _factory.CreateDbContext();
        ctx.Projetos.Update(project);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        var entity = await ctx.Projetos.FindAsync(id);
        if (entity is null) return;

        ctx.Projetos.Remove(entity);
        await ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProjectEntity>> FilteredListAsync(ProjectFilterDto filter)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new ProjectByFilterSpec(filter);

        return await ctx.Projetos
            .Where(spec.ToExpression())
            .Include(p => p.ResponsavelNavigation)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<PagedResult<ProjectEntity>> FilteredPagedAsync(ProjectFilterDto filter, int page, int pageSize)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new ProjectByFilterSpec(filter);

        var query = ctx.Projetos
            .Where(spec.ToExpression())
            .Include(p => p.ResponsavelNavigation)
            .AsNoTracking();

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ProjectEntity>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }
    
    private void Validate(ProjectEntity project)
    {
        if (string.IsNullOrWhiteSpace(project.Nome))
            throw new ValidationException("O nome do projeto é obrigatório.");

        if (project.PrecoHora.HasValue && project.PrecoHora < 0)
            throw new ValidationException("O preço/hora não pode ser negativo.");
    }
}
