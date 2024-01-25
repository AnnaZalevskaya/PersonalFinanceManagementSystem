using MassTransit;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.MassTransit.Requests;
using Operations.Application.Operations.Commands.DeleteOperation;

namespace Operations.API.Consumers
{
    public class DeleteOperationConsumer : OperationsBaseConsumer, IConsumer<DeleteOperationRequest>
    {
        public DeleteOperationConsumer(IMediator mediator) :base(mediator)
        {
            
        }

        public async Task Consume(ConsumeContext<DeleteOperationRequest> context)
        {
            await _mediator.Send(new DeleteOperationCommand(context.Message.Id));
        }
    }
}
