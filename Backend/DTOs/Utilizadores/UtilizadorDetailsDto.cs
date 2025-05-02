using Backend.DTOs.Projetos;

namespace Backend.DTOs.Utilizadores;
using Backend.DTOs.Projetos;
using Backend.DTOs.Membros;
using Backend.DTOs.Tarefas;
public class UtilizadorDetailsDto
{
    public int? NumHoras { get; set; }
    public List<MembroDto> Membros { get; set; } = new List<MembroDto>();
    public List<ProjetoDto> Projetos { get; set; } = new List<ProjetoDto>();
    public List<TarefaDto> IdTarefas { get; set; } = new List<TarefaDto>();
}