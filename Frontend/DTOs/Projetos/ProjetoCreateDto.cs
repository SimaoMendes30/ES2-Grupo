using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Projetos;

public class ProjetoCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(250, ErrorMessage = "O nome deve ter no máximo 250 caracteres.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [MaxLength(250, ErrorMessage = "O nome do cliente deve ter no máximo 250 caracteres.")]
    public string NomeCliente { get; set; } = null!;

    public string? Descricao { get; set; }

    [Range(0, 999999.99, ErrorMessage = "O valor por hora deve ser positivo.")]
    public decimal? PrecoHora { get; set; }

    [Required(ErrorMessage = "O responsável é obrigatório.")]
    public int Responsavel { get; set; }

    public bool IsDeleted { get; set; } = false;
}