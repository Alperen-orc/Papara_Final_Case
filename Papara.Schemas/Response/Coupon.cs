using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.Response
{
    public class CouponResponse
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
