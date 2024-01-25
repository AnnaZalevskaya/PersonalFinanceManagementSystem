using MassTransit;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.MassTransit.Requests;
using Operations.Application.Operations.Queries.GetOperationList;

namespace Operations.API.Consumers
{
    public class GetOperationsByAccountConsumer : OperationsBaseConsumer, IConsumer<GetOperationsByAccountRequest>
    {
        public GetOperationsByAccountConsumer(IMediator mediator) : base(mediator)
        {
            
        }

        public async Task Consume(ConsumeContext<GetOperationsByAccountRequest> context)
        {
            var operations = _mediator.Send(
                new GetOperationListByAccountIdQuery(context.Message.Id, context.Message.PaginationSettings));
            await context.RespondAsync(operations);
        }
    }
}
