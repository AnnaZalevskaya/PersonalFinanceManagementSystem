using MediatR;

namespace Operations.Application.Consumers
{
    public class OperationsBaseConsumer
    {
        protected readonly IMediator _mediator;

        public OperationsBaseConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
