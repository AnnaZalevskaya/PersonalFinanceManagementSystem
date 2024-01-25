using Auth.Application.Interfaces;
using Auth.Application.Models;
using MassTransit;

namespace Auth.Application.Consumers
{
    public class AuthenticateConsumer : AuthBaseConsumer, IConsumer<AuthRequest>
    {
        public AuthenticateConsumer(IUsersService usersService) : base(usersService)
        {
        }

        public async Task Consume(ConsumeContext<AuthRequest> context)
        {
            var order = await _usersService.AuthenticateAsync(context.Message, context.CancellationToken);
            await context.RespondAsync(order);
        }
    }
}
