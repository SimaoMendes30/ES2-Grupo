using Backend.DTOs.Membros;
using Backend.DTOs.Tarefas;
using Backend.DTOs.Utilizadores;

namespace Backend.DTOs.Projetos;

public class ProjetoDetailsDto
{
    public int IdProjeto { get; set; }
    public string Nome { get; set; } = null!;
    public string? NomeCliente { get; set; }
    public string? Descricao { get; set; }
    public decimal? PrecoHora { get; set; }
    public int Responsavel { get; set; }
    public DateOnly? DataCriacao { get; set; }
    public bool IsDeleted { get; set; }

    public UtilizadorDto ResponsavelUtilizador { get; set; } = null!;
    public List<MembroDto> Membros { get; set; } = new();
    public List<TarefaDto> Tarefas { get; set; } = new();
}