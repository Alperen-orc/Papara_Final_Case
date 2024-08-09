using FluentValidation;
using Papara.Data.Entities;
using Papara.Schema.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Validation
{
    public class CouponValidator : AbstractValidator<CouponRequest>
    {
        public CouponValidator()
        {
            // Code özelliği için doğrulama kuralları
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(10).WithMessage("Code cannot exceed 10 characters.");

            // Amount özelliği için doğrulama kuralları
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .ScalePrecision(2, 18).WithMessage("Amount must have up to 18 digits with 2 decimal places.");

            // IsActive özelliği için doğrulama kuralları
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive is required.");

            // ExpiryDate özelliği için doğrulama kuralları
            RuleFor(x => x.ExpiryDate)
                .NotEmpty().WithMessage("ExpiryDate is required.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("ExpiryDate cannot be in the past.");
        }
    }
}
