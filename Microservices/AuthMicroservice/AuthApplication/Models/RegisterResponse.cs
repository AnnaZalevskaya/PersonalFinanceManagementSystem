namespace Auth.Application.Models
{
    public class RegisterResponse
    {
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
