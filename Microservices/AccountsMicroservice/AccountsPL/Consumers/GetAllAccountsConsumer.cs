using Accounts.BusinessLogic.MassTransit.Requests;
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
            var cancellationToken = context.CancellationToken;
            var paginationSettings = context.Message.PaginationSettings;

            var accounts = await _accountsService.GetAllAsync(paginationSettings, cancellationToken);
            await context.RespondAsync(accounts);
        }
    }
}
