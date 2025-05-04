using Backend.Domain.DTOs.Project;
using Backend.Domain.DTOs.Common;

namespace Backend.Features.Project.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDetailsDto> CreateAsync(ProjectCreateDto dto);
        Task<IEnumerable<ProjectDetailsDto>> FilteredListAsync(ProjectFilterDto filter);
        Task<PagedResult<ProjectDetailsDto>> FilteredPagedAsync(ProjectFilterDto filter, int page, int pageSize);
        Task<ProjectDetailsExtendedDto> GetByIdAsync(int id);
        System.Threading.Tasks.Task UpdateAsync(int id, ProjectUpdateDto dto);
        System.Threading.Tasks.Task DeleteAsync(int id);

        Task<PagedResult<ProjectDetailsDto>> GetByUserPagedAsync(int userId, int page, int pageSize, string?
            orderBy = null, bool descending = false, ProjectFilterDto? filtros = null);
    }
}