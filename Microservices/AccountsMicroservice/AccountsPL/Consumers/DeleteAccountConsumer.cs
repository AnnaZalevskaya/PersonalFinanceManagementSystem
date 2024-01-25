using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using MassTransit;

namespace Accounts.Presentation.Consumers
{
    public class DeleteAccountConsumer : AccountsBaseConsumer, IConsumer<DeleteAccountRequest>
    {
        public DeleteAccountConsumer(IFinancialAccountService accountsService) : base(accountsService)
        {
            
        }

        public async Task Consume(ConsumeContext<DeleteAccountRequest> context)
        {
            _accountsService.DeleteAsync(context.Message.Id, context.CancellationToken);
        }
    }
}
