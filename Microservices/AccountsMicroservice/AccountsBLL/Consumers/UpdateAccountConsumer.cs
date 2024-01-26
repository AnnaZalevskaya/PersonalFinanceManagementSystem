using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.Services.Interfaces;
using MassTransit;

namespace Accounts.Presentation.Consumers
{
    public class UpdateAccountConsumer : AccountsBaseConsumer, IConsumer<UpdateAccountRequest>
    {
        public UpdateAccountConsumer(IFinancialAccountService accountsService) : base(accountsService)
        {
            
        }

        public async Task Consume(ConsumeContext<UpdateAccountRequest> context)
        {
            await context.RespondAsync(_accountsService
                .UpdateAsync(context.Message.Id, context.Message.Model, context.CancellationToken));
        }
    }
}
