using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationStageStepDiscussionComments object. 
    /// </summary>
    public partial class ApplicationStageStepDiscussionComment : IStandardDateFields
    {
        /// <summary>
        /// Populates the entity.
        /// </summary>
        /// <param name="applicationStageStepDiscussionEntityId">ApplicationStageStepDiscussion entity identifier.  The container for the discussion.</param>
        /// <param name="comment">The MOD comment</param>
        public void Populate(int applicationStageStepDiscussionEntityId, string comment)
        {
            this.ApplicationStageStepDiscussionId = applicationStageStepDiscussionEntityId;
            this.Comment = comment;
        }

        /// <summary>
        /// the entity containing a comment's authors information.
        /// </summary>
        /// <returns>UserInfo entity object</returns>
        public UserInfo AuthorInfo()
        {
            return this.ModifiedByUser.UserInfoEntity();
        }

        /// <summary>
        /// Gets the role name for the comment's author.
        /// </summary>
        /// <returns>Role name</returns>
        public string AuthorRole()
        {
            return this.ModifiedByUser.GetUserSystemRoleName();
        }

        /// <summary>
        /// the author's panel assignment.
        /// </summary>
        /// <returns>PanelUserAssignment entity</returns>
        public PanelUserAssignment AuthorPanelAssignment()
        {
            return
                this.ApplicationStageStepDiscussion.ApplicationStageStep.ApplicationStage.PanelApplication.SessionPanel
                    .PanelUserAssignment(this.CreatedBy ?? 0);
        }

        /// <summary>
        /// The author's application assignment.
        /// </summary>
        /// <returns>PanelApplicationReviewAssignment entity</returns>
        public PanelApplicationReviewerAssignment AuthorApplicationAssignment()
        {
            return
                this.ApplicationStageStepDiscussion.ApplicationStageStep.ApplicationStage.PanelApplication
                    .PanelApplicationReviewerAssignment(this.CreatedBy ?? 0);
        }

        /// <summary>
        /// Determines whether the author is a moderator.
        /// </summary>
        /// <returns>true if author is a moderator; otherwise false</returns>
        public bool IsAuthorModerator()
        {
            return this.AuthorPanelAssignment()?.ClientParticipantType.IsSro() ?? false;
        }

        /// <summary>
        /// Gets the recipient list for a discussion comment notification.
        /// </summary>
        /// <returns>UserInfo collection of recipients</returns>
        public IEnumerable<UserInfo> GetRecipientList()
        {
            int panelApplicationEntityId = this.ApplicationStageStepDiscussion.ApplicationStageStep.ApplicationStage.PanelApplicationId;
            //The recipient list current include all assigned reviewers and SRO/moderator minus the author of the comment
            var result =
                this.ApplicationStageStepDiscussion.ApplicationStageStep.ApplicationStage.PanelApplication
                    .PanelApplicationReviewerAssignments.Where(
                        x => AssignmentType.CritiqueAssignments.Contains(x.ClientAssignmentType.AssignmentTypeId))
                    .Select(x => x.PanelUserAssignment.User.UserInfoEntity()).Concat(
                    this.ApplicationStageStepDiscussion.ApplicationStageStep.ApplicationStage.PanelApplication.SessionPanel.PanelUserAssignments
                        .Where(x => x.ClientParticipantType.IsSro() || x.IsChair(panelApplicationEntityId))
                        .Select(x => x.User.UserInfoEntity()));
            return result.Where(x => x.UserID != this.ModifiedByUser.UserID);
        }
        /// <summary>
        /// Returns the Application entity for this ApplicationStageStepDiscussionComment entity
        /// </summary>
        /// <returns>ApplicationStageStepDiscussionComment entity</returns>
        public Application Application()
        {
            return
                this.ApplicationStageStepDiscussion.ApplicationStageStep.ApplicationStage.PanelApplication.Application;
        }
    }
}
