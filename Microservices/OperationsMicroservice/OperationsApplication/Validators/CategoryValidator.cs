using FluentValidation;
using Operations.Core.Entities;

namespace Operations.Application.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Id)
                .NotEmpty()
                .Must((category, id) => id == category.Id)
                .WithMessage("Id must not be changed!");

            RuleFor(category => category.Name)
                .NotEmpty();

            RuleFor(category => category.CategoryTypeId)
                .NotEmpty();
        }
    }
}
