using Frontend.DTOs.Project;
using Frontend.DTOs.User;

namespace Frontend.DTOs.Member
{
    public class MemberDetailsExtendedDto : MemberDetailsDto
    {
        public UserDetailsDto IdUserNavigation { get; set; } = null!;
        public ProjectDetailsDto IdProjectNavigation { get; set; } = null!;
    }
}