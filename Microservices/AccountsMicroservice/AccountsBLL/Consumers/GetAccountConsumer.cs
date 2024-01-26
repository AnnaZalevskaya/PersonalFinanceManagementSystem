using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.MassTransit.Responses;
using Accounts.BusinessLogic.Services.Interfaces;
using MassTransit;

namespace Accounts.Presentation.Consumers
{
    public class GetAccountConsumer : AccountsBaseConsumer, IConsumer<GetAccountRequest>
    {
        public GetAccountConsumer(IFinancialAccountService accountsService) : base(accountsService)
        {
            
        }

        public async Task Consume(ConsumeContext<GetAccountRequest> context)
        {
            var account = await _accountsService.GetByIdAsync(context.Message.Id, context.CancellationToken);

            var response = new GetAccountResponse
            {
                Account = account
            };

            await context.RespondAsync(response);
        }
    }
}
