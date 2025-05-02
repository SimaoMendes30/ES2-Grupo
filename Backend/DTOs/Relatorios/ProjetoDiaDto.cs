namespace Backend.DTOs.Relatorios;

public sealed class ProjetoDiaDto
{
    public string NomeProjeto { get; init; } = string.Empty;
    public decimal Horas { get; init; }
    public decimal Custo { get; init; }
}
