using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.Response
{
    public class ProductCategoryResponse
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
}
