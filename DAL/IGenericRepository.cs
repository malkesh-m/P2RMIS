using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sra.P2rmis.Dal
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        /// <summary>
        /// Get with eager loading for included data records
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The orderBy clause.</param>
        /// <param name="includeProperties">The included properties.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetEager(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(object id);
        void Add(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        IQueryable<TEntity> Select();
    }
}
