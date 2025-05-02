namespace Frontend.DTOs.Projetos;

public class ProjetoDto
{
    public int      IdProjeto   { get; set; }
    public string   Nome        { get; set; } = string.Empty;
    public string?  NomeCliente { get; set; }
    public string?  Descricao   { get; set; }
    public decimal? PrecoHora   { get; set; }
    public int      IdUtilizador{ get; set; }
    public DateOnly? DataCriacao{ get; set; }
}