using Backend.DTOs.Projeto;
using Backend.DTOs.Tarefas;
using Backend.DTOs.Membros;
using Backend.DTOs.Relatorios;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProjetoController : ControllerBase
{
    private readonly IProjetoService _service;
    public ProjetoController(IProjetoService service) => _service = service;

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjetoDto>> Get(int id) => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProjetoCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Post), null);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjetoDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id:int}/convidar")]
    public async Task<IActionResult> Convidar(int id, [FromQuery] string username)
    {
        await _service.ConvidarUtilizadorAsync(id, username);
        return Ok();
    }

    [HttpDelete("membro/{membroId:int}")]
    public async Task<IActionResult> RemoverMembro(int membroId)
    {
        await _service.RemoverMembroAsync(membroId);
        return NoContent();
    }
    
    [HttpGet("utilizador/{utilizadorId:int}")]
    public async Task<ActionResult<IEnumerable<ProjetoDto>>> PorUtilizador(int utilizadorId)
    {
        var projetos = await _service.GetByUtilizadorAsync(utilizadorId);
        return Ok(projetos);
    }

}