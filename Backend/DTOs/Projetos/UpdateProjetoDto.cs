namespace Backend.DTOs.Projeto;

public sealed record UpdateProjetoDto(
    string Nome,
    string? NomeCliente,
    string? Descricao,
    decimal? PrecoHora);
