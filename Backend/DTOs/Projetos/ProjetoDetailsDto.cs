namespace Backend.DTOs.Projeto;
using Backend.DTOs.Membros;
using Backend.DTOs.Utilizadores;
using Backend.DTOs.Tarefas;
public class ProjetoDetailsDto
{
    public UtilizadorDto Utilizador { get; set; } = null!;
    public List<MembroDto> Membros { get; set; } = new List<MembroDto>();
    public List<TarefaDto> Tarefas { get; set; } = new List<TarefaDto>();
}