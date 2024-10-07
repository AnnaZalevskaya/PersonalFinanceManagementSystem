using Accounts.DataAccess.Dapper.Repositories.Interfaces;

namespace Accounts.DataAccess.Dapper.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAccountStatisticsRepository FinancialAccountStatistics { get; }
        IGoalStatusRepository GoalStatuses { get; }
    }
}
