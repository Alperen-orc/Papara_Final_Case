using Papara.Data.Entities;
using Papara.Data.GenericRepository;
using Papara.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveDatabase();
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Coupon> CouponRepository { get; }
        IOrderRepository OrderRepository { get; }

    }
}
