using Accounts.BusinessLogic.Models;
using FluentValidation;

namespace Accounts.BusinessLogic.Validators
{
    public class FinancialAccountTypeValidator : AbstractValidator<FinancialAccountTypeModel>
    {
        public FinancialAccountTypeValidator()
        {
            RuleFor(type => type.Id)
                .NotEmpty()
                .Must((type, id) => id == type.Id)
                .WithMessage("Id must not be changed!");

            RuleFor(type => type.Name)
                .NotEmpty();
        }
    }
}
