using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.User;
using Backend.Models;

namespace Backend.Features.User.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByIdAsync(int id);
        Task<UserEntity?> GetByUsernameAsync(string username);
        Task<IEnumerable<UserEntity>> FilteredListAsync(UserFilterDto filter);
        Task<PagedResult<UserEntity>> FilteredPagedAsync(UserFilterDto filter, int page, int pageSize);
        System.Threading.Tasks.Task AddAsync(UserEntity user);
        System.Threading.Tasks.Task UpdateAsync(UserEntity user);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}

