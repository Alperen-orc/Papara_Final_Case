using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Schema.Response
{
    public class CategoryResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
    }
}
