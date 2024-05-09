using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationWorkflowStepElementContentHistory object.
    /// </summary>
    public partial class ApplicationWorkflowStepElementContentHistory : IDateFields
    {
        /// <summary>
        /// Constructor.  Constructs an entity ApplicationWorkflowStepElementContentHistory from
        /// an ApplicationWorkflowStepElementContent object.
        /// </summary>
        /// <param name="elementContent">ApplicationWorkflowStepElementContent object</param>
        /// <param name="userId">User identifier</param>
        /// <param name="worklogId">Worklog identifier</param>
        public void PopulateFromContent(ApplicationWorkflowStepElementContent elementContent, int userId, int worklogId)
        {
            this.ApplicationWorkflowStepElementId = elementContent.ApplicationWorkflowStepElementId;
            this.ContentText = elementContent.ContentText;
            this.Score = elementContent.Score;
            this.Abstain = elementContent.Abstain;
            this.ApplicationWorkflowStepWorkLog1 = worklogId;

            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);
        }
    }
}
