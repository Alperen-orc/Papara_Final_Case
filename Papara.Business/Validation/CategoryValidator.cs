using FluentValidation;
using Papara.Schema.Request;


namespace Papara.Business.Validation
{
    public class CategoryValidator: AbstractValidator<CategoryRequest>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(25).WithMessage("Name cannot exceed 25 characters.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .MaximumLength(200).WithMessage("Url cannot exceed 200 characters.");

            RuleFor(x => x.Tags)
                .MaximumLength(200).WithMessage("Tags cannot exceed 200 characters.");
        }
    }
}
