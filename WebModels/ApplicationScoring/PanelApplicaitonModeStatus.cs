
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model representing the MOD state of a critique
    /// </summary>
    public interface IPanelApplicaitonModeStatus
    {
        /// <summary>
        /// Indicates if the phase is an MOD (moderated on-line discussion)
        /// </summary>
        bool IsModPhase { get; }
        /// <summary>
        /// Indicates if the MOD can be started.
        /// </summary>
        bool IsModReady { get; }
        /// <summary>
        /// Indicates if the MOD is active
        /// </summary>
        bool IsModActive { get; }
        /// <summary>
        /// Indicates if the MOD is closed
        /// </summary>
        bool IsModClosed { get; }
        /// <summary>
        /// ApplicationStageStep entity identifier
        /// </summary>
        int ApplicationStageStepId { get; }
        /// <summary>
        /// ApplicationStageStepDiscussion identifier.  Container which holds the conversation.
        /// </summary>
        int? ApplicationStageStepDiscussionId { get; }
        /// <summary>
        /// Indicates if a MOD exists
        /// </summary>
        bool IsThereDiscussion { get; }
    }
    /// <summary>
    /// Model representing the MOD state of a critique
    /// </summary>
    public class PanelApplicaitonModeStatus: IPanelApplicaitonModeStatus
    {
        #region Construction & Setup
        /// <summary>
        /// Populate the model
        /// </summary>
        /// <param name="isModPhase">Indicates if the phase is an MOD (moderated on-line discussion)</param>
        /// <param name="isModReady">Indicates if the MOD can be started.</param>
        /// <param name="isModActive">Indicates if the MOD is active</param>
        /// <param name="isModClosed">Indicates if the MOD is closed</param>
        /// <param name="isThereDiscussion">Indicates if there is one or more discussion comments</param>
        public PanelApplicaitonModeStatus(bool isModPhase, bool isModReady, bool isModActive, bool isModClosed, int applicationStageStepId, int? applicationStageStepDiscussionId, bool isThereDiscussion)
        {
            this.IsModPhase = isModPhase;
            this.IsModReady = isModReady;
            this.IsModActive = isModActive;
            this.IsModClosed = isModClosed;
            this.IsThereDiscussion = isThereDiscussion;
            //
            // Now the id values
            //
            this.ApplicationStageStepId = applicationStageStepId;
            this.ApplicationStageStepDiscussionId = applicationStageStepDiscussionId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Indicates if the phase is an MOD (moderated on-line discussion)
        /// </summary>
        public bool IsModPhase { get; private set; }
        /// <summary>
        /// Indicates if the MOD can be started.
        /// </summary>
        public bool IsModReady { get; private set; }
        /// <summary>
        /// Indicates if the MOD is active
        /// </summary>
        public bool IsModActive { get; private set; }
        /// <summary>
        /// Indicates if the MOD is closed
        /// </summary>
        public bool IsModClosed { get; private set; }
        /// <summary>
        /// ApplicationStageStep entity identifier
        /// </summary>
        public int ApplicationStageStepId { get; private set; }
        /// <summary>
        /// ApplicationStageStepDiscussion identifier.  Container which holds the conversation.
        /// </summary>
        public int? ApplicationStageStepDiscussionId { get; private set; }
        /// <summary>
        /// Indicates if a MOD exists
        /// </summary>
        public bool IsThereDiscussion { get; private set; }
        #endregion
    }
}
