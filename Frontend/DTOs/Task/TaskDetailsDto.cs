namespace Frontend.DTOs.Task
{
    public class TaskDetailsDto
    {
        public int IdTarefa { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descricao { get; set; }

        public int Responsavel { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string Estado { get; set; } = null!;

        public decimal? PrecoHora { get; set; }

        public bool IsDeleted { get; set; }
    }
}