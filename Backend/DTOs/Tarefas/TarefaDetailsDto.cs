namespace Backend.DTOs.Tarefas;
using Backend.DTOs.Projeto;
using Backend.DTOs.Utilizadores;
public class TarefaDetailsDto
{
    public List<ProjetoDto> Projetos { get; set; } = new List<ProjetoDto>();
    public List<UtilizadorDto> Utilizadores { get; set; } = new List<UtilizadorDto>();
}