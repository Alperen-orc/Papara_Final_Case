﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.Request
{
    public class OrderRequest
    {
        public long UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponAmount { get; set; }
        public decimal UsedPoints { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
}
