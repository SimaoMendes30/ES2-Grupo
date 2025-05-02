namespace Backend.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using Backend.Models;

public sealed class TarefaFimPosteriorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext ctx)
    {
        var tarefa = (Tarefa)ctx.ObjectInstance;

        if (tarefa.DataHoraInicio is null || tarefa.DataFim is null)
            return ValidationResult.Success!;

        if (tarefa.DataFim < DateOnly.FromDateTime(tarefa.DataHoraInicio.Value))
            return new ValidationResult("DataFim deve ser posterior à DataHoraInicio");

        return ValidationResult.Success!;
    }
}