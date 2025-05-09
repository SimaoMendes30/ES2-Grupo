namespace Backend.Domain.DTOs.User
{
    public class UserTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset Expiration { get; set; }
        
        public int? UserId { get; set; }
    }
}