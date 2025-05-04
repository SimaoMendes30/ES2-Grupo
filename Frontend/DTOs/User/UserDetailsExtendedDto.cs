using Frontend.DTOs.Member;
using Frontend.DTOs.Project;
using Frontend.DTOs.Task;

namespace Frontend.DTOs.User
{
    public class UserDetailsExtendedDto : UserDetailsDto
    {
        public List<MemberDetailsDto> Membros { get; set; } = new();

        public List<ProjectDetailsDto> Projetos { get; set; } = new();

        public List<TaskDetailsDto> Tarefas { get; set; } = new();
    }
}