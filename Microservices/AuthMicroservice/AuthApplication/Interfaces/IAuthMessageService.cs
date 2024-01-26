using Auth.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Application.Interfaces
{
    public interface IAuthMessageService
    {
        public Task<ActionResult<AuthResponse>> AuthenticateAsync([FromBody] AuthRequest model);
        public Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest model);
    }
}
