using Accounts.DataAccess.Entities.Base;

namespace Accounts.DataAccess.Entities
{
    public class FinancialGoal : BaseEntity
    {
        public int AccountId { get; set; }
        public int TypeId { get; set; }
        public FinancialGoalType? Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
    }
}
