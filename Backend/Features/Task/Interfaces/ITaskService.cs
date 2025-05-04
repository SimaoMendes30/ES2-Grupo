using Backend.Domain.DTOs.Task;
using Backend.Domain.DTOs.Common;

namespace Backend.Features.Task.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDetailsDto> CreateAsync(TaskCreateDto dto);
        Task<IEnumerable<TaskDetailsDto>> FilteredListAsync(TaskFilterDto filter);
        Task<PagedResult<TaskDetailsDto>> FilteredPagedAsync(TaskFilterDto filter, int page, int pageSize);
        Task<TaskDetailsExtendedDto> GetByIdAsync(int id);
        System.Threading.Tasks.Task UpdateAsync(int id, TaskUpdateDto dto);
        System.Threading.Tasks.Task DeleteAsync(int id);

        Task<PagedResult<TaskDetailsDto>> GetByUserPagedAsync(int userId, int page, int pageSize, string?
            orderBy = null, bool descending = false, TaskFilterDto filters = null);
        Task<IEnumerable<TaskDetailsDto>> GetByProjetoIdAsync(int projetoId);
    }
}



