using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(256)]
        public string Nome { get; set; } = null!;

        public int? NumHoras { get; set; }

        [Required]
        [StringLength(256)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(256)]
        public string Password { get; set; } = null!;

        public bool Admin { get; set; }

        public bool SuperUser { get; set; }
    }
}