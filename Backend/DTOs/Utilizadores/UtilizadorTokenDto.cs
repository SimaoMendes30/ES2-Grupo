namespace Backend.DTOs.Utilizadores;

public class UtilizadorTokenDto
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}