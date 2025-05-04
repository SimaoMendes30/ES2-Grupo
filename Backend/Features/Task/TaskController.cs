using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Task;
using Backend.Features.Task.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.Task;

[ApiController]
[Route("api/tarefas")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<TaskDetailsDto>>> GetPaged([FromQuery] TaskFilterDto filter, int page = 1, int pageSize = 10)
    {
        var result = await _service.FilteredPagedAsync(filter, page, pageSize);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDetailsDto>>> GetAll([FromQuery] TaskFilterDto filter)
    {
        var result = await _service.FilteredListAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDetailsExtendedDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDetailsDto>> Create(TaskCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.IdTarefa }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet("utilizador/{userId}/paged")]
    public async Task<ActionResult<PagedResult<TaskDetailsDto>>> GetByUserPaged(
        int userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 15,
        [FromQuery] string? orderBy = null,
        [FromQuery] bool descending = false,
        [FromQuery] string? titulo = null,
        [FromQuery] int? responsavel = null,
        [FromQuery] DateTime? dataInicioDe = null,
        [FromQuery] DateTime? dataInicioAte = null,
        [FromQuery] DateTime? dataFimDe = null,
        [FromQuery] DateTime? dataFimAte = null,
        [FromQuery] string? estado = null,
        [FromQuery] decimal? precoHoraMin = null,
        [FromQuery] decimal? precoHoraMax = null,
        [FromQuery] bool? isDeleted = null)
    {
        var filters = new TaskFilterDto
        {
            Titulo = titulo,
            Responsavel = responsavel,
            DataInicioDe = dataInicioDe,
            DataInicioAte = dataInicioAte,
            DataFimDe = dataFimDe,
            DataFimAte = dataFimAte,
            Estado = estado,
            PrecoHoraMin = precoHoraMin,
            PrecoHoraMax = precoHoraMax,
            IsDeleted = isDeleted
        };

        var result = await _service.GetByUserPagedAsync(userId, page, pageSize, orderBy, descending, filters);
        return Ok(result);
    }
    
    [HttpGet("projeto/{projetoId}")]
    public async Task<ActionResult<IEnumerable<TaskDetailsDto>>> GetByProjetoId(int projetoId)
    {
        var result = await _service.GetByProjetoIdAsync(projetoId);
        return Ok(result);
    }
}