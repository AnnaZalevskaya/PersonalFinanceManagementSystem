using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.MassTransit.Responses;
using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
using Auth.Application.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class AccountMessageService : IAccountMessageService
    {
        private readonly IBusControl _busControl;
        private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/accountsQueue");

        public AccountMessageService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task<ActionResult<GetAccountsResponse>> CreateNewAccountAsync(FinancialAccountModel model,
            UserModel user)
        {
            var request = new CreateAccountsRequest()
            {
                AccountId = model.Id,
                User = user
            };

            var response = await GetResponseRabbitTask<CreateAccountsRequest,
                GetAccountsResponse>(request);
            response.UserName = user.Username;

            return response;
        }

        public async Task<IActionResult> CloseAccountAsync(int id, UserModel user)
        {
            var request = new DeleteAccountRequest()
            {
                Id = id,
                User = user
            };

            var response = await GetResponseRabbitTask<DeleteAccountRequest, NoContentResult>(request);

            return response;
        }

        public async Task<IActionResult> EditAccountAsync(int id, FinancialAccountModel model, UserModel user)
        {
            var request = new UpdateAccountRequest()
            {
                Id = id,
                Model = model,
                User = user
            };
            var response = await GetResponseRabbitTask<UpdateAccountRequest, NoContentResult>(request);

            return response;
        }

        public async Task<ActionResult<GetAccountResponse>> GetAccountAsync(int id, UserModel user)
        {
            var request = new GetAccountRequest
            {
                Id = id,
                User = user
            };

            var response = await GetResponseRabbitTask<GetAccountRequest, GetAccountResponse>(request);

            return response;
        }

        public async Task<ActionResult<GetAccountsResponse>> GetUserAccountsAsync(int userId,
            PaginationSettings paginationSettings)
        {
            var request = new GetAccountsByUserRequest
            {
                UserId = userId,
                PaginationSettings = paginationSettings
            };

            var response = await GetResponseRabbitTask<GetAccountsByUserRequest, GetAccountsResponse>(request);

            return response;
        }

        public async Task<ActionResult<GetAccountsResponse>> GetAllAccountsAsync(PaginationSettings paginationSettings)
        {
            var request = new GetAllAccountsRequest
            {
                PaginationSettings = paginationSettings
            };

            var response = await GetResponseRabbitTask<GetAllAccountsRequest, GetAccountsResponse>(request);

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
