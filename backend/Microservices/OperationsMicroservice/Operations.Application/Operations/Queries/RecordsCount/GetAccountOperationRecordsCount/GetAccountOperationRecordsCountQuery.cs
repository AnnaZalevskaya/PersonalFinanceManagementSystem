using MediatR;

namespace Operations.Application.Operations.Queries.RecordsCount.GetAccountOperationRecordsCount
{
    public class GetAccountOperationRecordsCountQuery : IRequest<long>
    {
        public int AccountId { get; set; }

        public GetAccountOperationRecordsCountQuery(int accountId)
        {
            AccountId = accountId;
        }
    }
}
