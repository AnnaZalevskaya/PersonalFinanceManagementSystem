using Microsoft.AspNet.Identity.EntityFramework;

namespace Auth.Core.Entities
{
    public class AppUser : IdentityUser<long>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
