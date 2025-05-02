namespace Frontend.DTOs.Utilizadores;

public class UtilizadorDto
{
    public int    IdUtilizador { get; set; }
    public string Nome         { get; set; } = string.Empty;
    public string Username     { get; set; } = string.Empty;
    public bool   Admin        { get; set; }
    public bool   SuperUser    { get; set; }
}