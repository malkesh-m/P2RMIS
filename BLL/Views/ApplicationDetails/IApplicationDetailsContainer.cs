using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public interface IApplicationDetailsContainer
    {
        IApplicationDetailFact ApplicationDetails { get; }
        IEnumerable<IReviewerFacts> ReviewerDetails { get; }
        IEnumerable<IReviewerScoresFacts> ReviewerScores { get; }
        IEnumerable<IReviewerCommentFacts> ReviewerComments { get; }
        IEnumerable<IUserApplicationCommentFacts> UserApplicationComments { get; }
        IEnumerable<ICommentLookupTypes> CommentLookupTypes { get; }
        IEnumerable<IReviewerLineScore> Details { get; }
        IEnumerable<KeyValuePair<int, string>> Columns { get; }
        /// <summary>
        /// Contains the Alt text values for the column titles
        /// </summary>
        IDictionary<int, string> ColumnAltText { get; set; }
        IReviewerLineScore Averages { get; set; }
        IReviewerLineScore StandardDeviation { get; set; }
    }
}
