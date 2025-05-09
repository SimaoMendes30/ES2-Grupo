using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.DTOs.Task
{
    public class TaskUpdateDto
    {
        [StringLength(256)]
        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public int? Responsavel { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DataInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DataFim { get; set; }

        [StringLength(256)]
        public string? Estado { get; set; }

        [Range(0, 9999999999.99)]
        public decimal? PrecoHora { get; set; }
    }
}