using AutoMapper;
using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Task;
using Backend.Domain.Patterns.Specifications;
using Backend.Features.Task.Interfaces;
using Backend.Models;
using TaskFactory = Backend.Domain.Patterns.Factories.TaskFactory;

namespace Backend.Features.Task;

public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ITaskRepository repo, IMapper mapper, ILogger<TaskService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TaskDetailsDto> CreateAsync(TaskCreateDto dto)
    {
        var entity = TaskFactory.Create(
            responsavelId: dto.Responsavel,
            titulo: dto.Titulo,
            dataInicio: dto.DataInicio,
            dataFim: dto.DataFim,
            estado: dto.Estado,
            precoHora: dto.PrecoHora
        );

        await _repo.AddAsync(entity);
        return _mapper.Map<TaskDetailsDto>(entity);
    }

    public async Task<IEnumerable<TaskDetailsDto>> FilteredListAsync(TaskFilterDto filter)
    {
        var list = await _repo.FilteredListAsync(filter);
        return _mapper.Map<IEnumerable<TaskDetailsDto>>(list);
    }

    public async Task<PagedResult<TaskDetailsDto>> FilteredPagedAsync(TaskFilterDto filter, int page, int pageSize)
    {
        var result = await _repo.FilteredPagedAsync(filter, page, pageSize);
        return new PagedResult<TaskDetailsDto>
        {
            Items = _mapper.Map<IEnumerable<TaskDetailsDto>>(result.Items),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
    }

    public async Task<TaskDetailsExtendedDto> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Tarefa {id} não encontrada");

        return _mapper.Map<TaskDetailsExtendedDto>(entity);
    }

    public async System.Threading.Tasks.Task UpdateAsync(int id, TaskUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Tarefa {id} não encontrada");

        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
    
    public async Task<PagedResult<TaskDetailsDto>> GetByUserPagedAsync(int userId, int page, int pageSize, string? orderBy = null, bool descending = false, TaskFilterDto filters = null)
    {
        var tarefasResponsavel = await _repo.FilteredListAsync(new TaskFilterDto { Responsavel = userId });
        
        var idsTarefasLigadas = await _repo.GetTarefaIdsByUtilizadorAsync(userId);

        var tarefasLigadas = idsTarefasLigadas.Any() ? await _repo.FilteredListAsync(new TaskFilterDto { IdsTarefas = idsTarefasLigadas }) : new List<TaskEntity>();
        
        var todas = tarefasResponsavel
            .Concat(tarefasLigadas)
            .GroupBy(t => t.IdTarefa)
            .Select(g => g.First())
            .AsQueryable()
            .Where(new TaskByFilterSpec(filters ?? new TaskFilterDto()).ToExpression());
        
        todas = (orderBy?.ToLower()) switch
        {
            "descricao" => descending ? todas.OrderByDescending(t => t.Descricao) : todas.OrderBy(t => t.Descricao),
            "dataInicio" => descending ? todas.OrderByDescending(t => t.DataInicio) : todas.OrderBy(t => t.DataInicio),
            "estado" => descending ? todas.OrderByDescending(t => t.Estado) : todas.OrderBy(t => t.Estado),
            _ => descending ? todas.OrderByDescending(t => t.IdTarefa) : todas.OrderBy(t => t.IdTarefa) // default
        };
        
        var totalCount = todas.Count();
        var pagedItems = todas
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<TaskDetailsDto>
        {
            Items = _mapper.Map<List<TaskDetailsDto>>(pagedItems),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
    
    public async Task<IEnumerable<TaskDetailsDto>> GetByProjetoIdAsync(int projetoId)
    {
        var tarefas = await _repo.GetByProjetoIdAsync(projetoId);
        return _mapper.Map<List<TaskDetailsDto>>(tarefas);
    }

}
