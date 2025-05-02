namespace Frontend.DTOs.Tarefas;
using System.ComponentModel.DataAnnotations;

public sealed record StartTarefaDto(
    [Required, MaxLength(256)] string Titulo,
    string? Descricao,
    [Range(1, int.MaxValue)] int Responsavel,
    [Range(1, int.MaxValue)] int ProjetoId,
    [Range(0, double.MaxValue)] decimal? PrecoHora,
    DateTime? DataInicio = null);
