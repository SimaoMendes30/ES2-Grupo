using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Tarefas
{
    public class TarefaCreateDto
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de início é obrigatória")]
        public DateOnly DataInicio { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public DateOnly? DataFim { get; set; }

        [Required]
        public string Estado { get; set; } = "Em Curso";

        [Range(0, double.MaxValue)]
        public decimal? PrecoHora { get; set; }
    }
}