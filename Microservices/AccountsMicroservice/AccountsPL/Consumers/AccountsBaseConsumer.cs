using Accounts.BusinessLogic.Services.Interfaces;

namespace Accounts.BusinessLogic.Consumers
{
    public class AccountsBaseConsumer
    {
        protected readonly IFinancialAccountService _accountsService;

        public AccountsBaseConsumer(IFinancialAccountService accountsService)
        {
            _accountsService = accountsService;
        }
    }
}
