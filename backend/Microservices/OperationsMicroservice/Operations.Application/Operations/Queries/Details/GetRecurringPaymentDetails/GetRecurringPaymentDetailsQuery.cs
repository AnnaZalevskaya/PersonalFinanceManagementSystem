using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Details.GetRecurringPaymentDetails
{
    public class GetRecurringPaymentDetailsQuery : IRequest<RecurringPaymentModel>
    {
        public string Id { get; set; }

        public GetRecurringPaymentDetailsQuery(string id)
        {
            Id = id;
        }
    }
}
