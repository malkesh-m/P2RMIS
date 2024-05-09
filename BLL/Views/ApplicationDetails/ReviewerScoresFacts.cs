using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ReviewerScoresFacts : IReviewerScoresFacts
    {
        #region Constants
        private class Constants
        {
            /// <summary>
            /// Default value for program part id
            /// </summary>
            public const int DefaultProgramPartId = 0;
            public const decimal DefaultScore = 0;
            public const int DefaultSortOrder = 0;
            public const bool DefaultOverallEval = false;
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor creates & populates the BL view of a Reviewer Scores.
        /// </summary>
        /// <param name="scores">-----</param>
        internal ReviewerScoresFacts(IReviewerScores scores)
        {
            this.PrgPartId = scores.PrgPartId.GetValueOrDefault(Constants.DefaultProgramPartId);
            this.ApplicationId = scores.ApplicationId;
            this.ShortDescription = scores.ShortDescription;
            this.EvaluationCriteriaDescription = scores.EvaluationCriteriaDescription;
            this.Score = scores.Score.GetValueOrDefault(Constants.DefaultScore);
            this.HasValue = scores.Score.HasValue | scores.AbstainFlag;
            this.CriteriaSortOrder = scores.CriteriaSortOrder.GetValueOrDefault(Constants.DefaultSortOrder);
            this.OverallEval = scores.OverallEval.GetValueOrDefault(Constants.DefaultOverallEval);
            this.UserId = scores.UserId;
            this.AbstainFlag = scores.AbstainFlag;
            this.AdjLabel = scores.AdjLabel;
            this.IntegerFlag = scores.IntegerFlag;
            this.ClientScoringId = scores.ClientScoringId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int PrgPartId { get; set; }
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
        public decimal Score { get; set; }
        /// <summary>
        /// Criteria sort order
        /// </summary>
        public int CriteriaSortOrder { get; set; }
        /// <summary>
        /// Flag indicating if the evaluation criteria is an overall criteria.
        /// </summary>
        public bool OverallEval { get; set; }
        /// <summary>
        /// Indicates if the score is an actual value
        /// </summary>
        public bool HasValue { get; set; }
        /// <summary>
        /// Reviewer's user identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Some scoring does not use numeric scoring.  In which case they use a text value such
        /// as Good, Bad or Ugly..  This is whatever the value assigned.
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
        /// Client's Adjectival scoring identifier
        /// </summary>
        public int? ClientScoringId { get; set; }
        #endregion
    }
}
