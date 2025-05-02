using Backend.Models;
using Backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repositories.Interfaces
{
    public interface IUtilizadorRepository
    {
        Task<Utilizador> GetByIdAsync(int id);
        Task<IEnumerable<Utilizador>> GetAllAsync();
        Task AddAsync(Utilizador utilizador);
        Task UpdateAsync(Utilizador utilizador);
        Task DeleteAsync(int id);
        Task<Utilizador> GetByUsernameAsync(string username);
    }
}