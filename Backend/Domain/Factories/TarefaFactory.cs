namespace Backend.Domain.Factories;
using Backend.Models;
using Backend.DTOs.Relatorios;
public static class TarefaFactory
{
    public static Tarefa Criar(string descricao, int responsavel, decimal? precoHora,
        DateTime? dataInicio = null)
    {
        return new Tarefa
        {
            Descricao = descricao,
            Responsavel = responsavel,
            DataHoraInicio = dataInicio ?? DateTime.UtcNow,
            DataInicio = DateOnly.FromDateTime((dataInicio ?? DateTime.UtcNow).Date),
            Estado = "Em Curso",
            PrecoHora = precoHora
        };
    }
}