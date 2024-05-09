
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// 
    /// Repository for PanelApplicationReviewerCoiDetailRepository.  Provides CRUD methods and 
    /// associated database services.
    ///
    public class PanelApplicationReviewerCoiDetailRepository : GenericRepository<PanelApplicationReviewerCoiDetail>, IPanelApplicationReviewerCoiDetailRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public PanelApplicationReviewerCoiDetailRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
		
		#region Services provided
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        public void Delete(int id, int userId)
        {
            var entity = GetByID(id);
            Helper.UpdateDeletedFields(entity, userId);
            Delete(id);
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        public void Delete(PanelApplicationReviewerCoiDetail entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        
        #endregion
        #region Services not provided
        #endregion
        #region Overwritten services provided

        #endregion
    }
}
