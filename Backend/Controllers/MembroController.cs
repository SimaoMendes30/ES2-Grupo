using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.DTOs.Membros;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]                        // JWT obrigatório
    [ApiController]
    [Route("api/[controller]")]
    public sealed class MembroController : ControllerBase
    {
        private readonly IMembroService _service;
        public MembroController(IMembroService service) => _service = service;

        // RF18
        [HttpGet("projeto/{projetoId:int}")]
        public async Task<IEnumerable<MembroDto>> PorProjeto(int projetoId) =>
            await _service.GetByProjetoAsync(projetoId);

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateMembroDto dto)
        {
            await _service.AddAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // RF19-21
        [HttpPatch("{membroId:int}/convite")]
        public async Task<IActionResult> ResponderConvite(int membroId, [FromBody] ConviteRespostaDto dto)
        {
            await _service.ResponderConviteAsync(membroId, dto.Aceitar);
            return NoContent();
        }
    }
}