namespace Frontend.DTOs.Project
{
    public class ProjectFilterDto
    {
        public int? IdProjeto { get; set; }

        public string? Nome { get; set; }

        public string? NomeCliente { get; set; }

        public decimal? PrecoHoraMin { get; set; }

        public decimal? PrecoHoraMax { get; set; }

        public int? Responsavel { get; set; }

        public DateTimeOffset? DataCriacaoDe { get; set; }

        public DateTimeOffset? DataCriacaoAte { get; set; }

        public bool? IsDeleted { get; set; }
    }
}