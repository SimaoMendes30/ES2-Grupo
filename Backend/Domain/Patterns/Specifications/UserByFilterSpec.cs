using System.Linq.Expressions;
using Backend.Domain.DTOs.User;
using Backend.Domain.Patterns.Specifications.Interfaces;
using Backend.Models;

namespace Backend.Domain.Patterns.Specifications;

public class UserByFilterSpec : ISpecification<UserEntity>
{
    private readonly UserFilterDto _filter;

    public UserByFilterSpec(UserFilterDto filter)
    {
        _filter = filter;
    }

    public Expression<Func<UserEntity, bool>> ToExpression()
    {
        return u =>
            (!_filter.IdUtilizador.HasValue || u.IdUtilizador == _filter.IdUtilizador) &&
            (_filter.IdsUtilizadores == null || !_filter.IdsUtilizadores.Any() || _filter.IdsUtilizadores.Contains(u.IdUtilizador)) &&

            (string.IsNullOrEmpty(_filter.Nome) || u.Nome.Contains(_filter.Nome)) &&
            (string.IsNullOrEmpty(_filter.Username) || u.Username.Contains(_filter.Username)) &&

            (!_filter.Admin.HasValue || u.Admin == _filter.Admin) &&
            (!_filter.SuperUser.HasValue || u.SuperUser == _filter.SuperUser) &&
            (!_filter.IsDeleted.HasValue || u.IsDeleted == _filter.IsDeleted);
    }
}
