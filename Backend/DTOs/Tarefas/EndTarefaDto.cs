namespace Backend.DTOs.Tarefas;
using System.ComponentModel.DataAnnotations;

public sealed record EndTarefaDto(
    [DataType(DataType.DateTime)] DateTime? DataFim);