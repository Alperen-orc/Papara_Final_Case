using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(long Id);
        Task<TEntity> Insert (TEntity entity);
        Task<List<TEntity>> GetAll();
        Task Delete(long Id);
        void Update(TEntity entity);
    }
}
