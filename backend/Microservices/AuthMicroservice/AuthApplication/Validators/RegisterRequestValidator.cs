using Auth.Application.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Auth.Application.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(request => request.Username)
                .NotEmpty();

            RuleFor(request => request.PhoneNumber)
                .NotEmpty()
                .Length(13)
                .Matches(new Regex(@"^\+375(17|29|33|44)[0-9]{7}$"));

            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(request => request.PasswordConfirm)
                .Equal(req => req.Password);
        }
    }
}
