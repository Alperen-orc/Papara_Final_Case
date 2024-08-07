using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Entities;

    public class OrderDetail:BaseEntity
    {
        public long OrderId { get; set; }
        public required virtual Order Order { get; set; }
        public long ProductId { get; set; }
        public required virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }    

    }

