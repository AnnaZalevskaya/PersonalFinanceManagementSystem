using Accounts.DataAccess.Dapper.Data;
using Accounts.DataAccess.Dapper.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace Accounts.DataAccess.Dapper.Repositories.Implementations
{
    public class GoalStatusRepository : IGoalStatusRepository
    {
        private readonly DapperContext _context;

        public GoalStatusRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task UpdateGoalStatusAsync(int goalId, int newStatus)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new { goal_id = goalId, new_status = newStatus };
                await connection
                    .QueryAsync("update_goal_status", 
                    parameters, 
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
