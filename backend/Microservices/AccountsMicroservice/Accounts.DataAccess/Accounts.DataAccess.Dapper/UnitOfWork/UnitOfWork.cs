using Accounts.DataAccess.Dapper.Data;
using Accounts.DataAccess.Dapper.Exceptions;
using Accounts.DataAccess.Dapper.Repositories.Implementations;
using Accounts.DataAccess.Dapper.Repositories.Interfaces;

namespace Accounts.DataAccess.Dapper.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext _context;

        private IAccountStatisticsRepository? _financialAccountStatisticRepository; 
        private IGoalStatusRepository? _financialGoalStatuRepository;

        public UnitOfWork(DapperContext context)
        {
            _context = context ?? throw new DatabaseNotFoundException();
        }

        public IAccountStatisticsRepository FinancialAccountStatistics
        {
            get
            {
                _financialAccountStatisticRepository ??= new AccountStatisticsRepository(_context);

                return _financialAccountStatisticRepository;
            }
        }

        public IGoalStatusRepository GoalStatuses
        {
            get
            {
                _financialGoalStatuRepository ??= new GoalStatusRepository(_context);

                return _financialGoalStatuRepository;
            }
        }
    }
}
