namespace Backend.Domain.Specifications;
using Backend.Models;
using Backend.DTOs.Relatorios;
public sealed class TarefasConcluidasEntreDatasSpec : ITarefaSpecification
{
    private readonly DateTime _inicio;
    private readonly DateTime _fim;
    public TarefasConcluidasEntreDatasSpec(DateTime inicio, DateTime fim)
    {
        _inicio = inicio;
        _fim = fim;
    }
    public IQueryable<Tarefa> Apply(IQueryable<Tarefa> query) =>
        query.Where(t => t.DataFim != null &&
                         t.DataHoraInicio != null &&
                         t.DataHoraInicio >= _inicio &&
                         t.DataFim.Value.ToDateTime(TimeOnly.MinValue) <= _fim);
}
