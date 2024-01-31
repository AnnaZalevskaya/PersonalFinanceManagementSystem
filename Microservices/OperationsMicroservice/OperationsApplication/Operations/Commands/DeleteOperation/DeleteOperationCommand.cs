using MediatR;

namespace Operations.Application.Operations.Commands.DeleteOperation
{
    public class DeleteOperationCommand : IRequest
    {
        public int AccountId { get; set; }
        public string Id { get; set; }

        public DeleteOperationCommand(int accountId, string id)
        {
            AccountId = accountId;
            Id = id;
        }
    }
}
