using Microsoft.EntityFrameworkCore;
using Papara.Data.DatabaseContext;
using Papara.Data.Entities;
using Papara.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetCartByUserId(long userId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderDetails.Count == 0);
        }
    }

}
