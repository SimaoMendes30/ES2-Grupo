using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IProjetoRepository
{
    Task<Projeto> GetByIdAsync(int id);
    Task<IEnumerable<Projeto>> GetByUtilizadorIdAsync(int utilizadorId);
    Task AddAsync(Projeto projeto);
    Task UpdateAsync(Projeto projeto);
    Task DeleteAsync(int id);
}