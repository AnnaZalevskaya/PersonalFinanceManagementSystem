namespace Auth.Application.Models
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
