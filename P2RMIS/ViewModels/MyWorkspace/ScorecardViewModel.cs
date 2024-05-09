using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Attributes;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;
using System.Linq;
using Sra.P2rmis.Bll.ApplicationScoring;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the Score Card page
    /// </summary>
    [Validator(typeof(ScoreCardValidator))]
    public class ScorecardViewModel
    {
        /// <summary>
        /// Indicates there is not score type.  I.E. the
        /// criteria is not to be scored.
        /// </summary>
        public int NoScoreType { get { return 0; } }
        /// <summary>
        /// Indicates the criteria is numeric (Integer or Decimal).  For our purposes
        /// in the javascript there is no difference
        /// </summary>
        public int NumericScoreType { get { return 1; } }
        /// <summary>
        /// Indicates if the criteria is adjectival
        /// </summary>
        public int AdjectivalScoreType { get { return 2; } }

        /// <summary>
        /// Application information
        /// </summary>
        public IApplicationInformationModel ApplicationInformation { get; set; }
        /// <summary>
        /// Collection of a reviewer's scores
        /// </summary>
        public List<ReviewerScores> ReviewerScores { get; set; }
        /// <summary>
        /// Collection of a reviewer's criteria scores
        /// </summary>
        public List<ReviewerScores> CriterionScores { get; set; }
        /// <summary>
        /// Collection of a reviewer's overall scores
        /// </summary>
        public List<ReviewerScores> OverallScores { get; set; }
        /// <summary>
        /// Whether the user is able to enter comments for the specified application
        /// </summary>
        public bool CanUserEnterComments { get; set; }
        /// <summary>
        /// Whether the user is able to adjust scores for the specified application and reviewer
        /// </summary>
        public bool CanUserEditScores { get; set; }
        #region Validation
        /// <summary>

        /// User's first name and last name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>

        /// Validator for ScorecardViewModel
        /// </summary>
        public class ScoreCardValidator : AbstractValidator<ScorecardViewModel>
        {
            /// <summary>
            /// Validation rules.
            /// </summary>
            public ScoreCardValidator()
            {

                RuleFor(x => x.CriterionScores).SetCollectionValidator(new ScoreValidator());
                RuleFor(x => x.OverallScores).SetCollectionValidator(new ScoreValidator());
            }
        }
        /// <summary>
        /// Validator to validate an individual score
        /// </summary>
        public class ScoreValidator : AbstractValidator<ReviewerScores>
        {
            #region Properties
            /// <summary>
            /// Value for Abstain
            /// </summary>
            /// <remarks>
            /// This should be common somewhere
            /// </remarks>
            public string Abstain { get { return "A"; } }
            #endregion}
            /// <summary>
            /// Constructor.  Validates a a single ReviewerScore entry
            /// </summary>
            public ScoreValidator()
            {
                RuleFor(x => x.Score).NotEmpty().Must(IsValidScore).WithMessage(MessageService.InvalidScore(), x => x.ScoreLowValue, x => x.ScoreHighValue);
            }
            /// <summary>
            /// Determines if the score is valid:
            ///  - It is if it is an A
            ///  - Is within the low & high range values
            /// </summary>
            /// <param name="model">ReviewScore model for a single score</param>
            /// <param name="value">Value to test</param>
            /// <returns>True if the value is valid;  False otherwise</returns>
            public bool IsValidScore(ReviewerScores model, string value)
            {
                bool result = false;
                decimal decimalValue;
                //
                // Are we an abstain?
                //
                if (Abstain == value)
                {
                    result = true;
                }
                else if (decimal.TryParse(value, out decimalValue))
                {
                    // 
                    // For some reason there is no ordering of the score values.  Which 
                    // means low = 1 & high = 5;  Or low = 5 & high = 1.
                    //
                    result = (model.ScoreLowValue < model.ScoreHighValue) ?
                        result = ((model.ScoreLowValue <= decimalValue) & (decimalValue <= model.ScoreHighValue)) :
                        result = ((model.ScoreHighValue <= decimalValue) & (decimalValue <= model.ScoreLowValue));
                }
                return result;
            }

            public string ConstructMessage(ReviewerScores model, string value) { return ""; }
        }
        #endregion
        /// <summary>
        /// the panel application id
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// the session panel id
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// the user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// the last page URL
        /// </summary>
        public string LastPageUrl { get; set; }
        /// <summary>
        /// Identifies the type of the overall score.
        /// </summary>
        public int OverallScoreType { get; private set; }
        /// <summary>
        /// Identifies the type of the criteria score type.
        /// </summary>
        public int CriteriaScoreType { get; private set; }
        /// <summary>
        /// Determines the scoring types (decimal, integer, or adjectival) for use in the view scripts.
        /// </summary>
        internal void DetermineScoringTypes()
        {
            OverallScoreType = DetermineOverallScoreType();
            CriteriaScoreType = DetermineCriteriaScoreType();
        }
        /// <summary>
        /// Determines the Overall Score type if one exists.
        /// </summary>
        /// <returns>Overall score type</returns>
        private int DetermineOverallScoreType()
        {
            string scoreType = this.OverallScores.DefaultIfEmpty(new ReviewerScores()).FirstOrDefault().ScoreType;
            return DetermineScoreType(scoreType);
        }
        /// <summary>
        /// Determines the Criteria Score type if one exists.
        /// </summary>
        /// <returns></returns>
        private int DetermineCriteriaScoreType()
        {
            string scoreType = this.CriterionScores.DefaultIfEmpty(new ReviewerScores()).FirstOrDefault().ScoreType;
            return DetermineScoreType(scoreType);
        }
        /// <summary>
        /// Does all the heavy lifting to determine the score type.
        /// </summary>
        /// <param name="scoreType">Score type </param>
        /// <returns>Numeric indicator of score type</returns>
        private int DetermineScoreType(string scoreType)
        {
            int result = this.NoScoreType;
            if (!string.IsNullOrWhiteSpace(scoreType))
            {
                if (ApplicationScoringService.IsSameScoringScale(scoreType, "Adjectival"))
                {
                    result = this.AdjectivalScoreType;
                }
                else if (ApplicationScoringService.IsSameScoringScale(scoreType, "Decimal"))
                {
                    result = this.NumericScoreType;
                }
                else if (ApplicationScoringService.IsSameScoringScale(scoreType, "Integer"))
                {
                    result = this.NumericScoreType;
                }
            }
            return result;
        }
        /// <summary>
        /// Gets or sets the profile type identifier.
        /// </summary>
        /// <value>
        /// The profile type identifier.
        /// </value>
        public int? ProfileTypeId { get; set; }
    }
}