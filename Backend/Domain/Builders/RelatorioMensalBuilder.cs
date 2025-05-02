using System;
using System.Collections.Generic;
using System.Linq;
using Backend.DTOs.Relatorios;
using Backend.Models;

namespace Backend.Domain.Builders;

public sealed class RelatorioMensalBuilder
{
    private readonly List<Tarefa> _tarefas = new();

    public RelatorioMensalBuilder Add(IEnumerable<Tarefa> tarefas)
    {
        _tarefas.AddRange(tarefas);
        return this;
    }

    private static decimal Horas(DateTime ini, DateTime fim) => (decimal)(fim - ini).TotalHours;

    public RelatorioMensalDto Build()
    {
        var dias = _tarefas.Where(t => t.DataFim is not null)
            .GroupBy(t => t.DataFim!.Value.Date)
            .Select(g =>
            {
                var horasDia = g.Sum(t => Horas(t.DataInicio, t.DataFim!.Value));
                var custoDia = g.Sum(t =>
                    (t.PrecoHora ?? t.IdProjetos.FirstOrDefault()?.PrecoHora ?? 0) * Horas(t.DataInicio, t.DataFim!.Value));

                var limite = g.First().IdUtilizadors.FirstOrDefault()?.NumHoras;
                return new DiaRelatorioDto
                {
                    Dia           = g.Key,
                    TotalHoras    = horasDia,
                    TotalCusto    = custoDia,
                    ExcedeuLimite = limite.HasValue && horasDia > limite.Value,
                    Projetos      = g.GroupBy(t => t.IdProjetos.FirstOrDefault()?.Nome ?? "Sem Projeto")
                                      .Select(p => new ProjetoDiaDto
                                      {
                                          NomeProjeto = p.Key,
                                          Horas       = p.Sum(x => Horas(x.DataInicio, x.DataFim!.Value)),
                                          Custo       = p.Sum(x =>
                                              (x.PrecoHora ?? x.IdProjetos.FirstOrDefault()?.PrecoHora ?? 0) * Horas(x.DataInicio, x.DataFim!.Value))
                                      }).ToList()
                };
            }).ToList();

        return new RelatorioMensalDto
        {
            Dias          = dias,
            TotalHorasMes = dias.Sum(d => d.TotalHoras),
            TotalCustoMes = dias.Sum(d => d.TotalCusto)
        };
    }
}