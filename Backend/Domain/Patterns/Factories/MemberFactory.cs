using Backend.Models;

namespace Backend.Domain.Patterns.Factories;

public static class MemberFactory
{
    public static MemberEntity Create(
        int idProjeto,
        int idUtilizador,
        string estadoConvite = "Pendente",
        string estadoAtividade = "Ativo",
        DateTime? dataConvite = null,
        DateTime? dataEstado = null)
    {
        return new MemberEntity
        {
            IdProjeto = idProjeto,
            IdUtilizador = idUtilizador,
            EstadoConvite = string.IsNullOrWhiteSpace(estadoConvite) ? "Pendente" : estadoConvite,
            EstadoAtividade = string.IsNullOrWhiteSpace(estadoAtividade) ? "Ativo" : estadoAtividade,
            DataConvite = dataConvite ?? DateTime.UtcNow,
            DataEstado = dataEstado ?? DateTime.UtcNow
        };
    }
}