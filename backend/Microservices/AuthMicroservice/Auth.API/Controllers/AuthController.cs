using Auth.Application.Interfaces;
using Auth.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController  : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(IUsersService userService)
        {
            _usersService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseModel>> RegisterAsync([FromBody] RegisterRequestModel model,
            CancellationToken cancellationToken)
        {
            var response = await _usersService.RegisterAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthResponseModel>> AuthenticateAsync([FromBody] AuthRequestModel model,
            CancellationToken cancellationToken)
        {
            var response = await _usersService.AuthenticateAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenModel>> RefreshAccessTokenAsync([FromBody] TokenModel model)
        {
            var tokens = await _usersService.RefreshAccessToken(model);

            return Ok(tokens);
        }
    }
}
