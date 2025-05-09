using AutoMapper;
using Backend.Domain.DTOs.Member;
using Backend.Domain.DTOs.Project;
using Backend.Domain.DTOs.Task;
using Backend.Domain.DTOs.User;
using Backend.Models;

namespace Backend.Dtos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Member
            CreateMap<MemberEntity, MemberCreateDto>().ReverseMap();
            CreateMap<MemberEntity, MemberDetailsDto>().ReverseMap();
            CreateMap<MemberEntity, MemberDetailsExtendedDto>().ReverseMap();
            CreateMap<MemberEntity, MemberUpdateDto>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Project
            CreateMap<ProjectEntity, ProjectCreateDto>().ReverseMap();
            CreateMap<ProjectEntity, ProjectDetailsDto>().ReverseMap();
            CreateMap<ProjectEntity, ProjectDetailsExtendedDto>().ReverseMap();
            CreateMap<ProjectEntity, ProjectUpdateDto>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task
            CreateMap<TaskEntity, TaskCreateDto>().ReverseMap();
            CreateMap<TaskEntity, TaskDetailsDto>().ReverseMap();
            CreateMap<TaskEntity, TaskDetailsExtendedDto>().ReverseMap();
            CreateMap<TaskEntity, TaskUpdateDto>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // User
            CreateMap<UserEntity, UserCreateDto>().ReverseMap();
            CreateMap<UserEntity, UserDetailsDto>().ReverseMap();
            CreateMap<UserEntity, UserDetailsExtendedDto>().ReverseMap();
            CreateMap<UserEntity, UserUpdateDto>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}