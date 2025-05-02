namespace Backend.DTOs.Utilizadores;

public class UtilizadorCreateDto
{
    public string Nome { get; set; } = null!;
    public string Username { get; set; } = null!;
    public bool Admin { get; set; }
    public bool SuperUser { get; set; }
    public string? Password { get; set; } = null!;
}