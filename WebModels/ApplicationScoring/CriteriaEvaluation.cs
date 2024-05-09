using System;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// The Criterion Scores
    /// </summary>
    public class CriteriaEvaluation : ICriteriaEvaluation
    {
        /// <summary>
        /// The scores
        /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// The panel user assignment identifier
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Did the reviewer abstain
        /// </summary>
        public bool Abstain { get; set; }
        /// <summary>
        /// Gets or sets the type of the score.
        /// </summary>
        /// <value>
        /// The type of the score.
        /// </value>
        public bool IntegerFlag { get; set; }
        /// <summary>
        /// Gets the score value.
        /// </summary>
        /// <value>
        /// The score value.
        /// </value>
        public string ScoreValue {
            get
            {
                if (Abstain)
                {
                    return "A";
                }
                else if (IntegerFlag)
                {
                    return Score != null ? Math.Round((double)Score).ToString() : String.Empty;
                }
                else
                {
                    return Score != null ? Score.ToString() : String.Empty;
                }
            }
        }
    }
}
