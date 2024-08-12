using Papara.Data.Entities;
using Papara.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetCartByUserId(long userId);
    }
}
