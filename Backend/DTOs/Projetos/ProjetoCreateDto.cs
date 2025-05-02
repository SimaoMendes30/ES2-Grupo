using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Projetos;

public class ProjetoCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(250)]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [MaxLength(250)]
    public string NomeCliente { get; set; } = null!;

    public string? Descricao { get; set; }

    [Range(0, 999999.99)]
    public decimal? PrecoHora { get; set; }

    [Required]
    public int Responsavel { get; set; }

    public bool IsDeleted { get; set; } = false;
}