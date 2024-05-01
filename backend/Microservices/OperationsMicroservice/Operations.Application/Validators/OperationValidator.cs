using FluentValidation;
using Operations.Application.Models;

namespace Operations.Application.Validators
{
    public class OperationValidator : AbstractValidator<OperationModel>
    {
        public OperationValidator() 
        {
            RuleFor(operation => operation.Id)
                .NotEmpty()
                .Must((operation, id) => id == operation.Id)
                .WithMessage("Id must not be changed!");
        }
    }
}
