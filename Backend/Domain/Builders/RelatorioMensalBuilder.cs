using System;
using System.Collections.Generic;
using System.Linq;
using Backend.DTOs.Relatorios;
using Backend.Models;

namespace Backend.Domain.Builders
{
    public sealed class RelatorioMensalBuilder
    {
        private readonly List<Tarefa> _tarefas = new();

        public RelatorioMensalBuilder Add(IEnumerable<Tarefa> tarefas)
        {
            _tarefas.AddRange(tarefas);
            return this;
        }

        private static decimal Horas(DateTime ini, DateOnly fim) =>
            (decimal)(fim.ToDateTime(TimeOnly.MinValue) - ini).TotalHours;

        public RelatorioMensalDto Build()
        {
            var dias = _tarefas.Where(t => t.DataFim != null && t.DataHoraInicio != null)
                .GroupBy(t => t.DataFim!.Value)
                .Select(g =>
                {
                    var horasDia = g.Sum(t => Horas(t.DataHoraInicio!.Value, t.DataFim!.Value));
                    var custoDia = g.Sum(t =>
                        (t.PrecoHora ?? t.IdProjetos.FirstOrDefault()?.PrecoHora ?? 0) *
                        Horas(t.DataHoraInicio!.Value, t.DataFim!.Value));

                    var limite = g.First().IdUtilizadors.FirstOrDefault()?.NumHoras; // assumimos todos igual
                    return new DiaRelatorioDto
                    {
                        Dia           = g.Key.ToDateTime(TimeOnly.MinValue).Date,
                        TotalHoras    = horasDia,
                        TotalCusto    = custoDia,
                        ExcedeuLimite = limite.HasValue && horasDia > limite.Value,
                        Projetos      = g.GroupBy(t => t.IdProjetos.FirstOrDefault()?.Nome ?? "Sem Projeto")
                                         .Select(p => new ProjetoDiaDto
                                         {
                                             NomeProjeto = p.Key,
                                             Horas       = p.Sum(t => Horas(t.DataHoraInicio!.Value, t.DataFim!.Value)),
                                             Custo       = p.Sum(t =>
                                                 (t.PrecoHora ?? t.IdProjetos.FirstOrDefault()?.PrecoHora ?? 0) *
                                                 Horas(t.DataHoraInicio!.Value, t.DataFim!.Value))
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
}
