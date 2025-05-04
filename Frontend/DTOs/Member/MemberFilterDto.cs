namespace Frontend.DTOs.Member
{
    public class MemberFilterDto
    {
        public int? IdMembro { get; set; }
        public int? IdUtilizador { get; set; }
        public int? IdProjeto { get; set; }
        public DateTime? DataConviteDe { get; set; }
        public DateTime? DataConviteAte { get; set; }
        public DateTime? DataEstadoDe { get; set; }
        public DateTime? DataEstadoAte { get; set; }
        public string? EstadoConvite { get; set; }
        public string? EstadoAtividade { get; set; }
    }
}