using Auth.Application.Interfaces;
using Auth.Application.Models;
using MassTransit;

namespace Auth.Application.Consumers
{
    public class RegisterUserConsumer : AuthBaseConsumer, IConsumer<RegisterRequest>
    {
        public RegisterUserConsumer(IUsersService usersService) : base(usersService)
        {

        }

        public async Task Consume(ConsumeContext<RegisterRequest> context)
        {
            var user = await _usersService.RegisterAsync(context.Message, context.CancellationToken);
            await context.RespondAsync(user);
        }
    }
}
