using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Domain.Builders;
using Backend.Domain.Strategies;
using Backend.DTOs.Relatorios;
using Backend.DTOs.Tarefas;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Backend.Services;

public sealed class TarefaService : ITarefaService
{
    private readonly ITarefaRepository    _repo;
    private readonly IProjetoRepository   _projRepo;
    private readonly IMapper              _mapper;
    private readonly IPrecoTarefaStrategy _price;
    private readonly ILogger<TarefaService> _log;

    public TarefaService(ITarefaRepository repo,
                         IProjetoRepository projRepo,
                         IMapper mapper,
                         IPrecoTarefaStrategy price,
                         ILogger<TarefaService> log)
    {
        _repo     = repo;
        _projRepo = projRepo;
        _mapper   = mapper;
        _price    = price;
        _log      = log;
    }

    /* ------------ helpers ------------ */
    private static decimal Horas(DateTime ini, DateTime fim) => (decimal)(fim - ini).TotalHours;
    private static decimal Horas(DateTime ini)               => (decimal)(DateTime.UtcNow - ini).TotalHours;

    /* ------------ criar / terminar / mover ------------ */
    public async Task<TarefaDto> StartAsync(StartTarefaDto dto)
    {
        var projeto = await _projRepo.GetByIdAsync(dto.ProjetoId);

        var tarefa = new Tarefa
        {
            Titulo      = dto.Titulo,
            Descricao   = dto.Descricao,
            Responsavel = dto.Responsavel,
            DataInicio  = dto.DataInicio ?? DateTime.UtcNow,
            Estado      = "Em Curso",
            PrecoHora   = dto.PrecoHora
        };
        tarefa.IdProjetos.Add(projeto);

        await _repo.AddAsync(tarefa);
        return _mapper.Map<TarefaDto>(tarefa);
    }

    public async Task<TarefaDto> EndAsync(int id, EndTarefaDto dto)
    {
        var tarefa = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Tarefa não encontrada");
        if (tarefa.DataFim is not null) throw new InvalidOperationException("Tarefa já concluída");

        tarefa.DataFim = dto.DataFim ?? DateTime.UtcNow;
        tarefa.Estado  = "Concluída";

        await _repo.UpdateAsync(tarefa);
        return _mapper.Map<TarefaDto>(tarefa);
    }

    public async Task MoveAsync(int tarefaId, int projetoDestinoId)
    {
        var tarefa  = await _repo.GetByIdAsync(tarefaId);
        var destino = await _projRepo.GetByIdAsync(projetoDestinoId);

        tarefa.IdProjetos.Clear();
        tarefa.IdProjetos.Add(destino);

        await _repo.UpdateAsync(tarefa);
    }

    /* ------------ soft‑delete ------------ */
    public async Task DeleteAsync(int id)
    {
        var tarefa = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Tarefa não encontrada");
        tarefa.IsDeleted = true;
        await _repo.UpdateAsync(tarefa);          // grava flag em vez de remover
    }

    /* ------------ listagens ------------ */
    public async Task<IEnumerable<TarefaDto>> ListarEmCursoAsync(int utilizadorId)
    {
        var lst  = (await _repo.GetEmCursoAsync(utilizadorId))
                   .Where(t => !t.IsDeleted)     // só ativas
                   .ToList();

        var dtos = _mapper.Map<List<TarefaDto>>(lst);
        for (int i = 0; i < dtos.Count; i++)
            dtos[i].HorasDecorridas = Horas(lst[i].DataInicio);
        return dtos;
    }

    public async Task<IEnumerable<TarefaDto>> ListarConcluidasAsync(int utilizadorId, DateTime ini, DateTime fim)
    {
        var lst = await _repo.GetConcluidasEntreDatasAsync(utilizadorId, ini, fim);
        return _mapper.Map<IEnumerable<TarefaDto>>(lst.Where(t => !t.IsDeleted));
    }

    /* ------------ relatórios ------------ */
    public async Task<RelatorioMensalDto> RelatorioMensalAsync(int utilizadorId, int ano, int mes)
    {
        var ini = new DateTime(ano, mes, 1);
        var fim = ini.AddMonths(1).AddSeconds(-1);

        var concluidas = (await _repo.GetConcluidasEntreDatasAsync(utilizadorId, ini, fim))
                         .Where(t => !t.IsDeleted);

        return new RelatorioMensalBuilder().Add(concluidas).Build();
    }

    public async Task<IEnumerable<RelatorioProjetoClienteDto>> RelatorioPorProjetoClienteAsync(int utilizadorId, int ano, int mes)
    {
        var ini = new DateTime(ano, mes, 1);
        var fim = ini.AddMonths(1).AddSeconds(-1);
        var lst = (await _repo.GetConcluidasEntreDatasAsync(utilizadorId, ini, fim))
                  .Where(t => !t.IsDeleted);

        return lst
            .SelectMany(t => t.IdProjetos.Select(p => new { T = t, P = p }))
            .GroupBy(x => x.P.IdProjeto)
            .Select(g =>
            {
                var p   = g.First().P;
                var hrs = g.Sum(x => Horas(x.T.DataInicio, x.T.DataFim!.Value));
                var cst = g.Sum(x => _price.CalcularPreco(x.T, p) * Horas(x.T.DataInicio, x.T.DataFim!.Value));

                return new RelatorioProjetoClienteDto
                {
                    Cliente      = p.NomeCliente ?? "(sem cliente)",
                    Projeto      = p.Nome,
                    TotalHoras   = hrs,
                    TotalCusto   = cst,
                    Utilizadores = _mapper.Map<List<DTOs.Utilizadores.UtilizadorDto>>(
                                       g.SelectMany(x => x.T.IdUtilizadors).Distinct())
                };
            })
            .ToList();
    }

    /* ------------ utilitários ------------ */
    public async Task<IEnumerable<TarefaDto>> GetByProjetoIdAsync(int projetoId) =>
        _mapper.Map<IEnumerable<TarefaDto>>(
            (await _repo.GetByProjetoIdAsync(projetoId)).Where(t => !t.IsDeleted));

    public async Task<IEnumerable<TarefaDto>> GetByUtilizadorIdAsync(int utilizadorId) =>
        _mapper.Map<IEnumerable<TarefaDto>>(
            (await _repo.GetByUtilizadorIdAsync(utilizadorId)).Where(t => !t.IsDeleted));
}
