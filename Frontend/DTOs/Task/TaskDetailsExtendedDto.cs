using Frontend.DTOs.Project;
using Frontend.DTOs.User;

namespace Frontend.DTOs.Task
{
    public class TaskDetailsExtendedDto : TaskDetailsDto
    {
        public UserDetailsDto ResponsavelNavigation { get; set; } = null!;

        public List<ProjectDetailsDto> Projetos { get; set; } = new();

        public List<UserDetailsDto> Utilizadores { get; set; } = new();
    }
}