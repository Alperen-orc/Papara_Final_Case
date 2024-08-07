using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Entities;

    public class UserCoupon:BaseEntity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }
        public DateTime UsedAt { get; set; }
    }

