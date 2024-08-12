using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Papara.Data.Entities;

    public class Order:BaseEntity
{
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public decimal TotalAmount { get; set; }
        public string CouponCode { get; set; }=string.Empty;
        public decimal CouponAmount { get; set; }
        public decimal UsedPoints { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PointTransaction> PointTransactions { get; set; }

}

