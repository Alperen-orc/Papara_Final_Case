using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Entities;

    public class Category:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}

