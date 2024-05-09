
namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Return results for the call PanelApplicationsReviewStatus()
    /// </summary>
    public class ReviewStatusResult
    {
        #region Construction & set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reviewStatusId">ReviewStatus entity identifier</param>
        /// <param name="reviewStatus">ReviewStatus</param>
        internal ReviewStatusResult(int reviewStatusId, string reviewStatus)
        {
            this.ReviewStatusId = reviewStatusId;
            this.ReviewStatus = reviewStatus;
        }
        #endregion

        /// <summary>
        /// ReviewStatus entity identifier
        /// </summary>
        public int? ReviewStatusId { get; private set; }
        /// <summary>
        /// ReviewStatus
        /// </summary>
        public string ReviewStatus { get; private set; }
    }
}
