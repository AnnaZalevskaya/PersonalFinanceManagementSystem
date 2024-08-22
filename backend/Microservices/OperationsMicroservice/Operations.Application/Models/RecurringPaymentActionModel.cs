using Operations.Application.Models.Enums;

namespace Operations.Application.Models
{
    public class RecurringPaymentActionModel
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public TimeOnly ExecutionTime { get; set; }
        public int IntervalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
