namespace Backend.DTOs.Utilizadores;

public class UtilizadorUpdatePasswordDto
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}