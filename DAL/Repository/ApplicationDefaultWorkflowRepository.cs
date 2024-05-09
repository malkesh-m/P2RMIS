using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationDefaultWorkflow objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class ApplicationDefaultWorkflowRepository: GenericRepository<ApplicationDefaultWorkflow>, IApplicationDefaultWorkflowRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationDefaultWorkflowRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
    }
}
