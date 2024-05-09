using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.Linq;

namespace Sra.P2rmis.Dal.Repository
{
    /// 
    /// Repository for PanelUserAssignment objects.  Provides CRUD methods and 
    /// associated database services.
    ///
    public class AssignmentTypeThresholdRepository : GenericRepository<AssignmentTypeThreshold>, IAssignmentTypeThresholdRepository
    {

        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public AssignmentTypeThresholdRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion

        #region Services provided        
        /// <summary>
        /// Gets the specified session panel identifier.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public AssignmentTypeThreshold Get(int sessionPanelId)
        {
            var o = Get(x => x.SessionPanelId == sessionPanelId).FirstOrDefault();
            return o;
        }

        public AssignmentTypeThreshold Upsert(int sessionPanelId, int? scientistReviewerSortOrder, int? advocateConsumerSortOrder,
            int? specialistReviewerSortOrder)
        {
            var o = Get(sessionPanelId);
            if (o != null)
            {
                o.ScientistReviewerSortOrder = scientistReviewerSortOrder;
                o.AdvocateConsumerSortOrder = advocateConsumerSortOrder;
                o.SpecialistReviewerSortOrder = specialistReviewerSortOrder;
            }
            else
            {
                o = new AssignmentTypeThreshold();
                o.SessionPanelId = sessionPanelId;
                o.ScientistReviewerSortOrder = scientistReviewerSortOrder;
                o.AdvocateConsumerSortOrder = advocateConsumerSortOrder;
                o.SpecialistReviewerSortOrder = specialistReviewerSortOrder;
                Add(o);
            }
            return o;
        }

        #endregion
        #region Services not provided
        #endregion
        #region Overwritten services provided

        #endregion
    }
}
