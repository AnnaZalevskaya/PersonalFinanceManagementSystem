using FluentValidation;
using Operations.Core.Entities;

namespace Operations.Application.Validators
{
    public class OperationValidator : AbstractValidator<Operation>
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
