public record UpdateProjetoDto(
    string Nome,
    string? NomeCliente,
    string? Descricao,
    decimal? PrecoHora);