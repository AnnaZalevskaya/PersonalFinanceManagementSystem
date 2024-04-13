﻿using Auth.Application.Models;
using FluentValidation;

namespace Auth.Application.Validators
{
    public class AuthRequestValidator : AbstractValidator<AuthRequestModel>
    {
        public AuthRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
