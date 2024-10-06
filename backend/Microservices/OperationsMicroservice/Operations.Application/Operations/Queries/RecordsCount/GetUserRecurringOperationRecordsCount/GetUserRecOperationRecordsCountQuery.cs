using MediatR;

namespace Operations.Application.Operations.Queries.RecordsCount.GetUserRecurringOperationRecordsCount
{
    public class GetUserRecOperationRecordsCountQuery : IRequest<long>
    {
        public int UserId { get; set; }

        public GetUserRecOperationRecordsCountQuery(int userId)
        {
            UserId = userId;
        }
    }
}
