using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Task
{
    public class TaskCreateDto
    {
        [Required]
        [StringLength(256)]
        public string Titulo { get; set; } = null!;

        public string? Descricao { get; set; }

        [Required]
        public int Responsavel { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset DataInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DataFim { get; set; }

        [Required]
        [StringLength(256)]
        public string Estado { get; set; } = null!;

        [Range(0, 9999999999.99)]
        public decimal? PrecoHora { get; set; }
    }
}