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
        private readonly IUsersService _userService;
        private readonly IAuthMessageService _messageService;

        public UsersController(IUsersService userService, IAuthMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthResponse>> AuthenticateAsync([FromBody] AuthRequest model)
        {
            var user = await _messageService.AuthenticateAsync(model);

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest model)
        {
            var newUser = await _messageService.RegisterAsync(model);

            return Ok(newUser);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllAsync([FromQuery] PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(paginationSettings, cancellationToken);

            return Ok(users);
        }
    }
}
