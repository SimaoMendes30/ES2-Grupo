namespace Frontend.DTOs.Tarefas;

public class TarefaDto
{
    public int IdTarefa { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public string Estado { get; set; } = null!;
    public int Responsavel { get; set; }
    public decimal? PrecoHora { get; set; }
    public decimal? HorasDecorridas { get; set; }
    public bool IsDeleted { get; set; }
}