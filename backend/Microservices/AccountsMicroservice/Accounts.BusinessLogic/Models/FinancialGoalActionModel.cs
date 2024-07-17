using Accounts.BusinessLogic.Models.Enums;

namespace Accounts.BusinessLogic.Models
{
    public class FinancialGoalActionModel
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
    }
}
