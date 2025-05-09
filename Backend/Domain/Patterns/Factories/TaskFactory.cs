using Backend.Models;

namespace Backend.Domain.Patterns.Factories;

public static class TaskFactory
{
    public static TaskEntity Create(
        int responsavelId,
        string titulo,
        string descricao,
        DateTimeOffset? dataInicio,
        DateTimeOffset? dataFim,
        string estado = "Em curso",
        decimal? precoHora = null,
        bool isDeleted = false)
    {
        return new TaskEntity
        {
            Responsavel     = responsavelId,
            Titulo          = titulo,
            Descricao       = descricao,
            DataInicio      = dataInicio ?? DateTimeOffset.UtcNow,
            DataFim         = dataFim,
            Estado          = string.IsNullOrWhiteSpace(estado) ? "Em curso" : estado,
            PrecoHora       = precoHora,
            IsDeleted       = isDeleted
        };
    }
}