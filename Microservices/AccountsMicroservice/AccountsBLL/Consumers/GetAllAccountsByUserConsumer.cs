using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.MassTransit.Responses;
using Accounts.BusinessLogic.Services.Interfaces;
using MassTransit;

namespace Accounts.Presentation.Consumers
{
    public class GetAllAccountsByUserConsumer : AccountsBaseConsumer, IConsumer<GetAccountsByUserRequest>
    {
        public GetAllAccountsByUserConsumer(IFinancialAccountService accountsService) : base(accountsService)
        {

        }

        public async Task Consume(ConsumeContext<GetAccountsByUserRequest> context)
        {
            var accounts = await _accountsService.GetAccountsByUserIdAsync(context.Message.UserId, 
                context.Message.PaginationSettings, context.CancellationToken);

            var response = new GetAccountsResponse
            {
                Accounts = accounts
            };

            await context.RespondAsync(response);
        }
    }
}
