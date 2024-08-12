using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.Request
{
    public class OrderDetailRequest
    {
       public long ProductId { get; set; }
       public int Quantity { get; set; }
    }
}
