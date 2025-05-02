using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Projetos;

public class UpdateProjetoDto
{
    [Required]
    public int IdProjeto { get; set; }

    [Required]
    [MaxLength(250)]
    public string Nome { get; set; } = null!;

    [Required]
    [MaxLength(250)]
    public string NomeCliente { get; set; } = null!;

    public string? Descricao { get; set; }

    [Range(0, 999999.99)]
    public decimal? PrecoHora { get; set; }

    [Required]
    public int Responsavel { get; set; }

    public bool IsDeleted { get; set; } = false;
}