using System;
using System.Linq;
using Backend.Models;

namespace Backend.Domain.Specifications;

public sealed class TarefasConcluidasEntreDatasSpec : ITarefaSpecification
{
    private readonly DateTime _ini;
    private readonly DateTime _fim;
    public TarefasConcluidasEntreDatasSpec(DateTime ini, DateTime fim) { _ini = ini; _fim = fim; }

    public IQueryable<Tarefa> Apply(IQueryable<Tarefa> q) =>
        q.Where(t => t.DataFim.HasValue && t.DataFim >= _ini && t.DataFim <= _fim);
}