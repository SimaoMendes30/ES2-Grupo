using Backend.Domain.DTOs.Member;
using Backend.Domain.DTOs.Project;
using Backend.Domain.DTOs.Task;

namespace Backend.Domain.DTOs.User
{
    public class UserDetailsExtendedDto : UserDetailsDto
    {
        public List<MemberDetailsDto> Membros { get; set; } = new();

        public List<ProjectDetailsDto> Projetos { get; set; } = new();

        public List<TaskDetailsDto> Tarefas { get; set; } = new();
    }
}