namespace Frontend.DTOs.User
{
    public class UserDetailsDto
    {
        public int IdUtilizador { get; set; }

        public string Nome { get; set; } = null!;

        public int? NumHoras { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Admin { get; set; }

        public bool SuperUser { get; set; }
    }
}