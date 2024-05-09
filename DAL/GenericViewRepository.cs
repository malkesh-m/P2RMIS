using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Generic repository for views (which do not support CRUD operations)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericViewRepository<TEntity> : GenericRepository<TEntity> where TEntity : class
    {
        #region Constants & Messages
        private const string NotSupportedMessage = "{0} method is not supported for this repository.";
        #endregion
        #region Construction; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public GenericViewRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion 
        public override void Add(TEntity entity)
        {
            string message = string.Format(NotSupportedMessage, "Add");
            throw new NotSupportedException(message);
        }

        public override void Delete(object id)
        {
            string message = string.Format(NotSupportedMessage, "Delete by object id");
            throw new NotSupportedException(message);
        }

        public override void Delete(TEntity entityToDelete)
        {
            string message = string.Format(NotSupportedMessage, "Delete");
            throw new NotSupportedException(message);
        }

        public override void Update(TEntity entityToUpdate)
        {
            string message = string.Format(NotSupportedMessage, "Update");
            throw new NotSupportedException(message);
        }
    }
}
