using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Container holding the results of retrieving an Application's detail.
    /// </summary>
    public class ApplicationDetailResultModel : IApplicationDetailResultModel
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        internal ApplicationDetailResultModel() { }
        #endregion
        #region Properties
        /// <summary>
        /// Container holding the data layer representation results for the Application details query section.
        /// </summary>
        public IApplicationDetail ApplicationDetails { get; internal set; }
        /// <summary>
        /// Container holding the data layer representation results for the Reviewer details query section.
        /// </summary>
        public IEnumerable<ReviewerInfo_Result> ReviewerDetails { get; internal set; }
        /// <summary>
        /// Container holding the data layer representation results for the Reviewer scores details query section.
        /// </summary>
        public IEnumerable<IReviewerScores> ReviewerScoreDetails { get; internal set; }
        /// <summary>
        /// Container holding the data layer representation results for the reviewer comments.
        /// </summary>
        public IEnumerable<ReviewerComments> ReviewerComments { get; internal set; }
        /// <summary>
        /// Container holding the data layer representation results for user application comments.
        /// </summary>
        public IEnumerable<UserApplicationComments> UserApplicationComments { get; internal set; }
        /// <summary>
        /// Container holding the data layer representation of comment types.
        /// </summary>
        public IEnumerable<CommentTypes> CommentLookupTypes { get; internal set; }
        #endregion
    }
}
