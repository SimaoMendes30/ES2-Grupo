namespace Frontend.DTOs.Tarefas;

public class TarefaDto
{
    public int      IdTarefa       { get; set; }
    public string   Descricao      { get; set; } = string.Empty;
    public DateOnly DataInicio     { get; set; }
    public DateOnly? DataFim       { get; set; }
    public string   Estado         { get; set; } = string.Empty;
    public int      Responsavel    { get; set; }
    public DateTime? DataHoraInicio{ get; set; }
    public decimal? PrecoHora      { get; set; }
}