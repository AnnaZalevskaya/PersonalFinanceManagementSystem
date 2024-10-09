using Accounts.DataAccess.Entities.Base;

namespace Accounts.DataAccess.Entities
{
    public class FinancialGoalType : BaseEntity
    {
        public List<FinancialGoal>? FinancialGoals { get; set; }
    }
}
