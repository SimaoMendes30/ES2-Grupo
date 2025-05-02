namespace Backend.Domain.Strategies;

using Models;

public interface IPrecoTarefaStrategy
{
    decimal CalcularPreco(Tarefa tarefa, Projeto projeto);
}