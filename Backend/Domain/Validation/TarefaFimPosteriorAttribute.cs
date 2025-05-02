using System;
using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Domain.Validation;

public sealed class TarefaFimPosteriorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext ctx)
    {
        var tarefa = (Tarefa)ctx.ObjectInstance;
        if (tarefa.DataFim is null) return ValidationResult.Success!;
        return tarefa.DataFim >= tarefa.DataInicio ? ValidationResult.Success! : new("DataFim deve ser posterior a DataInicio");
    }
}