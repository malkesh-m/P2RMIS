
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    #region CritiqueScore
    /// <summary>
    /// Represents a phase score for a critique.
    /// </summary>
    public interface ICritiqueScore
    {
        /// <summary>
        /// Phase name
        /// </summary>
        string PhaseName { get; }
        /// <summary>
        /// Phase score
        /// </summary>
        string Score { get; }
        /// <summary>
        /// Phase order
        /// </summary>
        int Order { get; }
        /// <summary>
        /// Indicates if the user abstained
        /// </summary>
        bool Abstain { get; }
    }
    /// <summary>
    /// Represents a phase score for a critique.
    /// </summary>
    public class CritiqueScore : ICritiqueScore
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationWorkflowStepId">Application workflow step identifier</param>
        /// <param name="phaseName">Phase name</param>
        /// <param name="score">Reviewer's score</param>
        /// <param name="order">Phase order</param>
        /// <param name="abstain">Indicates if the user abstained</param>
        /// <param name="isSubmitted">if set to <c>true</c> [is submitted].</param>
        public CritiqueScore(int applicationWorkflowStepId, string phaseName, string score, int order, bool abstain, bool isSubmitted, int stageOrder)
        {
            this.ApplicationWorkflowStepId = applicationWorkflowStepId;
            this.PhaseName = phaseName;
            this.Score = score;
            this.Order = order;
            this.Abstain = abstain;
            this.IsSubmitted = isSubmitted;
            this.StageOrder = stageOrder;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Phase name
        /// </summary>
        public string PhaseName { get; private set; }
        /// <summary>
        /// Gets the application workflow step identifier.
        /// </summary>
        /// <value>
        /// The application workflow step identifier.
        /// </value>
        public int ApplicationWorkflowStepId { get; private set; }
        /// <summary>
        /// Phase score
        /// </summary>
        public string Score { get; private set; }
        /// <summary>
        /// Phase order
        /// </summary>
        public int Order { get; private set; }
        /// <summary>
        /// Indicates if the user abstained
        /// </summary>
        public bool Abstain { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is submitted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is submitted; otherwise, <c>false</c>.
        /// </value>
        public bool IsSubmitted { get; private set; }
        /// <summary>
        /// Gets or sets the ApplicationStage StageOrder of the value.
        /// </summary>
        public int StageOrder { get; private set; }

        #endregion
    }
    #endregion

}
