using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Application.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService userService)
        {
            _usersService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthResponse>> AuthenticateAsync([FromBody] AuthRequest model,
            CancellationToken cancellationToken)
        {
            var response = await _usersService.AuthenticateAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest model,
            CancellationToken cancellationToken)
        {
            var response = await _usersService.RegisterAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("all-users")]
        [Authorize]
        public async Task<ActionResult<List<UserModel>>> GetAllAsync([FromQuery] PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            var users = await _usersService.GetAllAsync(paginationSettings, cancellationToken);

            return Ok(users);
        }
    }
}