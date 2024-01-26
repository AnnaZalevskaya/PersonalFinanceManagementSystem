using MassTransit;
using MediatR;
using Operations.Application.MassTransit.Requests;
using Operations.Application.MassTransit.Responses;
using Operations.Application.Operations.Queries.GetOperationList;

namespace Operations.Application.Consumers
{
    public class GetOperationsConsumer : OperationsBaseConsumer, IConsumer<GetAllOperationsRequest>
    {
        public GetOperationsConsumer(IMediator mediator) : base(mediator)
        {

        }

        public async Task Consume(ConsumeContext<GetAllOperationsRequest> context)
        {
            var operations = await _mediator.Send(new GetOperationListQuery(context.Message.PaginationSettings));

            var response = new GetOperationsResponse
            {
                Operations = operations, 
                Count = operations.Count
            };

            await context.RespondAsync(response);
        }
    }
}
