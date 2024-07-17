namespace Auth.Application.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
