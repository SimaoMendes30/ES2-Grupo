using Backend.Domain.DTOs.Project;
using Backend.Domain.DTOs.User;

namespace Backend.Domain.DTOs.Member
{
    public class MemberDetailsExtendedDto : MemberDetailsDto
    {
        public UserDetailsDto IdUserNavigation { get; set; } = null!;
        public ProjectDetailsDto IdProjectNavigation { get; set; } = null!;
    }
}