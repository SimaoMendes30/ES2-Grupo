using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Utilizadores;

public class RegisterDto  // = UtilizadorCreateDto no backend
{
    [Required] public string Nome       { get; set; } = string.Empty;
    [Required] public string Username   { get; set; } = string.Empty;

    // Flags (opcionais) – mantêm o mesmo nome do backend
    public bool Admin      { get; set; }
    public bool SuperUser  { get; set; }

    [Required] public string Password   { get; set; } = string.Empty;
}