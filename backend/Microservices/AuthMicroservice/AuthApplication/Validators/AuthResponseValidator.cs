using Auth.Application.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Auth.Application.Validators
{
    public class AuthResponseValidator : AbstractValidator<AuthResponse>
    {
        public AuthResponseValidator()
        {
            RuleFor(response => response.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(response => response.Username)
                .NotEmpty();

            RuleFor(response => response.PhoneNumber)
                .NotEmpty()
                .Length(13)
                .Matches(new Regex(@"^\+375(17|29|33|44)[0-9]{7}$"));
        }
    }
}
