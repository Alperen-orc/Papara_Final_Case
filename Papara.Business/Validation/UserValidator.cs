using FluentValidation;
using Papara.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            // FirstName özelliği için doğrulama kuralları
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .MaximumLength(20).WithMessage("FirstName cannot exceed 20 characters.");

            // LastName özelliği için doğrulama kuralları
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .MaximumLength(20).WithMessage("LastName cannot exceed 20 characters.");

            // Email özelliği için doğrulama kuralları
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("Email cannot exceed 50 characters.")
                .EmailAddress().WithMessage("A valid email address is required.");

            // PasswordHash özelliği için doğrulama kuralları
            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Password is required.");

            // Role özelliği için doğrulama kuralları
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.");

            // Status özelliği için doğrulama kuralları
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");

            // WalletBalance özelliği için doğrulama kuralları
            RuleFor(x => x.WalletBalance)
                .GreaterThanOrEqualTo(0).WithMessage("WalletBalance must be greater than or equal to zero.")
                .ScalePrecision(2, 18).WithMessage("WalletBalance must have up to 18 digits with 2 decimal places.");

            // PointBalance özelliği için doğrulama kuralları
            RuleFor(x => x.PointBalance)
                .GreaterThanOrEqualTo(0).WithMessage("PointBalance must be greater than or equal to zero.")
                .ScalePrecision(2, 18).WithMessage("PointBalance must have up to 18 digits with 2 decimal places.");

            // InsertDate özelliği için doğrulama kuralları
            RuleFor(x => x.InsertDate)
                .NotEmpty().WithMessage("InsertDate is required.");
        }
    }
}
