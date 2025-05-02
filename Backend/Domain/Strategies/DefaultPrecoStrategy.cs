namespace Backend.Domain.Strategies;
using Backend.Models;
public sealed class DefaultPrecoStrategy : IPrecoTarefaStrategy
{
    public decimal CalcularPreco(Tarefa tarefa, Projeto projeto) =>
        tarefa.PrecoHora ?? projeto?.PrecoHora ?? 0m;
}