public class ProjectFilterDto
{
    public int? IdProjeto { get; set; }
    public List<int>? IdsProjetos { get; set; }
    public string? Nome { get; set; }
    public string? NomeCliente { get; set; }
    public decimal? PrecoHoraMin { get; set; }
    public decimal? PrecoHoraMax { get; set; }
    public int? Responsavel { get; set; }
    public List<int>? IdsResponsaveis { get; set; }
    public DateTimeOffset? DataCriacaoDe { get; set; }
    public DateTimeOffset? DataCriacaoAte { get; set; }
    public bool? IsDeleted { get; set; }
}