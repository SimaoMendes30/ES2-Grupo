using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Task
{
    public class TaskUpdateDto
    {
        [StringLength(256)]
        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public int? Responsavel { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataFim { get; set; }

        [StringLength(256)]
        public string? Estado { get; set; }

        [Range(0, 9999999999.99)]
        public decimal? PrecoHora { get; set; }
    }
}