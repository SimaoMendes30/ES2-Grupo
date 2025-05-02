using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Projetos;


public class ProjetoCreateDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    public string NomeCliente { get; set; }

    public string Descricao { get; set; }

    [Range(0, 9999, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal? PrecoHora { get; set; }

    public int IdUtilizador { get; set; }
}