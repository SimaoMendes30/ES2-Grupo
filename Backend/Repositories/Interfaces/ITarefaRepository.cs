using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface ITarefaRepository
{
    Task<Tarefa> GetByIdAsync(int id);
    Task<IEnumerable<Tarefa>> GetByProjetoIdAsync(int projetoId);
    Task<IEnumerable<Tarefa>> GetByUtilizadorIdAsync(int utilizadorId);
    Task<IEnumerable<Tarefa>> GetConcluidasEntreDatasAsync(int utilizadorId, DateTime inicio, DateTime fim);
    Task<IEnumerable<Tarefa>> GetEmCursoAsync(int utilizadorId);
    Task AddAsync(Tarefa tarefa);
    Task UpdateAsync(Tarefa tarefa);
    Task DeleteAsync(int id);
}