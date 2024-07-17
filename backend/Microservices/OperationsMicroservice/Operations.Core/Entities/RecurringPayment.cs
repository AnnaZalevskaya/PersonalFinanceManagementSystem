using Operations.Core.Entities.Base;

namespace Operations.Core.Entities
{
    public class RecurringPayment : BaseOperationEntity
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public TimeOnly ExecutionTime { get; set; }
        public int IntervalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
