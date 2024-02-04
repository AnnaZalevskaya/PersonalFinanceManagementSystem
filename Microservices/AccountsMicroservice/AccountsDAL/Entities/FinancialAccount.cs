namespace Accounts.DataAccess.Entities
{
    public class FinancialAccount : BaseEntity
    {
        public int AccountTypeId { get; set; }
        public FinancialAccountType? AccountType { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int UserId { get; set; }
    }
}
