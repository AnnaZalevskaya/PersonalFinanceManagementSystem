using MassTransit;
using MediatR;
using Operations.Application.Models;
using Operations.Application.Operations.Commands.CreateOperation;

namespace Operations.Application.Consumers
{
    public class AddOperationConsumer : OperationsBaseConsumer, IConsumer<CreateOperationModel>
    {
        public AddOperationConsumer(IMediator mediator) : base (mediator)
        {
            
        }

        public async Task Consume(ConsumeContext<CreateOperationModel> context)
        {          
            await context.RespondAsync(_mediator.Send(new CreateOperationCommand(context.Message)));
        }
    }
}
