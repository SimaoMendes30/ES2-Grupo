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
        public DateTimeOffset? DataConviteDe { get; set; }
        public DateTimeOffset? DataConviteAte { get; set; }
        public DateTimeOffset? DataEstadoDe { get; set; }
        public DateTimeOffset? DataEstadoAte { get; set; }
        public string? EstadoConvite { get; set; }
        public string? EstadoAtividade { get; set; }
    }
}