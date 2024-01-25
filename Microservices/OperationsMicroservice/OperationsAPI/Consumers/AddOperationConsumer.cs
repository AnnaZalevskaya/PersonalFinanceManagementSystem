using MassTransit;
using MediatR;
using Operations.Application.MassTransit.Requests;
using Operations.Application.Operations.Commands.CreateOperation;
using Operations.Application.Operations.Queries.GetOperationList;

namespace Operations.Application.Consumers
{
    public class AddOperationConsumer : OperationsBaseConsumer, IConsumer<AddOperationRequest>
    {
        public AddOperationConsumer(IMediator mediator) : base (mediator)
        {
            
        }

        public async Task Consume(ConsumeContext<AddOperationRequest> context)
        {
            await _mediator.Send(new CreateOperationCommand(context.Message.Operation));
        }
    }
}
