using Backend.Domain.DTOs.Project;
using Backend.Domain.DTOs.User;

namespace Backend.Domain.DTOs.Task
{
    public class TaskDetailsExtendedDto : TaskDetailsDto
    {
        public UserDetailsDto ResponsavelNavigation { get; set; } = null!;

        public List<ProjectDetailsDto> Projetos { get; set; } = new();

        public List<UserDetailsDto> Utilizadores { get; set; } = new();
    }
}