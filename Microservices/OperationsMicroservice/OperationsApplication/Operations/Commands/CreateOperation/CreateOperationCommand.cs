using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.CreateOperation
{
    public class CreateOperationCommand : IRequest
    {
        public int AccountId { get; set; }
        public Dictionary<string, object> Description { get; set; }

        public CreateOperationCommand(CreateOperationModel model)
        {
            AccountId = model.AccountId;
            Description = model.Description;
        }
    }
}
