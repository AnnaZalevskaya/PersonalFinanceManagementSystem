using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.Services.Interfaces;
using MassTransit;

namespace Accounts.BusinessLogic.Consumers
{
    public class CreateAccountConsumer : AccountsBaseConsumer, IConsumer<CreateAccountsRequest>
    {
        public CreateAccountConsumer(IFinancialAccountService accountsService) : base(accountsService)
        {
            
        }

        public async Task Consume(ConsumeContext<CreateAccountsRequest> context)
        {
            var account = await _accountsService.GetByIdAsync(context.Message.AccountId, context.CancellationToken);
            await context.RespondAsync(account);
        }
    }
}
