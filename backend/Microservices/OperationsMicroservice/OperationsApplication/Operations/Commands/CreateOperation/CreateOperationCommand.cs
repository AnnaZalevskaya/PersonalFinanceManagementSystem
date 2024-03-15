using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.CreateOperation
{
    public class CreateOperationCommand : IRequest
    {
        public CreateOperationModel Model { get; set; }

        public CreateOperationCommand(CreateOperationModel model)
        {
            Model = model;
        }
    }
}
