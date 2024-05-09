
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Return results for SaveReviewersCritiquePostAssignment
    /// </summary>
    public interface ISaveReviewersCritiquePostAssignmentResultsModel
    {
        /// <summary>
        /// ApplicationWorkflowStepElement entity identifier.
        /// </summary>
        int ApplicationWorkflowStepElementId { get; }
        /// <summary>
        /// ApplicationWorkflowSetpElementContent entity identifier
        /// </summary>
        int ApplicationWorkflowStepElementContentId { get; }
        /// <summary>
        /// Indicates if the entity was an abstain
        /// </summary>
        bool Abstain { get; }
        /// <summary>
        /// Error when saving critique
        /// </summary>
        string ErrorMessage { get; }
    }
    /// <summary>
    /// Return results for SaveReviewersCritiquePostAssignment
    /// </summary>
    public class SaveReviewersCritiquePostAssignmentResultsModel : ISaveReviewersCritiquePostAssignmentResultsModel
    {
        #region Construction & Set up
        /// <summary>
        /// Model constructor
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElement element identifier</param>
        /// <param name="abstain">Indicates if the content was an abstain</param>
        public SaveReviewersCritiquePostAssignmentResultsModel(int applicationWorkflowStepElementId, int applicationWorkflowStepElementContentId, bool abstain, string errorMessage)
        {
            this.ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
            this.ApplicationWorkflowStepElementContentId = applicationWorkflowStepElementContentId;
            this.Abstain = abstain;
            this.ErrorMessage = errorMessage;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ApplicationWorkflowStepElement entity identifier.
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; private set; }
        /// <summary>
        /// ApplicationWorkflowSetpElementContent entity identifier
        /// </summary>
        public int ApplicationWorkflowStepElementContentId { get; private set; }
        /// <summary>
        /// Indicates if the entity was an abstain
        /// </summary>
        public bool Abstain { get; private set; }
        /// <summary>
        /// Error message when saving critique
        /// </summary>
        public string ErrorMessage { get; private set; }
        #endregion
    }
}
