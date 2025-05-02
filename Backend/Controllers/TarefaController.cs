using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.DTOs.Relatorios;
using Backend.DTOs.Tarefas;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _service;
        public TarefaController(ITarefaService service) => _service = service;

        // RF08-11
        [HttpPost("iniciar")]
        public async Task<ActionResult<TarefaDto>> Iniciar([FromBody] StartTarefaDto dto) =>
            Ok(await _service.StartAsync(dto));

        // RF12-13
        [HttpPost("{id:int}/terminar")]
        public async Task<ActionResult<TarefaDto>> Terminar(int id, [FromBody] EndTarefaDto dto) =>
            Ok(await _service.EndAsync(id, dto));

        // RF07
        [HttpPatch("{id:int}/mover/{destinoId:int}")]
        public async Task<IActionResult> Mover(int id, int destinoId)
        {
            await _service.MoveAsync(id, destinoId);
            return NoContent();
        }

        // RF16
        [HttpGet("emcurso/{utilizadorId:int}")]
        public async Task<IEnumerable<TarefaDto>> EmCurso(int utilizadorId) =>
            await _service.ListarEmCursoAsync(utilizadorId);

        // RF17
        [HttpGet("concluidas/{utilizadorId:int}")]
        public async Task<IEnumerable<TarefaDto>> Concluidas(int utilizadorId, DateTime inicio, DateTime fim) =>
            await _service.ListarConcluidasAsync(utilizadorId, inicio, fim);

        // RF14-15
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // RF23-27
        [HttpGet("relatorio/{utilizadorId:int}/{ano:int}/{mes:int}")]
        public async Task<RelatorioMensalDto> Relatorio(int utilizadorId, int ano, int mes) =>
            await _service.RelatorioMensalAsync(utilizadorId, ano, mes);

        // RF28
        [HttpGet("relatorio-projeto/{utilizadorId:int}/{ano:int}/{mes:int}")]
        public async Task<IEnumerable<RelatorioProjetoClienteDto>> RelatorioProjeto(
            int utilizadorId, int ano, int mes) =>
            await _service.RelatorioPorProjetoClienteAsync(utilizadorId, ano, mes);

        // Auxiliares
        [HttpGet("projeto/{projetoId:int}")]
        public async Task<IEnumerable<TarefaDto>> PorProjeto(int projetoId) =>
            await _service.GetByProjetoIdAsync(projetoId);

        [HttpGet("utilizador/{utilizadorId:int}")]
        public async Task<IEnumerable<TarefaDto>> PorUtilizador(int utilizadorId) =>
            await _service.GetByUtilizadorIdAsync(utilizadorId);
    }
}
