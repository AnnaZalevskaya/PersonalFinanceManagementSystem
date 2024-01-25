using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Application.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IBusControl _busControl;
        private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/usersQueue");

        public UsersController(IUsersService userService, IBusControl busControl)
        {
            _userService = userService;
            _busControl = busControl;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthResponse>> AuthenticateAsync([FromBody] AuthRequest model)
        {
            var response = await GetResponseRabbitTask<AuthRequest, AuthResponse>(model);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest model)
        {
            var response = await GetResponseRabbitTask<RegisterRequest, IdentityResult>(model);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllAsync([FromQuery] PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(paginationSettings, cancellationToken);

            return Ok(users);
        }

        private async Task<TOut> GetResponseRabbitTask<TIn, TOut>(TIn request)
        where TIn : class
        where TOut : class
        {
            var client = _busControl.CreateRequestClient<TIn>(_rabbitMqUrl);
            var response = await client.GetResponse<TOut>(request);
            return response.Message;
        }
    }
}
