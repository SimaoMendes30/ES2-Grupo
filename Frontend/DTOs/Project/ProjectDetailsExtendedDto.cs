using Frontend.DTOs.Member;
using Frontend.DTOs.User;

namespace Frontend.DTOs.Project
{
    public class ProjectDetailsExtendedDto : ProjectDetailsDto
    {
        public UserDetailsDto ResponsavelNavigation { get; set; } = null!;

        public List<MemberDetailsDto> Membros { get; set; } = new();
    }
}