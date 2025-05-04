using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.DTOs.Project
{
    public class ProjectUpdateDto
    {
        [StringLength(256)]
        public string? Nome { get; set; }

        [StringLength(256)]
        public string? NomeCliente { get; set; }

        public string? Descricao { get; set; }

        [Range(0, 9999999999.99)]
        public decimal? PrecoHora { get; set; }
        
        public int? Responsavel { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; }
    }
}