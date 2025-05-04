using System.Linq.Expressions;
using Backend.Domain.DTOs.Task;
using Backend.Domain.Patterns.Specifications.Interfaces;
using Backend.Models;

namespace Backend.Domain.Patterns.Specifications;

public class TaskByFilterSpec : ISpecification<TaskEntity>
{
    private readonly TaskFilterDto _filter;

    public TaskByFilterSpec(TaskFilterDto filter)
    {
        _filter = filter;
    }

    public Expression<Func<TaskEntity, bool>> ToExpression()
    {
        return t =>
            (!_filter.IdTarefa.HasValue || t.IdTarefa == _filter.IdTarefa) &&
            (_filter.IdsTarefas == null || !_filter.IdsTarefas.Any() || _filter.IdsTarefas.Contains(t.IdTarefa)) &&

            (string.IsNullOrEmpty(_filter.Titulo) || t.Titulo.Contains(_filter.Titulo)) &&

            (
                !_filter.Responsavel.HasValue || (
                    _filter.Responsavel >= 0
                        ? t.Responsavel == _filter.Responsavel
                        : t.Responsavel != -_filter.Responsavel 
                )
            ) &&

            (_filter.IdsResponsaveis == null || !_filter.IdsResponsaveis.Any() || _filter.IdsResponsaveis.Contains(t.Responsavel)) &&

            (!_filter.DataInicioDe.HasValue || t.DataInicio >= _filter.DataInicioDe) &&
            (!_filter.DataInicioAte.HasValue || t.DataInicio <= _filter.DataInicioAte) &&

            (!_filter.DataFimDe.HasValue || t.DataFim >= _filter.DataFimDe) &&
            (!_filter.DataFimAte.HasValue || t.DataFim <= _filter.DataFimAte) &&

            (string.IsNullOrEmpty(_filter.Estado) || t.Estado == _filter.Estado) &&

            (!_filter.PrecoHoraMin.HasValue || t.PrecoHora >= _filter.PrecoHoraMin) &&
            (!_filter.PrecoHoraMax.HasValue || t.PrecoHora <= _filter.PrecoHoraMax) &&

            (!_filter.IsDeleted.HasValue || t.IsDeleted == _filter.IsDeleted);
    }
}