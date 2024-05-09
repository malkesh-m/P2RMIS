using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// The object containing the premeeting scores for the application's reviewers
    /// </summary>
    public interface IReviewerApplicationPremeetingScoresModel
    {
        /// <summary>
        /// The list of premeeting criteria and their scores
        /// </summary>
        List<IPreMeetingCriteriaModel> Criteria { get; set; }
        /// <summary>
        /// The list of the application's reviewers
        /// </summary>

        List<IPreMeetingReviewerModel> Reviewers { get; set; }

    }
}

