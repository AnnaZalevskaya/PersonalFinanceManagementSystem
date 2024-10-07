using Accounts.DataAccess.Repositories.Interfaces;

namespace Accounts.DataAccess.EFCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        IFinancialAccountRepository FinancialAccounts { get; }
        IFinancialAccountTypeRepository FinancialAccountTypes { get; }
        ICurrencyRepository Currencies { get; }
        IFinancialGoalRepository FinancialGoals { get; }
        IFinancialGoalTypeRepository FinancialGoalTypes { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
