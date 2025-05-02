namespace Backend.DTOs.Utilizadores;

public class UtilizadorUpdateDto
{
    public string Nome { get; set; } = null!;
    public string Username { get; set; } = null!;
    public bool Admin { get; set; }
    public bool SuperUser { get; set; }
}