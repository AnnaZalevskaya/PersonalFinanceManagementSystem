using Auth.Application.Interfaces;

namespace Auth.Application.Consumers
{
    public class AuthBaseConsumer
    {
        protected readonly IUsersService _usersService;

        public AuthBaseConsumer(IUsersService usersService)
        {
            _usersService = usersService;
        }
    }
}
