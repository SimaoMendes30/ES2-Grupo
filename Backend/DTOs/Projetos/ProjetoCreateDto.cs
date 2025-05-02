namespace Backend.DTOs.Projeto;
using Backend.DTOs.Utilizadores;

public class ProjetoCreateDto
{
    public string Nome { get; set; } = null!;
    public string? NomeCliente { get; set; }
    public string? Descricao { get; set; }
    public decimal? PrecoHora { get; set; }
    public int IdUtilizador { get; set; }
}