namespace Backend.DTOs.Tarefas;
using Backend.DTOs.Projetos;
using Backend.DTOs.Utilizadores;

public class TarefaDetailsDto
{
    public List<ProjetoDto> Projetos { get; set; } = new();
    public List<UtilizadorDto> Utilizadores { get; set; } = new();
}