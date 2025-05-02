namespace Frontend.DTOs.Utilizadores;

public class UtilizadorUpdatePasswordDto
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}