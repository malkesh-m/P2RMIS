using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Sra.P2rmis.Dal
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>  where TEntity : class
    {
        
        //Class variables are declared for the database context and for the entity set that the repository is instantiated for
        internal P2RMISNETEntities context;
        internal DbSet<TEntity> dbSet;

        //The constructor accepts a database context instance and initializes the entity set variable
        public GenericRepository(P2RMISNETEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        /// <summary>
        /// The parameterless constructor is used only for testing.
        /// </summary>
        public GenericRepository ()
        {

        }
        public virtual IEnumerable<TEntity> Get(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        /// <summary>
        /// Get with eager loading for included data records
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The orderBy clause.</param>
        /// <param name="includeProperties">The included properties.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetEager(
             Expression<Func<TEntity, bool>> filter,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
             params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            for (var i = 0; i < includeProperties.Length; i++) 
            {
                query = query.Include(includeProperties[i]);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        /// <summary>
        /// Return entity as a query-able type.
        /// </summary>
        /// <returns>Entity set as a query-able object</returns>
        public virtual IQueryable<TEntity> Select()
        {
            return context.Set<TEntity>();
        }
    }
}