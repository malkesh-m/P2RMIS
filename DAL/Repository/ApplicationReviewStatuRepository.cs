using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationReviewStatu objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>   
    public class ApplicationReviewStatusRepository : GenericRepository<ApplicationReviewStatu>, IApplicationReviewStatusRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationReviewStatusRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services
        #endregion
    }
}
