namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public interface IReviewerScoresFacts
    {
        int PrgPartId { get; }
        string ApplicationId { get; }
        string ShortDescription { get; }
        string EvaluationCriteriaDescription { get; }
        decimal Score { get; }
        int CriteriaSortOrder { get; }
        bool OverallEval { get; }
        bool HasValue { get; }
        int UserId { get; }
        string AdjLabel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [integer flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [integer flag]; otherwise, <c>false</c>.
        /// </value>
        bool IntegerFlag { get; set; }
        /// <summary>
        /// Whether the reviewer has abstained from evaluating the application
        /// </summary>
        bool AbstainFlag { get; set; }
        /// <summary>
        /// Client's Adjectival scoring identifier
        /// </summary>
        int? ClientScoringId { get; set; }
    }
}
