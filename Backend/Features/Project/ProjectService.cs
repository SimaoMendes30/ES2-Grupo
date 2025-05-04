using AutoMapper;
using Backend.Models;
using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Member;
using Backend.Domain.DTOs.Project;
using Backend.Domain.Patterns.Specifications;
using Backend.Features.Member.Interfaces;
using Backend.Features.Project.Interfaces;

namespace Backend.Features.Project;

public sealed class ProjectService : IProjectService
{
    private readonly IProjectRepository _repoProject;
    private readonly IMemberRepository _repoMember;
    private readonly IMapper _mapper;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(IProjectRepository repoProject, IMapper mapper, ILogger<ProjectService> logger, IMemberRepository repoMember)
    {
        _repoProject = repoProject;
        _mapper = mapper;
        _logger = logger;
        _repoMember = repoMember;
    }

    public async Task<ProjectDetailsDto> CreateAsync(ProjectCreateDto dto)
    {
        var entity = _mapper.Map<ProjectEntity>(dto);
        await _repoProject.AddAsync(entity);
        return _mapper.Map<ProjectDetailsDto>(entity);
    }

    public async Task<IEnumerable<ProjectDetailsDto>> FilteredListAsync(ProjectFilterDto filter)
    {
        var list = await _repoProject.FilteredListAsync(filter);
        return _mapper.Map<IEnumerable<ProjectDetailsDto>>(list);
    }

    public async Task<PagedResult<ProjectDetailsDto>> FilteredPagedAsync(ProjectFilterDto filter, int page, int pageSize)
    {
        var result = await _repoProject.FilteredPagedAsync(filter, page, pageSize);
        return new PagedResult<ProjectDetailsDto>
        {
            Items = _mapper.Map<IEnumerable<ProjectDetailsDto>>(result.Items),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
    }

    public async Task<ProjectDetailsExtendedDto> GetByIdAsync(int id)
    {
        var entity = await _repoProject.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Projeto {id} não encontrado");

        return _mapper.Map<ProjectDetailsExtendedDto>(entity);
    }

    public async System.Threading.Tasks.Task UpdateAsync(int id, ProjectUpdateDto dto)
    {
        var entity = await _repoProject.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Projeto {id} não encontrado");

        _mapper.Map(dto, entity);
        await _repoProject.UpdateAsync(entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await _repoProject.DeleteAsync(id);
    }
    
    public async Task<PagedResult<ProjectDetailsDto>> GetByUserPagedAsync(int userId, int page, int pageSize, string? orderBy = null, bool descending = false, ProjectFilterDto? filtros = null)
    {
        var projetosResponsavel = await _repoProject.FilteredListAsync(new ProjectFilterDto
        {
            Responsavel = userId
        });
        
        var membros = await _repoMember.FilteredListAsync(new MemberFilterDto
        {
            IdUtilizador = userId
        });

        var idsProjetosMembro = membros.Select(m => m.IdProjeto).Distinct().ToList();

        var projetosMembro = idsProjetosMembro.Any()
            ? await _repoProject.FilteredListAsync(new ProjectFilterDto
            {
                IdsProjetos = idsProjetosMembro
            })
            : new List<ProjectEntity>();
        
        var todos = projetosResponsavel
            .Concat(projetosMembro)
            .GroupBy(p => p.IdProjeto)
            .Select(g => g.First())
            .AsQueryable()
            .Where(new ProjectByFilterSpec(filtros ?? new ProjectFilterDto()).ToExpression());

        
        todos = (orderBy?.ToLower()) switch
        {
            "nome" => descending ? todos.OrderByDescending(p => p.Nome) : todos.OrderBy(p => p.Nome),
            "datacriacao" => descending ? todos.OrderByDescending(p => p.DataCriacao) : todos.OrderBy(p => p.DataCriacao),
            "cliente" => descending ? todos.OrderByDescending(p => p.NomeCliente) : todos.OrderBy(p => p.NomeCliente),
            _ => descending ? todos.OrderByDescending(p => p.IdProjeto) : todos.OrderBy(p => p.IdProjeto) // default
        };
        
        var totalCount = todos.Count();
        var pagedItems = todos
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<ProjectDetailsDto>
        {
            Items = _mapper.Map<List<ProjectDetailsDto>>(pagedItems),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}