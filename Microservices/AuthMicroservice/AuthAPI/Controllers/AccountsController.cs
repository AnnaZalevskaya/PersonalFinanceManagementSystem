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
        public IActionResult Authenticate(AuthRequest model)
        {
            var response = _userService.Authenticate(model);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest model)
        {
            var response = await _userService.Register(model);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }
    }
}
