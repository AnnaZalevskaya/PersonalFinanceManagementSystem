using MassTransit;
using MassTransit.Transports;
using MediatR;
using Operations.Application.MassTransit.Requests;
using Operations.Application.Operations.Queries.GetOperationDetails;

namespace Operations.Application.Consumers
{
    public class GetOperationConsumer : OperationsBaseConsumer, IConsumer<GetOperationRequest>
    {
        public GetOperationConsumer(IMediator mediator) : base(mediator) 
        {

        }

        public async Task Consume(ConsumeContext<GetOperationRequest> context)
        {
            var operation = await _mediator.Send(new GetOperationDetailsQuery(context.Message.Id));
            await context.RespondAsync(operation);
        }
    }
}
