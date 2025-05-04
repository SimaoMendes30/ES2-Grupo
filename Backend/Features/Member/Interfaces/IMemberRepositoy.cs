using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Member;
using Backend.Models;

namespace Backend.Features.Member.Interfaces
{
    public interface IMemberRepository
    {
        Task<MemberEntity?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(MemberEntity member);
        System.Threading.Tasks.Task UpdateAsync(MemberEntity member);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<IEnumerable<MemberEntity>> FilteredListAsync(MemberFilterDto filter);
        Task<PagedResult<MemberEntity>> FilteredPagedAsync(MemberFilterDto filter, int page, int pageSize);
    }
}

