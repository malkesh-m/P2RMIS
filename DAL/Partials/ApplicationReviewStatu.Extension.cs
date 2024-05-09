using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationReviewStatu object. 
    /// </summary>	
    public partial class ApplicationReviewStatu: IStandardDateFields, ISpecifyEntitySetName
    {
        /// <summary>
        /// Populate the entity for modification.
        /// </summary>
        /// <param name="reviewStatusId">New ReviewStatus entity identifier</param>
        public void Populate(int reviewStatusId)
        {
            this.ReviewStatusId = reviewStatusId;
        }
        /// <summary>
        /// Returns the EntitySet Name for this entity.
        /// </summary>
        public string EntitySetName
        {
            get { return "ApplicationReviewStatus"; }
        }
        /// <summary>
        /// Populate a new entity or modify an existing entity.
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="reviewStatusId">ReviewStatus entity identifier</param>
        /// <param name="panelApplicaitonId">PanelApplicaiton entity identifier</param>
        /// 
        public void Populate(int applicationId, int reviewStatusId, int panelApplicaitonId)
        {
            this.PanelApplicationId = panelApplicaitonId;
            Populate(reviewStatusId);
        }
    }
}
