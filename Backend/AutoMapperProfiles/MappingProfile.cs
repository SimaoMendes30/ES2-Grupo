using AutoMapper;
using Backend.Models;

// DTO namespaces
using Backend.DTOs.Utilizadores;
using Backend.DTOs.Projeto;
using Backend.DTOs.Tarefas;
using Backend.DTOs.Membros;
using Backend.DTOs.Relatorios;

namespace Backend.AutoMapperProfiles;

/// <summary>
/// Configuração completa do AutoMapper para todas as entidades e DTOs do backend.
/// </summary>
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ------------------------------------------------------------------
        // UTILIZADOR
        // ------------------------------------------------------------------
        CreateMap<Utilizador, UtilizadorDto>()
            .ReverseMap();

        CreateMap<Utilizador, UtilizadorDetailsDto>()
            .ForMember(d => d.Membros   , o => o.MapFrom(s => s.Membros))
            .ForMember(d => d.Projetos  , o => o.MapFrom(s => s.Projetos))
            .ForMember(d => d.IdTarefas , o => o.MapFrom(s => s.IdTarefas))
            .ReverseMap();

        CreateMap<UtilizadorCreateDto , Utilizador>();
        CreateMap<UtilizadorUpdateDto , Utilizador>();
        CreateMap<UtilizadorUpdatePasswordDto, Utilizador>();

        // Mapeamento para login/geração de token (somente quando útil internamente)
        CreateMap<UtilizadorLoginDto , Utilizador>();
        CreateMap<Utilizador        , UtilizadorTokenDto>();

        // ------------------------------------------------------------------
        // PROJETO
        // ------------------------------------------------------------------
        CreateMap<Projeto, ProjetoDto>()
            .ReverseMap();

        CreateMap<Projeto, ProjetoDetailsDto>()
            .ForMember(d => d.Membros   , o => o.MapFrom(s => s.Membros))
            .ForMember(d => d.Tarefas   , o => o.MapFrom(s => s.IdTarefas))
            .ForMember(d => d.Utilizador, o => o.MapFrom(s => s.IdUtilizadorNavigation))
            .ReverseMap();

        CreateMap<ProjetoCreateDto , Projeto>()
            .ReverseMap();
        CreateMap<UpdateProjetoDto, Projeto>()
            .ForAllMembers(o => o.Condition((src, dest, val) => val != null)); // patch‑like

        // ------------------------------------------------------------------
        // TAREFA
        // ------------------------------------------------------------------
        CreateMap<Tarefa, TarefaDto>()
            .ReverseMap();

        CreateMap<Tarefa, TarefaDetailsDto>()
            .ForMember(d => d.Projetos    , o => o.MapFrom(s => s.IdProjetos))
            .ForMember(d => d.Utilizadores, o => o.MapFrom(s => s.IdUtilizadors))
            .ReverseMap();

        // Entrada (não mapeamos de volta)
        CreateMap<StartTarefaDto, Tarefa>();
        CreateMap<EndTarefaDto  , Tarefa>();

        // ------------------------------------------------------------------
        // MEMBRO
        // ------------------------------------------------------------------
        CreateMap<Membro, MembroDto>()
            .ReverseMap();

        CreateMap<Membro, MembroDetailsDto>()
            .ForMember(d => d.Projeto   , o => o.MapFrom(s => s.IdProjetoNavigation))
            .ForMember(d => d.Utilizador, o => o.MapFrom(s => s.IdUtilizadorNavigation))
            .ReverseMap();

        CreateMap<CreateMembroDto, Membro>()
            .ForMember(d => d.IdProjeto   , o => o.MapFrom(s => s.ProjetoId))
            .ForMember(d => d.IdUtilizador, o => o.MapFrom(s => s.UtilizadorId));

        // ------------------------------------------------------------------
        // RELATÓRIOS (somente entity -> DTO se necessário)
        // ------------------------------------------------------------------
        CreateMap<Tarefa, DiaRelatorioDto>()   // caso deseje projetar com AutoMapper Projection
            .ForMember(d => d.Dia  , o => o.MapFrom(s => s.DataFim!.Value.ToDateTime(TimeOnly.MinValue)))
            .ForMember(d => d.TotalHoras, o => o.Ignore())
            .ForMember(d => d.TotalCusto, o => o.Ignore())
            .ForMember(d => d.Projetos, o => o.Ignore());
    }
}