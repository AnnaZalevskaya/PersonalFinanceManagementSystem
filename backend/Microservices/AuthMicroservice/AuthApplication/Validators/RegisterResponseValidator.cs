using Auth.Application.Models;
using FluentValidation;

namespace Auth.Application.Validators
{
    public class RegisterResponseValidator : AbstractValidator<RegisterResponse>
    {
        public RegisterResponseValidator() {
            RuleFor(resp => resp.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(resp => resp.Username)
                .NotEmpty();
        }
    }
}
