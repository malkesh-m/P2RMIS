using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// The object containing the premeeting scores for the application's reviewers
    /// </summary>
    public class ReviewerApplicationPremeetingScoresModel : IReviewerApplicationPremeetingScoresModel
    {
        // constructor
        public ReviewerApplicationPremeetingScoresModel()
        {
            // ensure non-empty list
            List<IPreMeetingCriteriaModel> Criteria = new List<IPreMeetingCriteriaModel>();

            List<IPreMeetingReviewerModel> Reviewers = new List<IPreMeetingReviewerModel>();
        }

        /// <summary>
        /// The list of premeeting criteria and their scores
        /// </summary>
        public List<IPreMeetingCriteriaModel> Criteria { get; set; }
        /// <summary>
        /// The list of the application's reviewers
        /// </summary>
        public List<IPreMeetingReviewerModel> Reviewers { get; set; }
    }
}
