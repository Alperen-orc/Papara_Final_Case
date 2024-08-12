using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.Response
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponAmount { get; set; }
        public decimal UsedPoints { get; set; }
        public DateTime InsertDate { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; }
    }
}
