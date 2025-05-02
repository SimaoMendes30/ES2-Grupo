using Backend.DTOs.Utilizadores;

namespace Backend.Services.Interfaces;

public interface IUtilizadorService
{
    Task<UtilizadorTokenDto> GerarTokenAsync(UtilizadorLoginDto dto);
    Task<UtilizadorDto> CreateAsync(UtilizadorCreateDto dto);
    Task<IEnumerable<UtilizadorDto>> GetAllAsync();
    Task<UtilizadorDto> GetByIdAsync(int id);
    Task<UtilizadorDetailsDto> GetDetailsAsync(int id);
    Task UpdateAsync(int id, UtilizadorUpdateDto dto);
    Task UpdatePasswordAsync(int id, UtilizadorUpdatePasswordDto dto);
    Task DeleteAsync(int id);
}