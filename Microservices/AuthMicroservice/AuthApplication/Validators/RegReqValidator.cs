using Auth.Application.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Auth.Application.Validators
{
    public class RegReqValidator : AbstractValidator<RegisterRequest>
    {
        public RegReqValidator()
        {
            RuleFor(req => req.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(req => req.Username)
                .NotEmpty();

            RuleFor(req => req.Phonenumber)
                .Length(13)
                .Matches(new Regex(@"^\+375(17|29|33|44)[0-9]{7}$"))
                .NotEmpty();

            RuleFor(req => req.Password)
                .MinimumLength(8)
                .NotEmpty();

            RuleFor(req => req.PasswordConfirm)
                .Equal(req => req.Password);
        }
    }
}
