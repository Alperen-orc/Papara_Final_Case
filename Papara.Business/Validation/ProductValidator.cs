using FluentValidation;
using Papara.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            // Name özelliği için doğrulama kuralları
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            // Description özelliği için doğrulama kuralları
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

            // Price özelliği için doğrulama kuralları
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than zero.");

            // IsActive özelliği için doğrulama kuralları
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive is required.");

            // Stock özelliği için doğrulama kuralları
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to zero.");

            // PointPercentage özelliği için doğrulama kuralları
            RuleFor(x => x.PointPercentage)
                .InclusiveBetween(0, 100).WithMessage("PointPercentage must be between 0 and 100.")
                .ScalePrecision(2, 5).WithMessage("PointPercentage must have up to 5 digits with 2 decimal places.");

            // MaxPoint özelliği için doğrulama kuralları
            RuleFor(x => x.MaxPoint)
                .GreaterThanOrEqualTo(0).WithMessage("MaxPoint must be greater than or equal to zero.")
                .ScalePrecision(2, 18).WithMessage("MaxPoint must have up to 18 digits with 2 decimal places.");

            // InsertDate özelliği için doğrulama kuralları
            RuleFor(x => x.InsertDate)
                .NotEmpty().WithMessage("InsertDate is required.");

            // Category doğrulaması
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Product must be associated with a Category.");

            // OrderDetails doğrulaması
            RuleFor(x => x.OrderDetails)
                .NotNull().WithMessage("Product must have associated OrderDetails.");
        }
    }
}
