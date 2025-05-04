using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Member
{
    public class MemberCreateDto
    {
        [Required]
        public int IdUtilizador { get; set; }

        [Required]
        public int IdProjeto { get; set; }

        [Required]
        [StringLength(256)]
        public string EstadoConvite { get; set; } = "Pendente";

        [StringLength(256)]
        public string? EstadoAtividade { get; set; } = "Ativo";
        
        [DataType(DataType.DateTime)]
        public DateTime? DataConvite { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? DataEstado { get; set; }
    }
}