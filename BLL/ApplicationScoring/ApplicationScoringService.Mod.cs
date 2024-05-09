using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.Dal;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ApplicationScoringService
    {
        /// <summary>
        /// Determines the MOD states of PanelApplication.
        /// </summary>
        /// <param name="panelApplicationEntityId">PanelApplication entity identifier</param>
        /// <returns>IPanelApplicaitonModeStatus object containing MOD status of the PanelApplication</returns>
        public IPanelApplicaitonModeStatus PanelApplicationMODStatus(int panelApplicationEntityId)
        {
            ValidateInt(panelApplicationEntityId, FullName(nameof(ApplicationScoringService), nameof(PanelApplicationMODStatus)), nameof(panelApplicationEntityId));
            //
            // First things first, get the PanelApplication.  We have the identifier so....
            //
            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationEntityId);
            //
            //  Now that we have that we just call some exiting methods to determine the MOD values.
            //
            PanelStageStep panelStageStepEntity = panelApplicationEntity.LocateFinalPanelStageStep();
            bool modIsActive = IsModActive(panelApplicationEntity, panelStageStepEntity);
            bool panelStageStepIsOpen = IsStageStepOpen(panelStageStepEntity);
            ApplicationStageStep applicationStageStepEntity = panelStageStepEntity?.RetrieveApplicationStageStep(panelApplicationEntityId);
            //
            // And since we have all the information we just construct the model & Bob is your uncle.
            //
            return new PanelApplicaitonModeStatus(
                                PanelStageStep.IsModPhase(panelStageStepEntity),
                                PanelStageStep.IsModReady(panelStageStepEntity, panelStageStepIsOpen, modIsActive),
                                PanelStageStep.IsModActive(panelStageStepEntity, panelStageStepIsOpen, modIsActive),
                                PanelStageStep.IsModClosed(panelStageStepEntity, panelStageStepIsOpen),
                                DetermineApplicationStageStepId(applicationStageStepEntity),
                                applicationStageStepEntity?.RetrieveDiscussion()?.ApplicationStageStepDiscussionId,
                                modIsActive);
        }

        /// <summary>
        /// Sends the discussion notification.
        /// </summary>
        /// <param name="theMailService">Instance of the mail service.</param>
        /// <param name="applicationStageStepDiscussionCommentId">The application stage step discussion comment identifier.</param>
        /// <returns>MailStatus regarding success or failure</returns>
        public MailService.MailStatus SendDiscussionNotification(IMailService theMailService, int applicationStageStepDiscussionCommentId, bool isNew)
        {
            ValidateInt(applicationStageStepDiscussionCommentId, FullName(nameof(ApplicationScoringService), nameof(SendDiscussionNotification)), nameof(applicationStageStepDiscussionCommentId));
            //First retrieve the data users to send notification to
            var comment =
                UnitOfWork.ApplicationStageStepDiscussionCommentRepository.GetByID(
                    applicationStageStepDiscussionCommentId);
            var recipientList = comment.GetRecipientList();
            var statusList = new List<MailService.MailStatus>();
            foreach (var recipient in recipientList)
            {
                //Build the query object
                string prefixName = null;
                if(recipient.Prefix != null)
                {
                    prefixName = recipient.Prefix.PrefixName;
                }
                var emailData = new DiscussionNotificationModel(recipient.User.PrimaryUserEmailAddress(), prefixName, recipient.FirstName,
                    recipient.LastName, comment.AuthorPanelAssignment()?.ClientParticipantType.ParticipantTypeName, comment.AuthorApplicationAssignment().SortOrder,
                    comment.AuthorRole(), comment.AuthorPanelAssignment()?.ClientRole?.RoleName, comment.Application().LogNumber,
                    comment.Application().ApplicationTitle, comment.Application().ProgramMechanism.ProgramYear.Year, comment.Application().ProgramMechanism.ProgramYear.ClientProgram.ProgramAbbreviation,
                    comment.ApplicationStageStepDiscussion.ApplicationStageStep.ApplicationStage.PanelApplication.SessionPanel.PanelAbbreviation,
                    comment.ApplicationStageStepDiscussion.ApplicationStageStep.PanelStageStep.EndDate ?? GlobalProperties.P2rmisDateTimeNow, comment.ModifiedDate ?? GlobalProperties.P2rmisDateTimeNow, isNew,
                    comment.ApplicationStageStepDiscussion.ApplicationStageStep.PanelStageStep.ReOpenDate, comment.ApplicationStageStepDiscussion.ApplicationStageStep.PanelStageStep.ReCloseDate
                    );
                
                //Send the data to the mail service to send an email
                statusList.Add(theMailService.SendDiscussionNotification(emailData));
            }
            //TODO: Come up with a hierarchy for MailStatus for batch email situations so the correct status can be returned if multiple
            return statusList.First();
        }
    }
}
