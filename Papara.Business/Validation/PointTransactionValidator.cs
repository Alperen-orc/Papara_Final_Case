using FluentValidation;
using Papara.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Validation
{
    public class PointTransactionValidator : AbstractValidator<PointTransaction>
    {
        public PointTransactionValidator()
        {
            // TransactionType özelliği için doğrulama kuralları
            RuleFor(x => x.TransactionType)
                .NotEmpty().WithMessage("TransactionType is required.")
                .MaximumLength(10).WithMessage("TransactionType cannot exceed 10 characters.");

            // Points özelliği için doğrulama kuralları
            RuleFor(x => x.Points)
                .GreaterThan(0).WithMessage("Points must be greater than zero.")
                .ScalePrecision(2, 18).WithMessage("Points must have up to 18 digits with 2 decimal places.");

            // InsertDate özelliği için doğrulama kuralları
            RuleFor(x => x.InsertDate)
                .NotEmpty().WithMessage("InsertDate is required.");

            // User doğrulaması
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("PointTransaction must be associated with a User.");

        }
    }
}
