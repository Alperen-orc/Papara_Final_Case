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
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required.")
             .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive status is required.");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Stock is required.");

            RuleFor(x => x.PointPercentage)
                .NotEmpty().WithMessage("Point Percentage is required.")
                .ScalePrecision(2, 5).WithMessage("Point Percentage must have a maximum of 5 digits and 2 decimal places.");

            RuleFor(x => x.MaxPoint)
                .NotEmpty().WithMessage("Max Point is required.")
                .ScalePrecision(2, 18).WithMessage("Max Point must have a maximum of 18 digits and 2 decimal places.");

            RuleFor(x => x.InsertDate)
                .NotEmpty().WithMessage("InsertDate is required.");
        }
    }
}
