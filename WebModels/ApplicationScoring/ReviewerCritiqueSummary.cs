using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model container for an application's critique summary for all reviewers
    /// </summary>
    public interface IReviewerCritiqueSummary
    {
        /// <summary>
        /// Gets or sets the reviewer list.
        /// </summary>
        /// <value>
        /// The reviewer list.
        /// </value>
        IEnumerable<IReviewerModel> ReviewerList { get; set; }

        /// <summary>
        /// Gets or sets the phase list.
        /// </summary>
        /// <value>
        /// The phase list.
        /// </value>
        IEnumerable<IPhaseModel> PhaseList { get; set; }

        /// <summary>
        /// Gets or sets the criteria list.
        /// </summary>
        /// <value>
        /// The criteria list.
        /// </value>
        IEnumerable<ICriteriaReviewerScoreModel> CriteriaList { get; set; }
    }

    /// <summary>
    /// Model container for an application's critique summary for all reviewers
    /// </summary>
    public class ReviewerCritiqueSummary : IReviewerCritiqueSummary
    {
        public ReviewerCritiqueSummary()
        {
            ReviewerList = new List<IReviewerModel>();
            PhaseList = new List<IPhaseModel>();
            CriteriaList = new List<ICriteriaReviewerScoreModel>();
        }

        /// <summary>
        /// Gets or sets the reviewer list.
        /// </summary>
        /// <value>
        /// The reviewer list.
        /// </value>
        public IEnumerable<IReviewerModel> ReviewerList { get; set; }
        /// <summary>
        /// Gets or sets the phase list.
        /// </summary>
        /// <value>
        /// The phase list.
        /// </value>
        public IEnumerable<IPhaseModel> PhaseList { get; set; }
        /// <summary>
        /// Gets or sets the criteria list.
        /// </summary>
        /// <value>
        /// The criteria list.
        /// </value>
        public IEnumerable<ICriteriaReviewerScoreModel> CriteriaList { get; set; }
    }
}
