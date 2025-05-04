namespace Backend.Domain.DTOs.Member
{
    public class MemberDetailsDto
    {
        public int IdMembro { get; set; }
        public int IdUtilizador { get; set; }
        public int IdProjeto { get; set; }
        public DateTime DataConvite { get; set; }
        public DateTime? DataEstado { get; set; }
        public string EstadoConvite { get; set; } = null!;
        public string? EstadoAtividade { get; set; }
    }
}