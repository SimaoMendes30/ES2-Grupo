using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Repositories
{
    public sealed class MembroRepository : IMembroRepository
    {
        private readonly IDbContextFactory<sgscDbContext> _factory;
        private readonly ILogger<MembroRepository>        _logger;

        public MembroRepository(IDbContextFactory<sgscDbContext> factory,
            ILogger<MembroRepository> logger)
        {
            _factory = factory;
            _logger  = logger;
        }

        public async Task<IEnumerable<Membro>> GetByProjetoIdAsync(int projetoId)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Membros.Where(m => m.IdProjeto == projetoId)
                .Include(m => m.IdUtilizadorNavigation)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Membro?> GetByIdAsync(int id)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Membros.FirstOrDefaultAsync(m => m.IdMembro == id);
        }

        public async Task AddAsync(Membro membro)
        {
            await using var ctx = _factory.CreateDbContext();
            ctx.Membros.Add(membro);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Membro membro)
        {
            await using var ctx = _factory.CreateDbContext();
            ctx.Membros.Update(membro);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var ctx = _factory.CreateDbContext();
            var entity = await ctx.Membros.FindAsync(id);
            if (entity is null) return;
            ctx.Membros.Remove(entity);
            await ctx.SaveChangesAsync();
        }
    }
}