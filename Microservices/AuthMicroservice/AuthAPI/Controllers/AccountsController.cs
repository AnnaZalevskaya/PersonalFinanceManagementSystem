using Auth.Application.Interfaces;
using Auth.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _userService;

        public AccountsController(IAccountService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthResponse>> AuthenticateAsync([FromBody] AuthRequest model, 
            CancellationToken cancellationToken)
        {
            var response = await _userService.AuthenticateAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> RegisterAsync([FromBody] RegisterRequest model,
            CancellationToken cancellationToken)
        {
            var response = await _userService.RegisterAsync(model, cancellationToken);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(cancellationToken);

            return Ok(users);
        }
    }
}
