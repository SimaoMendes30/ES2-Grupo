namespace Backend.Features.User.Interfaces;

using Backend.Domain.DTOs.User;
using Backend.Domain.DTOs.Common;

public interface IUserService
{
    Task<UserTokenDto> GenerateTokenAsync(UserLoginDto loginDto);
    Task<UserDetailsDto> CreateAsync(UserCreateDto dto);
    Task<IEnumerable<UserDetailsDto>> FilteredListAsync(UserFilterDto filter);
    Task<PagedResult<UserDetailsDto>> FilteredPagedAsync(UserFilterDto filter, int page, int pageSize);
    Task<UserDetailsExtendedDto> GetByIdAsync(int id);
    System.Threading.Tasks.Task UpdateAsync(int id, UserUpdateDto dto);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
