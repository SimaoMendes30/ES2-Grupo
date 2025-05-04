using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Task;
using Backend.Models;

namespace Backend.Features.Task.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TaskEntity>> FilteredListAsync(TaskFilterDto filter);
        System.Threading.Tasks.Task AddAsync(TaskEntity task);
        System.Threading.Tasks.Task UpdateAsync(TaskEntity task);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<PagedResult<TaskEntity>> FilteredPagedAsync(TaskFilterDto filter, int page, int pageSize);
        Task<List<int>> GetTarefaIdsByUtilizadorAsync(int userId);
        Task<List<TaskEntity>> GetByProjetoIdAsync(int projetoId);
    }
}

