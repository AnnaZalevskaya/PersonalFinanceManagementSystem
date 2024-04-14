using FluentValidation;
using Operations.Application.Models;

namespace Operations.Application.Validators
{
    public class CategoryTypeValidator : AbstractValidator<CategoryTypeModel>
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
