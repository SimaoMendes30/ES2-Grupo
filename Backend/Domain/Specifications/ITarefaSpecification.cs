namespace Backend.Domain.Specifications;
using Backend.Models;
public interface ITarefaSpecification
{
    IQueryable<Tarefa> Apply(IQueryable<Tarefa> query);
}