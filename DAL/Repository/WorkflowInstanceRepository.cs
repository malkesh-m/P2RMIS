using System;
using Sra.P2rmis.Dal.Common;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for WorkflowTepmplate objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// OBJECT (I.E. USER) NEEDS TO BE CHANGED WHEN CRAIG DEFINES DATABASE  
    /// ALSO NEED TO CHANGE TYPE IN Delete() below
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class WorkflowInstanceRepository: GenericRepository<User>, IWorkflowInstanceRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public WorkflowInstanceRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services provided
        /// <summary>
        /// Determines if the instance id is valid
        /// </summary>
        /// <param name="instanceId">Instance Id</param>
        /// <returns></returns>
        public bool IsInstanceValid(int instanceId)
        {
            return (
                    (instanceId > 0) &&
                    (this.GetByID(instanceId) != null)
                    );
        }
        #endregion
        #region Services Not Provided
        /// <summary>
        /// Delete an object
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(object id)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(id)");
            throw new NotSupportedException(message);
        }

        public override void Delete(User entityToDelete)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(object)");
            throw new NotSupportedException(message);
        }

        #endregion 
    }
}
