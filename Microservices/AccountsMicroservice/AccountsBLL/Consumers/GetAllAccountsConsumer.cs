using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.MassTransit.Responses;
using Accounts.BusinessLogic.Services.Interfaces;
using MassTransit;

namespace Accounts.BusinessLogic.Consumers
{
    public class GetAllAccountsConsumer : AccountsBaseConsumer, IConsumer<GetAllAccountsRequest>
    {
        public GetAllAccountsConsumer(IFinancialAccountService accountsService) : base(accountsService)
        {

        }

        public async Task Consume(ConsumeContext<GetAllAccountsRequest> context)
        {
            var accounts = await _accountsService
                .GetAllAsync(context.Message.PaginationSettings, context.CancellationToken);

            var response = new GetAccountsResponse
            {
                Accounts = accounts
            };

            await context.RespondAsync(response);
        }
    }
}
