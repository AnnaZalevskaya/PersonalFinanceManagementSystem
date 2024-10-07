using Accounts.DataAccess.Data;
using Accounts.DataAccess.Exceptions;
using Accounts.DataAccess.Repositories.Implementations;
using Accounts.DataAccess.Repositories.Interfaces;

namespace Accounts.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinancialAccountsDbContext _accountsContext;

        private IFinancialAccountRepository? _financialAccountRepository;
        private IFinancialAccountTypeRepository? _financialAccountTypeRepository;
        private ICurrencyRepository? _currencyRepository;
        private IFinancialGoalRepository? _financialGoalRepository;
        private IFinancialGoalTypeRepository? _financialGoalTypeRepository;

        public UnitOfWork(FinancialAccountsDbContext accountsContext)
        {
            _accountsContext = accountsContext ?? throw new DatabaseNotFoundException();
        }

        public IFinancialAccountRepository FinancialAccounts
        {
            get
            {
                _financialAccountRepository ??= new FinancialAccountRepository(_accountsContext);

                return _financialAccountRepository;
            }
        }

        public IFinancialAccountTypeRepository FinancialAccountTypes
        {
            get
            {
                _financialAccountTypeRepository ??= new FinancialAccountTypeRepository(_accountsContext);

                return _financialAccountTypeRepository;
            }
        }

        public ICurrencyRepository Currencies
        {
            get
            {
                _currencyRepository ??= new CurrencyRepository(_accountsContext);

                return _currencyRepository;
            }
        }

        public IFinancialGoalRepository FinancialGoals
        {
            get
            {
                _financialGoalRepository ??= new FinancialGoalRepository(_accountsContext);

                return _financialGoalRepository;
            }
        }

        public IFinancialGoalTypeRepository FinancialGoalTypes
        {
            get
            {
                _financialGoalTypeRepository ??= new FinancialGoalTypeRepository(_accountsContext);

                return _financialGoalTypeRepository;
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _accountsContext.SaveChangesAsync(cancellationToken);
        }
    }
}
