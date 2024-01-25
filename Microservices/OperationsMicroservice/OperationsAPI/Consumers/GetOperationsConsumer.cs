using MassTransit;
using MediatR;
using Operations.Application.MassTransit.Requests;
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
            var operations = _mediator.Send(new GetOperationListQuery(context.Message.PaginationSettings));
            await context.RespondAsync(operations);

        }
    }
}
