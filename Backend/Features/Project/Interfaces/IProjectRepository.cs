using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Project;
using Backend.Models;

namespace Backend.Features.Project.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectEntity?> GetByIdAsync(int id);
        Task<IEnumerable<ProjectEntity>> FilteredListAsync(ProjectFilterDto filter);
        System.Threading.Tasks.Task AddAsync(ProjectEntity project);
        System.Threading.Tasks.Task UpdateAsync(ProjectEntity project);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<PagedResult<ProjectEntity>> FilteredPagedAsync(ProjectFilterDto filter, int page, int pageSize);
    }
}

