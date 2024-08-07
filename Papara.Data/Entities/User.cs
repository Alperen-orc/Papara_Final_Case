using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Entities;

    public class User:BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal WalletBalance { get; set; }
        public decimal PointBalance { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<UserCoupon>? UserCoupons { get; set; }
        public virtual ICollection<PointTransaction>? PointTransactions { get; set; }
    }

