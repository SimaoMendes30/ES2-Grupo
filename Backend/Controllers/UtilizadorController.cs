using Backend.DTOs.Utilizadores;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UtilizadorController : ControllerBase
{
    private readonly IUtilizadorService _svc;
    private readonly ILogger<UtilizadorController> _log;

    public UtilizadorController(IUtilizadorService svc, ILogger<UtilizadorController> log)
    {
        _svc = svc;
        _log = log;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UtilizadorTokenDto>> Login([FromBody] UtilizadorLoginDto dto)
    {
        var token = await _svc.GerarTokenAsync(dto);
        return Ok(token);
    }

    [HttpPost("criar")]
    public async Task<ActionResult<UtilizadorDto>> Criar([FromBody] UtilizadorCreateDto dto)
    {
        var novo = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = novo.IdUtilizador }, novo);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilizadorDto>>> GetAll()
    {
        var list = await _svc.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UtilizadorDto>> GetById(int id)
    {
        var user = await _svc.GetByIdAsync(id);
        return Ok(user);
    }

    [HttpGet("{id:int}/details")]
    public async Task<ActionResult<UtilizadorDetailsDto>> GetDetails(int id)
    {
        var details = await _svc.GetDetailsAsync(id);
        return Ok(details);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UtilizadorUpdateDto dto)
    {
        await _svc.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id:int}/password")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] UtilizadorUpdatePasswordDto dto)
    {
        await _svc.UpdatePasswordAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}