using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Domain.Builders;
using Backend.Domain.Strategies;
using Backend.DTOs.Relatorios;
using Backend.DTOs.Tarefas;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Backend.Models;
using Microsoft.Extensions.Logging;

namespace Backend.Services
{
    public sealed class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository      _repo;
        private readonly IProjetoRepository     _projetoRepo;
        private readonly IMapper                _mapper;
        private readonly IPrecoTarefaStrategy   _precoStrategy;
        private readonly ILogger<TarefaService> _logger;

        public TarefaService(
            ITarefaRepository      repo,
            IProjetoRepository     projetoRepo,
            IMapper                mapper,
            IPrecoTarefaStrategy   precoStrategy,
            ILogger<TarefaService> logger)
        {
            _repo          = repo;
            _projetoRepo   = projetoRepo;
            _mapper        = mapper;
            _precoStrategy = precoStrategy;
            _logger        = logger;
        }

        // ───────────────────────── Helpers ─────────────────────────
        private static decimal Horas(DateTime inicio, DateOnly fim) =>
            (decimal)(fim.ToDateTime(TimeOnly.MinValue) - inicio).TotalHours;

        private static decimal Horas(DateTime inicio) =>
            (decimal)(DateTime.UtcNow - inicio).TotalHours;

        // ─────────────── CRUD / RF08 – RF13 ───────────────
        public async Task<TarefaDto> StartAsync(StartTarefaDto dto)
        {
            var projeto = await _projetoRepo.GetByIdAsync(dto.ProjetoId);

            var tarefa = new Tarefa
            {
                Descricao      = dto.Descricao,
                Responsavel    = dto.Responsavel,
                DataHoraInicio = dto.DataHoraInicio ?? DateTime.UtcNow,
                DataInicio     = DateOnly.FromDateTime((dto.DataHoraInicio ?? DateTime.UtcNow).Date),
                Estado         = "Em Curso",
                PrecoHora      = dto.PrecoHora,
            };
            tarefa.IdProjetos.Add(projeto);

            await _repo.AddAsync(tarefa);
            return _mapper.Map<TarefaDto>(tarefa);
        }

        public async Task<TarefaDto> EndAsync(int id, EndTarefaDto dto)
        {
            var tarefa = await _repo.GetByIdAsync(id)
                         ?? throw new KeyNotFoundException("Tarefa não encontrada.");

            if (tarefa.DataFim != null)
                throw new InvalidOperationException("Tarefa já concluída.");

            tarefa.DataFim = dto.DataFim.HasValue
                ? DateOnly.FromDateTime(dto.DataFim.Value)
                : DateOnly.FromDateTime(DateTime.UtcNow);

            tarefa.Estado = "Concluída";

            await _repo.UpdateAsync(tarefa);
            return _mapper.Map<TarefaDto>(tarefa);
        }

        public async Task MoveAsync(int tarefaId, int projetoDestinoId)
        {
            var tarefa  = await _repo.GetByIdAsync(tarefaId);
            var destino = await _projetoRepo.GetByIdAsync(projetoDestinoId);

            tarefa.IdProjetos.Clear();
            tarefa.IdProjetos.Add(destino);

            await _repo.UpdateAsync(tarefa);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);   // RF14-15

        // ─────────────── Listagens RF16 – RF17 ───────────────
        public async Task<IEnumerable<TarefaDto>> ListarEmCursoAsync(int utilizadorId)
        {
            // ToList() para permitir indexação sem erro CS0021
            var tarefas = (await _repo.GetEmCursoAsync(utilizadorId)).ToList();
            var dtos    = _mapper.Map<List<TarefaDto>>(tarefas);

            for (int i = 0; i < dtos.Count; i++)
                dtos[i].HorasDecorridas = tarefas[i].DataHoraInicio is null
                    ? null
                    : Horas(tarefas[i].DataHoraInicio!.Value);

            return dtos;
        }

        public async Task<IEnumerable<TarefaDto>> ListarConcluidasAsync(
            int utilizadorId, DateTime inicio, DateTime fim)
        {
            var tarefas = await _repo.GetConcluidasEntreDatasAsync(utilizadorId, inicio, fim);
            return _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
        }

        // ─────────────── Relatórios RF23 – RF28 ───────────────
        public async Task<RelatorioMensalDto> RelatorioMensalAsync(int utilizadorId, int ano, int mes)
        {
            var i = new DateTime(ano, mes, 1);
            var f = i.AddMonths(1).AddSeconds(-1);

            var concluidas = await _repo.GetConcluidasEntreDatasAsync(utilizadorId, i, f);
            return new RelatorioMensalBuilder().Add(concluidas).Build();
        }

        public async Task<IEnumerable<RelatorioProjetoClienteDto>> RelatorioPorProjetoClienteAsync(
            int utilizadorId, int ano, int mes)
        {
            var i   = new DateTime(ano, mes, 1);
            var f   = i.AddMonths(1).AddSeconds(-1);
            var lst = await _repo.GetConcluidasEntreDatasAsync(utilizadorId, i, f);

            return lst
                .SelectMany(t => t.IdProjetos.Select(p => new { T = t, P = p }))
                .GroupBy(tp => tp.P.IdProjeto)
                .Select(g =>
                {
                    var p  = g.First().P;
                    var hs = g.Sum(x => Horas(x.T.DataHoraInicio!.Value, x.T.DataFim!.Value));
                    var cs = g.Sum(x =>
                        (x.T.PrecoHora ?? p.PrecoHora ?? 0) *
                        Horas(x.T.DataHoraInicio!.Value, x.T.DataFim!.Value));

                    return new RelatorioProjetoClienteDto
                    {
                        Cliente      = p.NomeCliente ?? "(sem cliente)",
                        Projeto      = p.Nome,
                        TotalHoras   = hs,
                        TotalCusto   = cs,
                        Utilizadores = _mapper.Map<List<DTOs.Utilizadores.UtilizadorDto>>(
                                           g.SelectMany(x => x.T.IdUtilizadors).Distinct())
                    };
                })
                .ToList();
        }

        // ─────────────── Consultas auxiliares ───────────────
        public async Task<IEnumerable<TarefaDto>> GetByProjetoIdAsync(int projetoId) =>
            _mapper.Map<IEnumerable<TarefaDto>>(await _repo.GetByProjetoIdAsync(projetoId));

        public async Task<IEnumerable<TarefaDto>> GetByUtilizadorIdAsync(int utilizadorId) =>
            _mapper.Map<IEnumerable<TarefaDto>>(await _repo.GetByUtilizadorIdAsync(utilizadorId));
    }
}
