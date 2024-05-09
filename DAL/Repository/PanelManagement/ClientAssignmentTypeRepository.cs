using System;
using Sra.P2rmis.Dal.Common;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{

    /// 
    /// Repository for ClientAssignmentType objects.  Provides CRUD methods and 
    /// associated database services.
    ///    
    public class ClientAssignmentTypeRepository: GenericRepository<ClientAssignmentType>, IClientAssignmentTypeRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ClientAssignmentTypeRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
		
		#region Services provided
        
        #endregion
        #region Services not provided
        /// <summary>
        /// Delete an object by id
        /// </summary>
        /// <param name="id">ClientAssignmentType identifier</param>
        public override void Delete(object id)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(id)");
            throw new NotSupportedException(message);
        }
        /// <summary>
        /// Delete an object
        /// </summary>
        /// <param name="entityToDelete">ClientAssignmentType object</param>
        public override void Delete(ClientAssignmentType entityToDelete)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(object)");
            throw new NotSupportedException(message);
        }
        #endregion
        #region Overwritten services provided

        #endregion
    }
}
