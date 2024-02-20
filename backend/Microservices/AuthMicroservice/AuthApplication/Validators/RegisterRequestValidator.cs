using Auth.Application.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Auth.Application.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(req => req.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(req => req.Username)
                .NotEmpty();

            RuleFor(req => req.PhoneNumber)
                .NotEmpty()
                .Length(13)
                .Matches(new Regex(@"^\+375(17|29|33|44)[0-9]{7}$"));

            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(req => req.PasswordConfirm)
                .Equal(req => req.Password);
        }
    }
}
