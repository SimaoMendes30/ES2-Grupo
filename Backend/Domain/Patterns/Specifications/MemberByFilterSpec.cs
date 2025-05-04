using System.Linq.Expressions;
using Backend.Domain.DTOs.Member;
using Backend.Domain.Patterns.Specifications.Interfaces;
using Backend.Models;

namespace Backend.Domain.Patterns.Specifications;

public class MemberByFilterSpec : ISpecification<MemberEntity>
{
    private readonly MemberFilterDto _filter;

    public MemberByFilterSpec(MemberFilterDto filter)
    {
        _filter = filter;
    }

    public Expression<Func<MemberEntity, bool>> ToExpression()
    {
        return m =>
            (!_filter.IdMembro.HasValue || m.IdMembro == _filter.IdMembro) &&
            (_filter.IdsMembros == null || !_filter.IdsMembros.Any() || _filter.IdsMembros.Contains(m.IdMembro)) &&

            (!_filter.IdUtilizador.HasValue || m.IdUtilizador == _filter.IdUtilizador) &&
            (_filter.IdsUtilizadores == null || !_filter.IdsUtilizadores.Any() || _filter.IdsUtilizadores.Contains(m.IdUtilizador)) &&

            (!_filter.IdProjeto.HasValue || m.IdProjeto == _filter.IdProjeto) &&
            (_filter.IdsProjetos == null || !_filter.IdsProjetos.Any() || _filter.IdsProjetos.Contains(m.IdProjeto)) &&

            (!_filter.DataConviteDe.HasValue || m.DataConvite >= _filter.DataConviteDe) &&
            (!_filter.DataConviteAte.HasValue || m.DataConvite <= _filter.DataConviteAte) &&

            (!_filter.DataEstadoDe.HasValue || m.DataEstado >= _filter.DataEstadoDe) &&
            (!_filter.DataEstadoAte.HasValue || m.DataEstado <= _filter.DataEstadoAte) &&

            (string.IsNullOrEmpty(_filter.EstadoConvite) || m.EstadoConvite == _filter.EstadoConvite) &&
            (string.IsNullOrEmpty(_filter.EstadoAtividade) || m.EstadoAtividade == _filter.EstadoAtividade);
    }
}
