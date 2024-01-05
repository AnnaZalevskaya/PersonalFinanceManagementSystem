using Accounts.DataAccess.Entities;
using FluentValidation;

namespace Accounts.BusinessLogic.Validators
{
    public class FinancialAccountValidator : AbstractValidator<FinancialAccount>
    {
        public FinancialAccountValidator() 
        {
            RuleFor(type => type.Id)
                .NotEmpty()
                .Must((type, id) => id == type.Id)
                .WithMessage("Id must not be changed!");

            RuleFor(account => account.Name)
                .NotEmpty();

            RuleFor(account => account.AccountTypeId)
                .NotEmpty();

            RuleFor(type => type.CurrencyId)
                .NotEmpty()
                .NotNull();
        }
    }
}
