using Backend.Domain.DTOs.Member;
using Backend.Domain.DTOs.User;

namespace Backend.Domain.DTOs.Project
{
    public class ProjectDetailsExtendedDto : ProjectDetailsDto
    {
        public UserDetailsDto ResponsavelNavigation { get; set; } = null!;

        public List<MemberDetailsDto> Membros { get; set; } = new();
    }
}