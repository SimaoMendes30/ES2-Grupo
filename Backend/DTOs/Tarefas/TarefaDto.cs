namespace Backend.DTOs.Tarefas
{
    public class TarefaDto
    {
        public int    IdTarefa        { get; set; }
        public string Descricao       { get; set; } = null!;
        public DateOnly DataInicio    { get; set; }
        public DateOnly? DataFim      { get; set; }
        public string Estado          { get; set; } = null!;
        public int    Responsavel     { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public decimal? PrecoHora     { get; set; }
        public decimal? HorasDecorridas { get; set; }
    }
}