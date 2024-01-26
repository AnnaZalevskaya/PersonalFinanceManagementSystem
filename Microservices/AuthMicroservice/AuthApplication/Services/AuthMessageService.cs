using Auth.Application.Interfaces;
using Auth.Application.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Application.Services
{
    public class AuthMessageService : IAuthMessageService
    {
        private readonly IBusControl _busControl;
        private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/usersQueue");

        public AuthMessageService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task<ActionResult<AuthResponse>> AuthenticateAsync([FromBody] AuthRequest model)
        {
            var response = await GetResponseRabbitTask<AuthRequest, AuthResponse>(model);

            return response;
        }

        public async Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest model)
        {
            var response = await GetResponseRabbitTask<RegisterRequest, RegisterResponse>(model);

            return response;
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
