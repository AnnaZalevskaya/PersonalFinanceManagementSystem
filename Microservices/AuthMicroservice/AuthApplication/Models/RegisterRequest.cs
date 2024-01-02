namespace Auth.Application.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;
    }
}
