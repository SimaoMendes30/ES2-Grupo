using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Project
{
    public class ProjectCreateDto
    {
        [Required]
        [StringLength(256)]
        public string Nome { get; set; } = null!;
        
        [StringLength(256)]
        public string? NomeCliente { get; set; }
        
        public string? Descricao { get; set; }

        [Range(0, 9999999999.99)] // máximo de 10 dígitos, 2 decimais
        public decimal? PrecoHora { get; set; }

        [Required]
        public int Responsavel { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; }
    }
}