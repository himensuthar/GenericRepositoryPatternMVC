using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IGenericRepository <TEntity> where TEntity : class 
    {
        TEntity GetById(long id);
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetPaginated(IQueryable<TEntity> query,dynamic whereClause, string searchBy, int take, int skip, out int totalRecords, out int recordsFiltered, string sort, bool sortdir);
        
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
        public TEntity GetById(long id)
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

       public  IQueryable<TEntity> GetPaginated(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> whereClause, string searchBy, int take, int skip, out int totalRecords, out int recordsFiltered, string sortBy, bool sortDir)
        {
            totalRecords = query.Count();
            query = query.Where(whereClause);
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }
            recordsFiltered = query.Count();
            query = query
                   .OrderBy(sortBy, sortDir)
                   .Skip(skip)
                   .Take(take);
            return query;
        }
    }
}
