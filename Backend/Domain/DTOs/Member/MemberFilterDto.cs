namespace Backend.Domain.DTOs.Member
{
    public class MemberFilterDto
    {
        public int? IdMembro { get; set; }
        public List<int>? IdsMembros { get; set; } 
        public int? IdUtilizador { get; set; }
        public List<int>? IdsUtilizadores { get; set; } 
        public int? IdProjeto { get; set; }
        public List<int>? IdsProjetos { get; set; } 
        public DateTime? DataConviteDe { get; set; }
        public DateTime? DataConviteAte { get; set; }
        public DateTime? DataEstadoDe { get; set; }
        public DateTime? DataEstadoAte { get; set; }
        public string? EstadoConvite { get; set; }
        public string? EstadoAtividade { get; set; }
    }
}