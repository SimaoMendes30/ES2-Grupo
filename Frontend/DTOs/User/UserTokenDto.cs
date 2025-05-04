namespace Frontend.DTOs.User
{
    public class UserTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public int? UserId { get; set; }
    }
}