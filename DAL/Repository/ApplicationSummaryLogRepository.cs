using System;
using Sra.P2rmis.Dal.Common;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationWorkflow objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>     
    public class ApplicationSummaryLogRepository : GenericRepository<ApplicationSummaryLog>, IApplicationSummaryLogRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationSummaryLogRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services Provided
        
        #endregion
        #region Services Not Provided
        /// <summary>
        /// Delete an object
        /// </summary>
        /// <param name="id">Entity Object Identifier</param>
        public override void Delete(object id)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(id)");
            throw new NotSupportedException(message);
        }

        public override void Delete(ApplicationSummaryLog entityToDelete)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(object)");
            throw new NotSupportedException(message);
        }

        #endregion 
    }
}
