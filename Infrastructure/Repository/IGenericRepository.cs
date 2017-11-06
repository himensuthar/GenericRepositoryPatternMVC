using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IGenericRepository <TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
    }

    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly LearnMVCEntities context;
        public DbSet dbset;
        public GenericRepository(LearnMVCEntities _db)
        {
            this.context = _db;
            this.dbset = context.Set<TEntity>();            
        }
        public TEntity GetById(int id)
        {
            return (TEntity)dbset.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return (IQueryable<TEntity>)dbset;
        }
        public void Insert(TEntity entity)
        {
            dbset.Add(entity);
        }
        public void Edit(TEntity entity) {
            context.Entry(entity).State = EntityState.Modified;   
        }
        public void Delete(TEntity entity) {
            context.Entry(entity).State = EntityState.Deleted;
        }
    }
}
