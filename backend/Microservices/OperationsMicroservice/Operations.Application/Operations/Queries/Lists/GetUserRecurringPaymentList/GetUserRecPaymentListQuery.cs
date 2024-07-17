using MediatR;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Queries.Lists.GetUserRecurringPaymentList
{
    public class GetUserRecPaymentListQuery : IRequest<List<RecurringPaymentModel>>
    {
        public int UserId { get; set; }
        public PaginationSettings paginationSettings;

        public GetUserRecPaymentListQuery(int id, PaginationSettings settings)
        {
            UserId = id;
            paginationSettings = settings;
        }
    }
}
