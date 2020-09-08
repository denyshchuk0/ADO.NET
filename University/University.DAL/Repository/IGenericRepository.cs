using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DAL.Repository
{
   public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        IEnumerable<TEntity> Read();
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
