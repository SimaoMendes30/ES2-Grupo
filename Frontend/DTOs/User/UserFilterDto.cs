namespace Frontend.DTOs.User
{
    public class UserFilterDto
    {
        public int? IdUtilizador { get; set; }

        public string? Nome { get; set; }

        public string? Username { get; set; }

        public bool? Admin { get; set; }

        public bool? SuperUser { get; set; }
        
        public bool? IsDeleted { get; set; }
    }
}