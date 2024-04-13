using FluentValidation;
using Operations.Application.Models;

namespace Operations.Application.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Id)
                .NotEmpty()
                .Must((category, id) => id == category.Id)
                .WithMessage("Id must not be changed!");

            RuleFor(category => category.Name)
                .NotEmpty();

            RuleFor(category => category.CategoryType)
                .NotEmpty();
        }
    }
}
