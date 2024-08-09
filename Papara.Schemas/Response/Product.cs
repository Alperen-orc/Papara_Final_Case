using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.Response
{
    public class ProductResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Stock { get; set; }
        public decimal PointPercentage { get; set; }
        public decimal MaxPoint { get; set; }
        public bool IsActive { get; set; }
        public long CategoryId {  get; set; } 

    }
}
