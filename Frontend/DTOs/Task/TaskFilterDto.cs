namespace Frontend.DTOs.Task
{
    public class TaskFilterDto
    {
        public int? IdTarefa { get; set; }

        public string? Titulo { get; set; }

        public int? Responsavel { get; set; }

        public DateTime? DataInicioDe { get; set; }

        public DateTime? DataInicioAte { get; set; }

        public DateTime? DataFimDe { get; set; }

        public DateTime? DataFimAte { get; set; }

        public string? Estado { get; set; }

        public decimal? PrecoHoraMin { get; set; }

        public decimal? PrecoHoraMax { get; set; }

        public bool? IsDeleted { get; set; }
    }
}