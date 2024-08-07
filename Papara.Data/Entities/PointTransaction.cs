using Papara.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Entities;

    public class PointTransaction:BaseEntity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string TransactionType { get; set; }
        public decimal Points { get; set; }
    }

