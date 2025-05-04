using System;
using System.Linq.Expressions;

namespace Backend.Domain.Patterns.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}