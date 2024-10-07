namespace Accounts.DataAccess.Dapper.Repositories.Interfaces
{
    public interface IGoalStatusRepository
    {
        Task UpdateGoalStatus(int goalId, int newStatus);
    }
}
