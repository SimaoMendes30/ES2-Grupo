using System.Linq.Expressions;
using Backend.Domain.DTOs.Task;
using Backend.Domain.Patterns.Specifications.Interfaces;
using Backend.Models;

namespace Backend.Domain.Patterns.Specifications;

public class TaskByOrderSpec : ISpecification<TaskEntity>
{
    private readonly TaskOrderDto _order;

    public TaskByOrderSpec(TaskOrderDto order)
    {
        _order = order;
    }

    /// <summary>
    /// Gera a expressão de ordenação com base no DTO de ordenação.
    /// Caso não seja especificado, ordena por "Titulo".
    /// </summary>
    public Func<IQueryable<TaskEntity>, IOrderedQueryable<TaskEntity>> GetOrderExpression()
    {
        return query =>
        {
            // Ordenação explícita pelo Título, se estiver definida no DTO
            if (!string.IsNullOrEmpty(_order.Titulo))
            {
                return _order.IsDescending 
                    ? query.OrderByDescending(t => t.Titulo)
                    : query.OrderBy(t => t.Titulo);
            }

            // Ordenação explícita pela Data de Início, se estiver definida no DTO
            if (_order.DataInicio.HasValue)
            {
                return _order.IsDescending
                    ? query.OrderByDescending(t => t.DataInicio)
                    : query.OrderBy(t => t.DataInicio);
            }

            // Ordenação por defeito, caso não haja critérios definidos, vai por "Titulo"
            return query.OrderBy(t => t.Titulo); 
        };
    }

    /// <summary>
    /// Implementação da interface, mas a ordenação não necessita de expressão de filtro.
    /// </summary>
    public Expression<Func<TaskEntity, bool>> ToExpression()
    {
        return t => true; // Não aplica filtro, apenas ordenação
    }
}