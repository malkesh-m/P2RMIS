namespace Sra.P2rmis.Dal.ResultModels
{
    public interface IReviewerScores
    {
        int? PrgPartId { get; set; }
        string ApplicationId { get; set; }
        string ShortDescription { get; set; }
        string EvaluationCriteriaDescription { get; set; }
        decimal? Score { get; set; }
        int? CriteriaSortOrder { get; set; }
        bool? OverallEval { get; set; }
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
        int UserId { get; set; }
        /// <summary>
        /// ClientScoring entity identifier
        /// </summary>
        int? ClientScoringId { get; set; }
    }
}
