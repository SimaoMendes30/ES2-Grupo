namespace Frontend.DTOs.Tarefas;

public class StartTarefaDto
{
    public string Descricao { get; set; } = string.Empty;
    public int Responsavel { get; set; }
    public int ProjetoId { get; set; }
    public DateTime? DataHoraInicio { get; set; }
    public decimal? PrecoHora { get; set; }
}
