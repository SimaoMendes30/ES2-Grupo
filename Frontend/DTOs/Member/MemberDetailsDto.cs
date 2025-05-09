namespace Frontend.DTOs.Member
{
    public class MemberDetailsDto
    {
        public int IdMembro { get; set; }
        public int IdUtilizador { get; set; }
        public int IdProjeto { get; set; }
        public DateTimeOffset DataConvite { get; set; }
        public DateTimeOffset? DataEstado { get; set; }
        public string EstadoConvite { get; set; } = null!;
        public string? EstadoAtividade { get; set; }
    }
}