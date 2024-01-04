using Accounts.DataAccess.Repositories.Interfaces;

namespace Accounts.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IFinancialAccountRepository FinancialAccounts { get; }
        IFinancialAccountTypeRepository FinancialAccountTypes { get; }
        ICurrencyRepository Currencies { get; }

        Task SaveChangesAsync();
    }
}
