using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.DTOs.Membros;

namespace Backend.Services.Interfaces
{
    public interface IMembroService
    {
        Task<IEnumerable<MembroDto>> GetByProjetoAsync(int projetoId);
        Task AddAsync(CreateMembroDto dto);
        Task DeleteAsync(int id);
        
        Task ResponderConviteAsync(int membroId, bool aceitar);
    }
}