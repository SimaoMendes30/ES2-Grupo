using System.Linq.Expressions;
using Backend.Domain.DTOs.Project;
using Backend.Domain.Patterns.Specifications.Interfaces;
using Backend.Models;

namespace Backend.Domain.Patterns.Specifications;

public class ProjectByFilterSpec : ISpecification<ProjectEntity>
{
    private readonly ProjectFilterDto _filter;

    public ProjectByFilterSpec(ProjectFilterDto filter)
    {
        _filter = filter;
    }

    public Expression<Func<ProjectEntity, bool>> ToExpression()
    {
        return p =>
            (!_filter.IdProjeto.HasValue || p.IdProjeto == _filter.IdProjeto) &&
            (_filter.IdsProjetos == null || !_filter.IdsProjetos.Any() || _filter.IdsProjetos.Contains(p.IdProjeto)) &&
            
            (string.IsNullOrEmpty(_filter.Nome) || p.Nome.Contains(_filter.Nome)) &&
            (string.IsNullOrEmpty(_filter.NomeCliente) || (p.NomeCliente ?? "").Contains(_filter.NomeCliente)) &&
            
            (!_filter.PrecoHoraMin.HasValue || p.PrecoHora >= _filter.PrecoHoraMin) &&
            (!_filter.PrecoHoraMax.HasValue || p.PrecoHora <= _filter.PrecoHoraMax) &&

            (!_filter.Responsavel.HasValue || p.Responsavel == _filter.Responsavel) &&
            (_filter.IdsResponsaveis == null || !_filter.IdsResponsaveis.Any() || _filter.IdsResponsaveis.Contains(p.Responsavel)) &&

            (!_filter.DataCriacaoDe.HasValue || p.DataCriacao >= _filter.DataCriacaoDe) &&
            (!_filter.DataCriacaoAte.HasValue || p.DataCriacao <= _filter.DataCriacaoAte) &&
            
            (!_filter.IsDeleted.HasValue || p.IsDeleted == _filter.IsDeleted);
    }
}
