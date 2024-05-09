namespace Sra.P2rmis.WebModels.HelperClasses
{
    public class ReviewerModel : IReviewerModel
    {
        /// <summary>
        /// reviewer unique identifier
        /// </summary>
        public int ReviewerId { get; set; }
        /// <summary>
        /// reviewers name
        /// </summary>
        public string ReviewerName { get; set; }
    }
}
