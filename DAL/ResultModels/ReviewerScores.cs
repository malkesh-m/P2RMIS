namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Data layer representation of a reviewer's scores.
    /// </summary>
    public class ReviewerScores : IReviewerScores
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        internal ReviewerScores() { }
        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int? PrgPartId { get; set; }
        /// <summary>
        /// Application identification
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// TODO: document me
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string EvaluationCriteriaDescription { get; set; }
        /// <summary>
        /// Reviewer's score
        /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// Criteria sort order
        /// </summary>
        public int? CriteriaSortOrder { get; set; }
        /// <summary>
        /// Flag indicating if the evaluation criteria is an overall criteria.
        /// </summary>
        public bool? OverallEval { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string AdjLabel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [integer flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [integer flag]; otherwise, <c>false</c>.
        /// </value>
        public bool IntegerFlag { get; set; }
        /// <summary>
        /// Whether the reviewer has abstained from evaluating the application
        /// </summary>
        public bool AbstainFlag { get; set; }

        /// <summary>
        /// Reviewer's user identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ClientScoring entity identifier
        /// </summary>
        public int? ClientScoringId { get; set; }
        #endregion
    }
}
