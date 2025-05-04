using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Member
{
    public class MemberUpdateDto
    {
        [Required]
        [StringLength(256)]
        public string EstadoConvite { get; set; } = null!;
        
        [Required]
        [StringLength(256)]
        public string? EstadoAtividade { get; set; } = null!;
        
        [DataType(DataType.DateTime)]
        public DateTime? DataEstado { get; set; }
    }
}