using Auth.Application.Models;
using FluentValidation;

namespace Auth.Application.Validators
{
    public class RegRespValidator : AbstractValidator<RegisterResponse>
    {
        public RegRespValidator() {
            RuleFor(resp => resp.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(resp => resp.Username)
                .NotEmpty();
        }
    }
}
