namespace Backend.Domain.DTOs.Task
{
    public class TaskFilterDto
    {
        public int? IdTarefa { get; set; }
        public List<int>? IdsTarefas { get; set; } 
        public string? Titulo { get; set; }
        public int? Responsavel { get; set; }
        public List<int>? IdsResponsaveis { get; set; }
        public DateTimeOffset? DataInicioDe { get; set; }
        public DateTimeOffset? DataInicioAte { get; set; }
        public DateTimeOffset? DataFimDe { get; set; }
        public DateTimeOffset? DataFimAte { get; set; }
        public string? Estado { get; set; }
        public decimal? PrecoHoraMin { get; set; }
        public decimal? PrecoHoraMax { get; set; }
        public bool? IsDeleted { get; set; }
    }
}