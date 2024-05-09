using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete ApplicationReviewStatu entities.
    /// </summary>
    public class ApplicationReviewStatusesServiceAction : ServiceAction<ApplicationReviewStatu>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationReviewStatusesServiceAction()
        {
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        public void Populate(int reviewStatusId)
        {
            this.ReviewStatusId = reviewStatusId;
        }
        #endregion
        #region Properties
        public int ReviewStatusId { get; private set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserApplicationComment entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">UserApplicationComment entity</param>
        protected override void Populate(ApplicationReviewStatu entity)
        {
            entity.Populate(this.ReviewStatusId);
        }
        /// <summary>
        /// Indicates if the UserApplicationComment has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  
                //
                return true;
            }
        }
        #endregion
    }
}
