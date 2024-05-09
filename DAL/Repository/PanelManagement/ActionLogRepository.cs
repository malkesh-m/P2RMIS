using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ActionLog objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class ActionLogRepository: GenericRepository<ActionLog>, IActionLogRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ActionLogRepository(P2RMISNETEntities context): base(context)
        {     
             
        }
        #endregion
    }
}
