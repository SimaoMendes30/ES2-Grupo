namespace Backend.DTOs.Projetos;

public class ProjetoDto
{
    public int IdProjeto { get; set; }
    public string Nome { get; set; } = null!;
    public string? NomeCliente { get; set; }
    public string? Descricao { get; set; }
    public decimal? PrecoHora { get; set; }
    public int Responsavel { get; set; }
    public DateOnly? DataCriacao { get; set; }
    public bool IsDeleted { get; set; }
}