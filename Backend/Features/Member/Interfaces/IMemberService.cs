using Backend.Domain.DTOs.Member;
using Backend.Domain.DTOs.Common;

namespace Backend.Features.Member.Interfaces
{
    public interface IMemberService
    {
        Task<MemberDetailsDto> CreateAsync(MemberCreateDto dto);
        Task<IEnumerable<MemberDetailsDto>> FilteredListAsync(MemberFilterDto filter);
        Task<PagedResult<MemberDetailsDto>> FilteredPagedAsync(MemberFilterDto filter, int page, int pageSize);
        Task<MemberDetailsExtendedDto> GetByIdAsync(int id);
        System.Threading.Tasks.Task UpdateAsync(int id, MemberUpdateDto dto);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}




