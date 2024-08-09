using FluentValidation;
using Papara.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            // TotalAmount özelliği için doğrulama kuralları
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("TotalAmount must be greater than zero.")
                .ScalePrecision(2, 18).WithMessage("TotalAmount must have up to 18 digits with 2 decimal places.");

            // CouponCode özelliği için doğrulama kuralları
            RuleFor(x => x.CouponCode)
                .MaximumLength(10).WithMessage("CouponCode cannot exceed 10 characters.");

            RuleFor(x => x.CouponAmount)
                .GreaterThanOrEqualTo(0).WithMessage("CouponAmount must be greater than or equal to zero.")
                .ScalePrecision(2, 18).WithMessage("CouponAmount must have up to 18 digits with 2 decimal places.")
                .When(x => x.CouponAmount != null);

            RuleFor(x => x.UsedPoints)
                .GreaterThanOrEqualTo(0).WithMessage("UsedPoints must be greater than or equal to zero.")
                .ScalePrecision(2, 18).WithMessage("UsedPoints must have up to 18 digits with 2 decimal places.")
                .When(x => x.UsedPoints != null);

            // InsertDate özelliği için doğrulama kuralları
            RuleFor(x => x.InsertDate)
                .NotEmpty().WithMessage("InsertDate is required.");

            // OrderDetails doğrulaması
            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("Order must have at least one OrderDetail.");

            // User doğrulaması
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Order must be associated with a User.");
        }
    }
}
