using System.Collections.Generic;
using Backend.DTOs.Utilizadores;

namespace Backend.DTOs.Relatorios
{
    public sealed class RelatorioProjetoClienteDto
    {
        public string  Cliente      { get; init; } = string.Empty;
        public string  Projeto      { get; init; } = string.Empty;
        public decimal TotalHoras   { get; init; }
        public decimal TotalCusto   { get; init; }
        public List<UtilizadorDto> Utilizadores { get; init; } = new();
    }
}