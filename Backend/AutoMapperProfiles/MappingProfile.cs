using AutoMapper;
using Backend.Models;
using Backend.DTOs.Utilizadores;
using Backend.DTOs.Projetos;
using Backend.DTOs.Tarefas;
using Backend.DTOs.Membros;
using Backend.DTOs.Relatorios;

namespace Backend.AutoMapperProfiles;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // UTILIZADOR
        CreateMap<Utilizador, UtilizadorDto>().ReverseMap();
        CreateMap<Utilizador, UtilizadorDetailsDto>()
            .ForMember(d => d.Membros, o => o.MapFrom(s => s.Membros))
            .ForMember(d => d.Projetos, o => o.MapFrom(s => s.Projetos))
            .ForMember(d => d.IdTarefas, o => o.MapFrom(s => s.Tarefas))
            .ReverseMap();
        CreateMap<UtilizadorCreateDto, Utilizador>();
        CreateMap<UtilizadorUpdateDto, Utilizador>();
        CreateMap<UtilizadorLoginDto, Utilizador>();
        CreateMap<Utilizador, UtilizadorTokenDto>();

        // PROJETO
        CreateMap<Projeto, ProjetoDto>()
            .ForMember(d => d.Responsavel, o => o.MapFrom(s => s.Responsavel))
            .ReverseMap();
        CreateMap<Projeto, ProjetoDetailsDto>()
            .ForMember(d => d.Membros, o => o.MapFrom(s => s.Membros))
            .ForMember(d => d.Tarefas, o => o.MapFrom(s => s.IdTarefas))
            .ForMember(d => d.ResponsavelUtilizador, o => o.MapFrom(s => s.ResponsavelNavigation))
            .ReverseMap();
        CreateMap<ProjetoCreateDto, Projeto>()
            .ForMember(d => d.Responsavel, o => o.MapFrom(s => s.Responsavel));
        CreateMap<UpdateProjetoDto, Projeto>()
            .ForAllMembers(o => o.Condition((src, dest, val) => val != null));

        // TAREFA
        CreateMap<Tarefa, TarefaDto>().ReverseMap();
        CreateMap<Tarefa, TarefaDetailsDto>()
            .ForMember(d => d.Projetos, o => o.MapFrom(s => s.IdProjetos))
            .ForMember(d => d.Utilizadores, o => o.MapFrom(s => s.IdUtilizadors))
            .ReverseMap();
        CreateMap<StartTarefaDto, Tarefa>()
            .ForMember(d => d.Titulo, o => o.MapFrom(s => s.Titulo))
            .ForMember(d => d.Descricao, o => o.MapFrom(s => s.Descricao))
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio ?? DateTime.UtcNow));
        CreateMap<EndTarefaDto, Tarefa>();

        // MEMBRO
        CreateMap<Membro, MembroDto>().ReverseMap();
        CreateMap<Membro, MembroDetailsDto>()
            .ForMember(d => d.Projeto, o => o.MapFrom(s => s.IdProjetoNavigation))
            .ForMember(d => d.Utilizador, o => o.MapFrom(s => s.IdUtilizadorNavigation))
            .ReverseMap();
        CreateMap<CreateMembroDto, Membro>()
            .ForMember(d => d.IdProjeto, o => o.MapFrom(s => s.ProjetoId))
            .ForMember(d => d.IdUtilizador, o => o.MapFrom(s => s.UtilizadorId));

        // RELATÓRIO (por projeção)
        CreateMap<Tarefa, DiaRelatorioDto>()
            .ForMember(d => d.Dia, o => o.MapFrom(s => s.DataFim!.Value))
            .ForMember(d => d.TotalHoras, o => o.Ignore())
            .ForMember(d => d.TotalCusto, o => o.Ignore())
            .ForMember(d => d.Projetos, o => o.Ignore());
    }
}