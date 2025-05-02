namespace Frontend.DTOs.Utilizadores;

public class UtilizadorTokenDto
{
    public string   Token      { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}