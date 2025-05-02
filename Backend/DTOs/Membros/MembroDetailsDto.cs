namespace Backend.DTOs.Membros;
using Backend.DTOs.Projeto;
using Backend.DTOs.Utilizadores;
public class MembroDetailsDto
{
    public ProjetoDto Projeto { get; set; } = null!;
    public UtilizadorDto Utilizador { get; set; } = null!;
}