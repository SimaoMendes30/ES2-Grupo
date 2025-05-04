using Backend.Domain.DTOs.Common;
using Backend.Domain.DTOs.Member;
using Backend.Features.Member.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.Member;

[ApiController]
[Route("api/membros")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _service;

    public MemberController(IMemberService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<MemberDetailsDto>>> GetPaged([FromQuery] MemberFilterDto filter, int page = 1, int pageSize = 10)
    {
        var result = await _service.FilteredPagedAsync(filter, page, pageSize);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDetailsDto>>> GetAll([FromQuery] MemberFilterDto filter)
    {
        var result = await _service.FilteredListAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDetailsExtendedDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MemberDetailsDto>> Create(MemberCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.IdUtilizador }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MemberUpdateDto dto)
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
}