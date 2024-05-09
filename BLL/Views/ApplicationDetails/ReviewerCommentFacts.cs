using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    /// <summary>
    /// Business layer representation of a reviewer's comments
    /// </summary>
    public class ReviewerCommentFacts : IReviewerCommentFacts
    {
        #region Constants
        private class Constants
        {
            /// <summary>
            /// Default value for program part id
            /// </summary>
            public const int DefaultProgramPartId = 0;
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor creates & populates the business layer representation of a Reviewer comments.
        /// <remarks>
        /// There are other data elements returned from the data layer but did not appear to be required
        /// in the business layer or presentation layer.
        /// </remarks>
        /// </summary>
        /// <param name="item">Data layer's Reviewer comment object</param>
        internal ReviewerCommentFacts(IReviewerComments item)
        {
            this.ApplicationId  = item.ApplicationIDd;
            this.ProgramPartId = item.PrgPartId.GetValueOrDefault(Constants.DefaultProgramPartId);
            this.Comment = ViewHelpers.SetNonNull(item.Comment);
            this.ReviewerName  = ViewHelpers.ConstructNameWithSpace(ViewHelpers.SetNonNull(item.FirstName),  ViewHelpers.SetNonNull(item.LastName));
            this.ReviewerId = item.ReviewerId;
      }
        #endregion
        #region Properties
        /// <summary>
        /// Application identifier.
        /// </summary>
        public string ApplicationId { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int ProgramPartId { get; private set; }
        /// <summary>
        /// Reviewer's comment
        /// </summary>
        public string Comment { get; private set; }
        /// <summary>
        /// Reviewer's name 
        /// </summary>
        public string ReviewerName { get; private set; }
        /// <summary>
        /// Reviewer's identifier
        /// </summary>
        public int ReviewerId { get; private set; }
        #endregion
    }
}
