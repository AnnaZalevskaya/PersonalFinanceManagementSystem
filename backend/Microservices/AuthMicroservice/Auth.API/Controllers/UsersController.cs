using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Application.Models.Consts;
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
        public async Task<ActionResult<AuthResponseModel>> AuthenticateAsync([FromBody] AuthRequestModel model,
            CancellationToken cancellationToken)
        {
            var response = await _usersService.AuthenticateAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseModel>> RegisterAsync([FromBody] RegisterRequestModel model,
            CancellationToken cancellationToken)
        {
            var response = await _usersService.RegisterAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("all-users")]
        [Authorize(Policy = AuthPolicyConsts.AdminOnly)]
        public async Task<ActionResult<List<UserModel>>> GetAllAsync([FromQuery] PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            var users = await _usersService.GetAllAsync(paginationSettings, cancellationToken);

            return Ok(users);
        }

        [HttpGet("user-info/{id}")]
        [Authorize]
        public async Task<ActionResult<List<UserModel>>> GetUserByIdAsync(long id, CancellationToken cancellationToken)
        {
            var user = await _usersService.GetUserByIdAsync(id, cancellationToken);

            return Ok(user);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<List<UserModel>>> RefreshAccessTokenAsync([FromBody] TokenModel model)
        {
            var tokens = await _usersService.RefreshAccessToken(model);

            return Ok(tokens);
        }
    }
}