using Papara.Data.Entities;
using Papara.Data.GenericRepository;
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
    }
}
