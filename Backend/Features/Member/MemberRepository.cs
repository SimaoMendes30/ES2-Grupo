using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Member;
using Backend.Domain.Patterns.Specifications;
using Backend.Features.Member.Interfaces;

namespace Backend.Features.Member;

public sealed class MemberRepository : IMemberRepository
{
    private readonly IDbContextFactory<SgscDbContext> _factory;
    private readonly ILogger<MemberRepository> _logger;

    public MemberRepository(IDbContextFactory<SgscDbContext> factory, ILogger<MemberRepository> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task<MemberEntity?> GetByIdAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        return await ctx.Membro
            .Include(m => m.IdUserEntityNavigation)
            .Include(m => m.IdProjectEntityNavigation)
            .FirstOrDefaultAsync(m => m.IdMembro == id);
    }

    public async System.Threading.Tasks.Task AddAsync(MemberEntity member)
    {
        Validate(member);

        await using var ctx = _factory.CreateDbContext();
        ctx.Membro.Add(member);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateAsync(MemberEntity member)
    {
        Validate(member);

        await using var ctx = _factory.CreateDbContext();
        ctx.Membro.Update(member);
        await ctx.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await using var ctx = _factory.CreateDbContext();
        var entity = await ctx.Membro.FindAsync(id);
        if (entity is null) return;

        ctx.Membro.Remove(entity);
        await ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<MemberEntity>> FilteredListAsync(MemberFilterDto filter)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new MemberByFilterSpec(filter);
        return await ctx.Membro
            .Where(spec.ToExpression())
            .Include(m => m.IdUserEntityNavigation)
            .Include(m => m.IdProjectEntityNavigation)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<PagedResult<MemberEntity>> FilteredPagedAsync(MemberFilterDto filter, int page, int pageSize)
    {
        await using var ctx = _factory.CreateDbContext();
        var spec = new MemberByFilterSpec(filter);

        var query = ctx.Membro
            .Where(spec.ToExpression())
            .Include(m => m.IdUserEntityNavigation)
            .Include(m => m.IdProjectEntityNavigation)
            .AsNoTracking();

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<MemberEntity>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    private void Validate(MemberEntity member)
    {
        var allowedInviteStatuses = new[] { "Pendente", "Aceite", "Recusado" };
        if (!allowedInviteStatuses.Contains(member.EstadoConvite))
            throw new ValidationException("EstadoConvite inválido. Deve ser: Pendente, Aceite ou Recusado.");

        var allowedActivityStatuses = new[] { "Ativo", "Inativo" };
        if (!string.IsNullOrEmpty(member.EstadoAtividade) &&
            !allowedActivityStatuses.Contains(member.EstadoAtividade))
        {
            throw new ValidationException("EstadoAtividade inválido. Deve ser: Ativo ou Inativo.");
        }
    }
}
