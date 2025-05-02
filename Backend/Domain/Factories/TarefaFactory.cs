using System;
using Backend.Models;

namespace Backend.Domain.Factories;

public static class TarefaFactory
{
    public static Tarefa Criar(string titulo, string? descricao, int responsavel, decimal? precoHora, DateTime? ini = null)
    {
        return new Tarefa
        {
            Titulo      = titulo,
            Descricao   = descricao,
            Responsavel = responsavel,
            DataInicio  = ini ?? DateTime.UtcNow,
            Estado      = "Em Curso",
            PrecoHora   = precoHora
        };
    }
}