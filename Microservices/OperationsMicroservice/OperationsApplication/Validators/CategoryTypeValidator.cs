using FluentValidation;
using Operations.Core.Entities;

namespace Operations.Application.Validators
{
    public class CategoryTypeValidator : AbstractValidator<CategoryType>
    {
        public CategoryTypeValidator()
        {
            RuleFor(type => type.Id)
                .NotEmpty()
                .Must((type, id) => id == type.Id)
                .WithMessage("Id must not be changed!");

            RuleFor(type => type.Name)
                .NotEmpty();
        }
    }
}
