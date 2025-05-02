using Backend.DTOs.Projeto;
namespace Backend.Services.Interfaces;

public interface IProjetoService
{
    Task<ProjetoDto> GetByIdAsync(int id);
    Task<IEnumerable<ProjetoDto>> GetByUtilizadorAsync(int utilizadorId);
    Task<ProjetoDto> CreateAsync(ProjetoCreateDto dto);
    Task UpdateAsync(int id, UpdateProjetoDto dto);
    Task DeleteAsync(int id);
    Task ConvidarUtilizadorAsync(int projetoId, string username);
    Task RemoverMembroAsync(int membroId);
}