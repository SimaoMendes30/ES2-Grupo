namespace Frontend.DTOs.Utilizadores;

public class UtilizadorCreateDto
{
    public string Nome       { get; set; } = string.Empty;
    public string Username   { get; set; } = string.Empty;
    public string Password   { get; set; } = string.Empty;
    public bool   Admin      { get; set; }
    public bool   SuperUser  { get; set; }
}