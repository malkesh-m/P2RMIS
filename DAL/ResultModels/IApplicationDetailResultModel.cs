using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// TODO: document me
    /// </summary>
    public interface IApplicationDetailResultModel
    {
        IApplicationDetail ApplicationDetails { get; }
        IEnumerable<ReviewerInfo_Result> ReviewerDetails { get; }
        IEnumerable<IReviewerScores> ReviewerScoreDetails { get; }
        IEnumerable<ReviewerComments> ReviewerComments { get; }
        IEnumerable<UserApplicationComments> UserApplicationComments { get; }
        IEnumerable<CommentTypes> CommentLookupTypes { get;}
    }
}
