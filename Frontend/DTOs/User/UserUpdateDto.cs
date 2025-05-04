using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.User
{
    public class UserUpdateDto
    {
        [StringLength(256)]
        public string? Nome { get; set; }

        public int? NumHoras { get; set; }

        [StringLength(256)]
        public string? Username { get; set; }

        [StringLength(250)]
        public string? Password { get; set; }

        public bool? Admin { get; set; }

        public bool? SuperUser { get; set; }
    }
}