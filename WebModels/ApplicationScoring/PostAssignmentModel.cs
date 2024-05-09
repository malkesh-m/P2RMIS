
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    #region Interface
    public interface IPostAssignmentModel : IPreAssignmentModel
    {
        /// <summary>
        /// Populates the overall score.
        /// </summary>
        /// <param name="overallScore">The overall score.</param>
        void PopulateOverallScore(string overallScore);
        /// <summary>
        /// Populates the post assignment status information.
        /// </summary>
        /// <param name="critiqueActionState">State of the critique action.</param>
        /// <param name="isCritiqueSubmitted">Whether the critique has been submitted.</param>
        /// <param name="isAssigned">Whether the critique is assigned to the reviewer.</param>
        /// <param name="critiqueStepId">The critique step identifier.</param>
        /// <param name="panelStepId">The panel step identifier.</param>
        /// <param name="critiquePhaseName">Name of the critique phase.</param>
        /// <param name="presentationOrder">Reviewer presentation order</param>
        /// <param name="isReopened">Indicates if the phase has been reopened</param>
        void PopulateStatusInfo(StateResult critiqueActionState, StateResult newCritiqueActionState, bool isCritiqueSubmitted, bool isAssigned, int critiqueStepId, int panelStepId, string critiquePhaseName, int? presentationOrder, bool isReopened);
        /// <summary>
        /// Gets the state ENUM of the critique action.
        /// </summary>
        /// <value>
        /// The state of the critique action.
        /// </value>
        StateResult CritiqueActionState { get; }
        StateResult NewCritiqueActionState { get; }

        /// <summary>
        /// Gets the overall score.
        /// </summary>
        /// <value>
        /// The overall score.
        /// </value>
        string OverallScore { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance of critique is submitted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is critique submitted; otherwise, <c>false</c>.
        /// </value>
        bool IsCritiqueSubmitted { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is assigned to the reviewer.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is assigned; otherwise, <c>false</c>.
        /// </value>
        bool IsAssigned { get; }
        /// <summary>
        /// Gets or sets the name of the critique phase.
        /// </summary>
        /// <value>
        /// The name of the critique phase.
        /// </value>
        string CritiquePhaseName { get; set; }
        /// <summary>
        /// Presentation order, if any
        /// </summary>
        int? PresentationOrder { get; set; }
        /// <summary>
        /// Indicates if the application is in Reopened
        /// </summary>
        bool IsReopened { get; }
        /// <summary>
        /// The on-line discussion status for the application.
        /// </summary>
        IPanelApplicaitonModeStatus OnLineDiscussionStatus { get; }
        /// <summary>
        /// Indicates if the reviewer is a chairperson
        /// </summary>
        bool IsChairPerson { get; }
    }
    #endregion

    public class PostAssignmentModel : PreAssignmentModel, IPostAssignmentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostAssignmentModel"/> class.
        /// </summary>
        /// <param name="blinded">whether the mechanism is blinded</param>
        /// <param name="isChairPerson">Indicates if the reviewer is a chairperson</param>
        public PostAssignmentModel(bool blinded, bool isChairPerson) : base(blinded)
        {
            this.IsChairPerson = isChairPerson;
        }

        /// <summary>
        /// Populates the overall score.
        /// </summary>
        /// <param name="overallScore">The overall score.</param>
        public void PopulateOverallScore(string overallScore)
        {
            OverallScore = overallScore;
        }

        /// <summary>
        /// Populates the post assignment status information.
        /// </summary>
        /// <param name="critiqueActionState">State of the critique action.</param>
        /// <param name="isCritiqueSubmitted">Whether the critique has been submitted.</param>
        /// <param name="isAssigned">Whether the critique is assigned to the reviewer.</param>
        /// <param name="critiqueStepId">The critique step identifier.</param>
        /// <param name="panelStepId">The panel step identifier.</param>
        /// <param name="critiquePhaseName">Name of the critique phase.</param>
        /// <param name="presentationOrder">Reviewer presentation order</param>
        /// <param name="isReopened">Indicates if the phase has been reopened</param>
        public void PopulateStatusInfo(StateResult critiqueActionState, StateResult newCritiqueActionState, bool isCritiqueSubmitted, bool isAssigned, int critiqueStepId, int panelStepId, string critiquePhaseName, int? presentationOrder, bool isReopened)
        {
            CritiqueStepId = critiqueStepId;
            PanelStepId = panelStepId;
            CritiquePhaseName = critiquePhaseName;
            CritiqueActionState = critiqueActionState;
            NewCritiqueActionState = newCritiqueActionState;
            IsCritiqueSubmitted = isCritiqueSubmitted;
            IsAssigned = isAssigned;
            PresentationOrder = presentationOrder;
            IsReopened = isReopened;
        } 
        /// <summary>
        /// Populates the post assignment model with on-line discussion status.
        /// </summary>
        /// <param name="a"></param>
        public void Populate(IPanelApplicaitonModeStatus onLineDiscussionStatus)
        {
            this.OnLineDiscussionStatus = onLineDiscussionStatus;
        }
        #region Properties
        /// <summary>
        /// Gets the state ENUM of the critique action.
        /// </summary>
        /// <value>
        /// The state of the critique action.
        /// </value>
        public StateResult CritiqueActionState { get; private set; }
        public StateResult NewCritiqueActionState { get; private set; }
        /// <summary>
        /// Gets the overall score.
        /// </summary>
        /// <value>
        /// The overall score.
        /// </value>
        public string OverallScore { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance of critique is submitted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is critique submitted; otherwise, <c>false</c>.
        /// </value>
        public bool IsCritiqueSubmitted { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is assigned to the reviewer.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is assigned; otherwise, <c>false</c>.
        /// </value>
        public bool IsAssigned { get; private set; }
        /// <summary>
        /// Gets or sets the name of the critique phase.
        /// </summary>
        /// <value>
        /// The name of the critique phase.
        /// </value>
        public string CritiquePhaseName { get; set; }
        /// <summary>
        /// Gets or sets the panel step identifier.
        /// </summary>
        /// <value>
        /// The panel step identifier.
        /// </value>
        public int PanelStepId { get; set; }
        /// <summary>
        /// Gets or sets the critique step identifier.
        /// </summary>
        /// <value>
        /// The critique step identifier.
        /// </value>
        public int CritiqueStepId { get; set; }
        /// <summary>
        /// Presentation order, if any
        /// </summary>
        public int? PresentationOrder { get; set; }
        /// <summary>
        /// Indicates if the application is in Reopened
        /// </summary>
        public bool IsReopened { get; private set; }
        /// <summary>
        /// The on-line discussion status for the application.
        /// </summary>
        public IPanelApplicaitonModeStatus OnLineDiscussionStatus { get; private set; }
        /// <summary>
        /// Indicates if the reviewer is a chairperson
        /// </summary>
        public bool IsChairPerson { get; private set; }
        #endregion
    }
}
