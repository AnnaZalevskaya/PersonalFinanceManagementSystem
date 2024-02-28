﻿using Auth.Application.Models;
using FluentValidation;

namespace Auth.Application.Validators
{
    public class RegisterResponseValidator : AbstractValidator<RegisterResponse>
    {
        public RegisterResponseValidator() {
            RuleFor(response => response.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(response => response.Username)
                .NotEmpty();
        }
    }
}
