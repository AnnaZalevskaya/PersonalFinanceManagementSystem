using System.ComponentModel.DataAnnotations;

namespace Auth.Application.Models
{
    public class RegisterRequest
    {
        public int Id { get; set; } 
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;
    }
}
