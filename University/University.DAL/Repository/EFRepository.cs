using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DAL.Repository
{
    public class EFRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> set;

        public EFRepository(DbContext _context)
        {
            context = _context;
            set = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            
            set.Add(entity);
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            set.Remove(entity);
            context.SaveChanges();
            }

        public IEnumerable<TEntity> Read()
        {
            return set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().AddOrUpdate(entity);
            context.SaveChanges();
        }
    }
}
