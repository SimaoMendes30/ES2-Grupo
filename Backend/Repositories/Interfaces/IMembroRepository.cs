using Backend.Models;

namespace Backend.Repositories.Interfaces
{
    public interface IMembroRepository
    {
        Task<IEnumerable<Membro>> GetByProjetoIdAsync(int projetoId);
        Task<Membro?>  GetByIdAsync(int id);          
        Task AddAsync(Membro membro);
        Task UpdateAsync(Membro membro);           
        Task DeleteAsync(int id);
    }
}