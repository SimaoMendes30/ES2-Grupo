namespace Backend.DTOs.Relatorios;

public sealed class RelatorioMensalDto
{
    public List<DiaRelatorioDto> Dias { get; init; } = new();
    public decimal TotalHorasMes { get; init; }
    public decimal TotalCustoMes { get; init; }
}