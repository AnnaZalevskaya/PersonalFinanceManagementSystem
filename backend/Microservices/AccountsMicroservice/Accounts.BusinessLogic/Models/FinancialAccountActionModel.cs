namespace Accounts.BusinessLogic.Models
{
    public class FinancialAccountActionModel
    {
        public string? Name { get; set; }
        public int AccountTypeId { get; set; }
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
    }
}
