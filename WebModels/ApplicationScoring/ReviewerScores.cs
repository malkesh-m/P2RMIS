using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    public interface IReviewerScores: IEditable
    {
        /// <summary>
        /// Display name of a criterion
        /// </summary>
        string ElementName { get; set; }
        /// <summary>
        /// Identifier for a criterion instance of an application workflow step
        /// </summary>
        int ApplicationWorkflowStepElementId { get; set; }
        /// <summary>
        /// The type of scoring expected by a criterion
        /// </summary>
        string ScoreType { get; set; }
        /// <summary>
        /// Whether the criterion has been abstained by the reviewer
        /// </summary>
        bool AbstainFlag { get; set; }
        /// <summary>
        /// The score value provided by a reviewer for the criterion
        /// </summary>
        string Score { get; set; }
        /// <summary>
        /// The highest value accepted as a score for the criterion
        /// </summary>
        decimal ScoreHighValue { get; set; }
        /// <summary>
        /// The lowest value accepted as a score for the criterion
        /// </summary>
        decimal ScoreLowValue { get; set; }
        /// <summary>
        /// Whether the criterion is considered to be an overall indicator of merit for the application
        /// </summary>
        bool OverallFlag { get; set; }
        /// <summary>
        /// Display name for the scoring legend
        /// </summary>
        string ScoringLegendName { get; set; }
        /// <summary>
        /// Collection of items used to populate a scoring legend
        /// </summary>
        IEnumerable<ScoringLegendItem> ScoringLegendItems { get; set; }
        /// <summary>
        /// Collection of possible adjectival scores for the criterion
        /// </summary>
        IEnumerable<AdjectivalScoreValue> AdjectivalScoreValues { get; set; }
        /// <summary>
        /// The order in which criteria should be ordered
        /// </summary>
        int SortOrder { get; set; }
    }

    public class ReviewerScores : Editable, IReviewerScores
    {
        /// <summary>
        /// Populates the ReviewerScores object
        /// </summary>
        /// <param name="scoringLegendItems"></param>
        /// <param name="scoringLegendName"></param>
        /// <param name="overallFlag"></param>
        /// <param name="scoreLowValue"></param>
        /// <param name="scoreHighValue"></param>
        /// <param name="score"></param>
        /// <param name="abstainFlag"></param>
        /// <param name="scoreType"></param>
        /// <param name="applicationWorkflowStepElementId"></param>
        /// <param name="elementName"></param>
        /// <param name="adjectivalScoreValues"></param>
        /// <param name="sortOrder"></param>
        public void Populate(IEnumerable<ScoringLegendItem> scoringLegendItems, string scoringLegendName, bool overallFlag, decimal scoreLowValue, decimal scoreHighValue, decimal? score, bool abstainFlag, string scoreType, int applicationWorkflowStepElementId, string elementName, IEnumerable<AdjectivalScoreValue> adjectivalScoreValues, int sortOrder)
        {
            ScoringLegendItems = scoringLegendItems;
            ScoringLegendName = scoringLegendName;
            OverallFlag = overallFlag;
            ScoreLowValue = scoreLowValue;
            ScoreHighValue = scoreHighValue;
            Score = String.Equals(scoreType, "Integer", StringComparison.OrdinalIgnoreCase) && score != null ? Math.Round((double)score).ToString() : score.ToString();
            AbstainFlag = abstainFlag;
            ScoreType = scoreType;
            ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
            ElementName = elementName;
            AdjectivalScoreValues = adjectivalScoreValues;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Display name of a criterion
        /// </summary>
        public string ElementName { get; set; }
        /// <summary>
        /// Identifier for a criterion instance of an application workflow step
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; set; }
        /// <summary>
        /// The type of scoring expected by a criterion
        /// </summary>
        public string ScoreType { get; set; }
        /// <summary>
        /// Whether the criterion has been abstained by the reviewer
        /// </summary>
        public bool AbstainFlag { get; set; }
        /// <summary>
        /// The score value provided by a reviewer for the criterion
        /// </summary>
        public string Score { get; set; }
        /// <summary>
        /// The highest value accepted as a score for the criterion
        /// </summary>
        public decimal ScoreHighValue { get; set; }
        /// <summary>
        /// The lowest value accepted as a score for the criterion
        /// </summary>
        public decimal ScoreLowValue { get; set; }
        /// <summary>
        /// Whether the criterion is considered to be an overall indicator of merit for the application
        /// </summary>
        public bool OverallFlag { get; set; }
        /// <summary>
        /// Display name for the scoring legend
        /// </summary>
        public string ScoringLegendName { get; set; }
        /// <summary>
        /// Collection of items used to populate a scoring legend
        /// </summary>
        public IEnumerable<ScoringLegendItem> ScoringLegendItems { get; set; }
        /// <summary>
        /// Collection of possible adjectival scores for the criterion
        /// </summary>
        public IEnumerable<AdjectivalScoreValue> AdjectivalScoreValues { get; set; } 
        #region IEditable implementation
        /// <summary>
        /// Does the model have data?
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public override bool HasData()
        {
            return true; 
        }
        #endregion
        /// <summary>
        /// The order in which criteria should be ordered
        /// </summary>
        public int SortOrder { get; set; }
    }
    /// <summary>
    /// Represents a scoring legend to provide the user additional guidance on their score selection
    /// </summary>
    public class ScoringLegendItem
    {
        /// <summary>
        /// Label for an item within the scoring legend
        /// </summary>
        public string ItemLabel { get; set; }
        /// <summary>
        /// High value for a scoring legend
        /// </summary>
        public string HighValue { get; set; }
        /// <summary>
        /// Low value for a scoring legend
        /// </summary>
        public string LowValue { get; set; }
        /// <summary>
        /// Order in which the scoring legend should be sorted
        /// </summary>
        public int SortOrder { get; set; }
    }
    /// <summary>
    /// Represents a possible score choice for the user if criterion is adjectivally scored
    /// </summary>
    public class AdjectivalScoreValue
    {
        /// <summary>
        /// The client scoring scal adjectival Identifer for this score
        /// </summary>
        public int ClientScoringScaleAdjectivalId { get; set; }
        /// <summary>
        /// Numeric score equivalent of an adjectival value
        /// </summary>
        public string NumericValue { get; set; }
        /// <summary>
        /// Label for an adjectival score value
        /// </summary>
        public string AdjectivalLabel { get; set; }
        /// <summary>
        /// Order in which adjectival choices should be displayed
        /// </summary>
        public int SortOrder { get; set; }
    }
}
