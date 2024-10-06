using Accounts.BusinessLogic.Models;
using FluentValidation;

namespace Accounts.BusinessLogic.Validators
{
    public class FinancialAccountActionValidator : AbstractValidator<FinancialAccountActionModel>
    {
        public FinancialAccountActionValidator()
        {
            RuleFor(account => account.Name)
                .NotEmpty();

            RuleFor(account => account.AccountTypeId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(account => account.CurrencyId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
