using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.DTOs.Project
{
    public class ProjectCreateDto
    {
        [Required]
        [StringLength(256)]
        public string Nome { get; set; } = null!;
        
        [StringLength(256)]
        public string? NomeCliente { get; set; }
        
        public string? Descricao { get; set; }

        [Range(0, 9999999999.99)] 
        public decimal? PrecoHora { get; set; }

        [Required]
        public int Responsavel { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset DataCriacao { get; set; }
    }
}