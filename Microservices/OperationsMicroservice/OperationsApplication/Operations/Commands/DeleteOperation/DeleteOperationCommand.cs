using MediatR;

namespace Operations.Application.Operations.Commands.DeleteOperation
{
    public class DeleteOperationCommand : IRequest
    {
        public string Id { get; set; }

        public DeleteOperationCommand(string id)
        {
            Id = id;
        }
    }
}
