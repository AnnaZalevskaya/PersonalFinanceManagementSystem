using FluentValidation;
using Operations.Application.Models;

namespace Operations.Application.Validators
{
    public class CreateOperationValidator : AbstractValidator<CreateOperationModel>
    {
        public CreateOperationValidator()
        {
            RuleFor(createModel => createModel.AccountId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
