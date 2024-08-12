using Microsoft.EntityFrameworkCore;
using Papara.Data.DatabaseContext;
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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Coupon> CouponRepository { get; }
        public IOrderRepository OrderRepository { get; }

    public UnitOfWork(Context context)
        {
            _context = context;

            CategoryRepository = new GenericRepository<Category>(_context);
            ProductRepository = new GenericRepository<Product>(_context);
            UserRepository = new GenericRepository<User>(_context);
            CouponRepository = new GenericRepository<Coupon>(_context);
            OrderRepository = new OrderRepository(_context);
        }

        public void Dispose()
        {
        }
        public async Task SaveDatabase()
        {
            await _context.SaveChangesAsync();
        }
    }
}