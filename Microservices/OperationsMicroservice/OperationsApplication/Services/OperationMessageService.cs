using Accounts.BusinessLogic.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Interfaces;
using Operations.Application.MassTransit.Requests;
using Operations.Application.MassTransit.Responses;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Services
{
    public class OperationMessageService : IOperationMessageService
    {
        private readonly IBusControl _busControl;
        private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/operationsQueue");

        public OperationMessageService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task<ActionResult<GetOperationsResponse>> GetAllAsync(PaginationSettings paginationSettings)
        {
            var request = new GetAllOperationsRequest
            {
                PaginationSettings = paginationSettings
            };

            var response = await GetResponseRabbitTask<GetAllOperationsRequest, GetOperationsResponse>(request);

            return response;
        }

        public async Task<ActionResult<OperationModel>> GetByIdAsync(string id, FinancialAccountModel account)
        {
            var request = new GetOperationRequest
            {
                Id = id,
                Account = account
            };

            var response = await GetResponseRabbitTask<GetOperationRequest, OperationModel>(request);

            return response;
        }

        public async Task<ActionResult<GetOperationsResponse>> GetByAccountIdAsync(int accountId,
            PaginationSettings paginationSettings, FinancialAccountModel account)
        {
            var request = new GetOperationsByAccountRequest
            {
                Id = accountId,
                Account = account,
                PaginationSettings = paginationSettings,
            };

            var response = await GetResponseRabbitTask<GetOperationsByAccountRequest, 
                GetOperationsResponse>(request);

            return response;
        }

        public async Task<ActionResult<OperationModel>> CreateAsync(CreateOperationModel model, 
            FinancialAccountModel account)
        {
            var request = model;
            request.AccountId = account.Id;
            var response = await GetResponseRabbitTask<CreateOperationModel, OperationModel>(request);

            return response;
        }

        public async Task<IActionResult> DeleteFromHistoryAsync(string id, FinancialAccountModel account)
        {
            var request = new DeleteOperationRequest()
            {
                Account = account,
                Id = id
            };

            var response = await GetResponseRabbitTask<DeleteOperationRequest, NoContentResult>(request);

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
