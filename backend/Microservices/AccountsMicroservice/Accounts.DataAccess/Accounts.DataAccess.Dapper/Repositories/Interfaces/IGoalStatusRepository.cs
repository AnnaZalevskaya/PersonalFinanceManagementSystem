namespace Accounts.DataAccess.Dapper.Repositories.Interfaces
{
    public interface IGoalStatusRepository
    {
        Task UpdateGoalStatusAsync(int goalId, int newStatus);
    }
}
