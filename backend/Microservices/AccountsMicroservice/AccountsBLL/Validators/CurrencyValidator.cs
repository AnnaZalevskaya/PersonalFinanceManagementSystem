using Accounts.DataAccess.Entities;
using FluentValidation;

namespace Accounts.BusinessLogic.Validators
{
    public class CurrencyValidator : AbstractValidator<Currency>
    {
        public CurrencyValidator()
        {
            RuleFor(currency => currency.Id)
                .NotEmpty()
                .Must((type, id) => id == type.Id)
                .WithMessage("Id must not be changed!");

            RuleFor(currency => currency.Name)
                .NotEmpty();

            RuleFor(currence => currence.Abbreviation)
                .NotEmpty()
                .Length(3);
        }
    }
}
