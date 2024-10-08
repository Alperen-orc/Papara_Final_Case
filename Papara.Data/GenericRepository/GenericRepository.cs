﻿using Microsoft.EntityFrameworkCore;
using Papara.Base.Entity;
using Papara.Data.DatabaseContext;
using Papara.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Context _context;
        public GenericRepository(Context context)
        {
            _context = context;
        }
        public async Task Delete(long Id)
        {
            var entity=await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Set<TEntity>(),x=>x.Id==Id);
            if (entity!=null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(long Id)
        {
            var entity=await _context.Set<TEntity>().FirstOrDefaultAsync(x=>x.Id==Id);
            return entity;
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            entity.InsertDate = DateTime.UtcNow;
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public  void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public async Task<TEntity> FindByEmailAsync(string email)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Email") == email);
        }

    }
}
