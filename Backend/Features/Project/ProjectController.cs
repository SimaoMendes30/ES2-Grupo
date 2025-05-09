using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Project;
using Backend.Features.Project.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.Project;

[ApiController]
[Route("api/projetos")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectController(IProjectService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<ProjectDetailsDto>>> GetPaged([FromQuery] ProjectFilterDto filter, int page = 1, int pageSize = 10)
    {
        var result = await _service.FilteredPagedAsync(filter, page, pageSize);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDetailsDto>>> GetAll([FromQuery] ProjectFilterDto filter)
    {
        var result = await _service.FilteredListAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDetailsExtendedDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDetailsDto>> Create(ProjectCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.IdProjeto }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProjectUpdateDto dto)
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
    public async Task<ActionResult<PagedResult<ProjectDetailsDto>>> GetByUserPaged(
        int userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 15,
        [FromQuery] string? orderBy = null,
        [FromQuery] bool descending = false,
        [FromQuery] string? nome = null,
        [FromQuery] string? nomeCliente = null,
        [FromQuery] DateTimeOffset? dataCriacaoDe = null,
        [FromQuery] DateTimeOffset? dataCriacaoAte = null,
        [FromQuery] decimal? precoHoraMin = null,
        [FromQuery] decimal? precoHoraMax = null,
        [FromQuery] bool? isDeleted = false)
    {
        var filtro = new ProjectFilterDto
        {
            Nome = nome,
            NomeCliente = nomeCliente,
            DataCriacaoDe = dataCriacaoDe,
            DataCriacaoAte = dataCriacaoAte,
            PrecoHoraMin = precoHoraMin,
            PrecoHoraMax = precoHoraMax,
            IsDeleted = isDeleted
        };

        var result = await _service.GetByUserPagedAsync(userId, page, pageSize, orderBy, descending, filtro);
        return Ok(result);
    }
}