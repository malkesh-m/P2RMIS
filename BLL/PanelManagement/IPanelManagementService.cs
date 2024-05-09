using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System;
using System.Collections.Generic;
using WebModel = Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// PanelManagement application services.
    /// </summary>
    public partial interface IPanelManagementService
    {
        /// <summary>
        /// Updates the applications within a panel order of review.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier; identifies the panel being reordered</param>
        /// <param name="collection">Collection of SetOrderOfReviewToSave objects describing the reordering.</param>
        /// <param name="userId">User requesting the action</param>
        void SetOrderOfReview(int sessionPanelId, ICollection<SetOrderOfReviewToSave> collection, int userId);
        /// <summary>
        /// Gets the assignment type threshold.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        IAssignmentTypeThreshold GetAssignmentTypeThreshold(int sessionPanelId);
        /// <summary>
        /// Sets the assignment type threshold.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="scientistReviewerSortOrder">The scientist reviewer sort order.</param>
        /// <param name="advocateConsumerSortOrder">The advocate consumer sort order.</param>
        /// <param name="specialistReviewerSortOrder">The specialist reviewer sort order.</param>
        /// <returns></returns>
        IAssignmentTypeThreshold SetAssignmentTypeThreshold(int sessionPanelId, int? scientistReviewerSortOrder, int? advocateConsumerSortOrder,
            int? specialistReviewerSortOrder);
        /// <summary>
        /// Calculates the individual reviewer presentation order counts for a single panel.
        /// </summary>
        /// <param name="expertise">A panel's ReviewerExpertise objects for a session panel</param>
        /// <returns>Dictionary of OrderOfReviewCounts (basically the header information for the Reviewer Experience tab of the Panel Management application</returns>
        Dictionary<int, OrderOfReviewCounts> CalculatePresentationOrderCounts(IEnumerable<WebModel.IReviewerExpertise> expertise);
        /// <summary>
        /// Create a Reviewer Evaluation and insert it into the ReviewerEvaluation
        /// </summary>
        /// <param name="reviewerEvaluations">Reviewer Evaluations To record</param>
        /// <param name="userId">User identifier</param>
        void SaveReviewerEvaluation(List<WebModel.ReviewerEvaluation> reviewerEvaluations, int userId);
        /// <summary>
        /// Calculates the reviewer experience for an application on a panel.
        /// </summary>
        /// <param name="expertise">A panel's ReviewerExpertise objects for a session panel</param>
        /// <returns>Dictionary of ExperienceCounts</returns>
        Dictionary<int, ExperienceCounts> CalculateExpertiseCounts(IEnumerable<WebModel.IReviewerExpertise> expertise);
        /// <summary>
        /// Detects if a reviewer has a workflow associated with an application
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <returns>True if the reviewer has a step assignment associated with the application. Otherwise return false.</returns>
        bool ReviewerHasWorkflow(int panelUserAssignmentId, int panelApplicationId);
        /// <summary>
        /// Un-assigns a reviewer from an application and flag step assignment and critique data as deleted
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <param name="userId">The current user identifier</param>
        void UnAssignReviewer(int panelUserAssignmentId, int panelApplicationId, int userId, bool removeMeetingCritique = false);
        /// <summary>
        /// Saves the assignment.
        /// </summary>
        /// <param name="clientExpertiseRatingId">The client expertise rating identifier.</param>
        /// <param name="clientCoiTypeId">The client coi type identifier.</param>
        /// <param name="presentationOrder">The presentation order.</param>
        /// <param name="clientAssignmentTypeId">The client assignment type identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isWorkflowDeletionOverride">if set to <c>true</c> [is workflow deletion override].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="clientAssignmentChanged">whether the client assignment has changed</param>
        /// <returns></returns>
        ReviewerAssignmentStatus SaveAssignment(int? clientExpertiseRatingId, int? clientCoiTypeId, int? presentationOrder, int? clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, string comment, bool isWorkflowDeletionOverride, int userId, bool clientAssignmentChanged);
        /// <summary>
        /// Determines if all applications in a panel can be released.  If the applications can be 
        /// released then it releases them.  
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="userId">User identifier of user making the change</param>
        /// <returns>ReleaseStatus indicating status of release</returns>
        ReleaseStatus ReleaseApplications(int sessionPanelId, int userId);
        /// <summary>
        /// Sets up all the necessary information for a reviewer to evaluate an application
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="panelApplicationId">The application identifier</param>
        /// <param name="userId">User identifier</param>
        void StartReviewerWorkflow(int panelUserAssignmentId, int panelApplicationId, int userId);
        /// <summary>
        /// Determines whether [is meeting phase started] [the specified panel application identifier].
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is meeting phase started] [the specified panel application identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsMeetingPhaseStarted(int panelApplicationId);
        /// <summary>
        /// Get a session panel's program year information
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>IProgramYearModel model</returns>
        WebModel.IProgramYearModel GetProgramYear(int sessionPanelId);
        /// <summary>
        /// Gets the session panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        WebModel.ISessionPanelModel GetSessionPanel(int sessionPanelId);
        /// <summary>
        /// Retrieves the content of the specified email
        /// </summary>
        /// <param name="communicationsLogId"></param>
        /// <returns></returns>
        WebModel.IEmailContent GetEmailContent(int communicationsLogId);
        /// <summary>
        /// Returns a list of the emails for the indicated session panel
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <returns></returns>
        Container<WebModel.ISessionPanelCommunicationsList> ListPanelCommunicationMessages(int sessionPanelId);
        /// <summary>
        /// Submit critiques contained in the list of application work flow steps, that
        /// are not empty and have not been submitted by setting the ApplicationWorkflowStep
        /// to 'Resolved' and supplying a resolution date
        /// </summary>
        /// <param name="theWorkflowService">An instance of the workflow service</param>
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <param name="stepTypeId">Step type identifier (identifies the phase)</param>
        /// <param name="userId">User identifier of submitting user</param>
        void FinalizeCritique(IWorkflowService theWorkflowService, int sessionPanelId, int stepTypeId, int userId);
        /// <summary>
        /// Checks if the critique is submittable
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier</param>
        /// <returns>True if the critique is submittable; false otherwise </returns>
        bool IsCritiqueSubmittable(int applicationWorkflowId);
        /// <summary>
        /// Retrieves contents of the reviewers critique
        /// </summary>
        /// <param name="applicationWorkflowStepId">The applicationWorkflowStep identifier</param>
        /// <returns>IApplicationCritiqueDetailsModel</returns>
        WebModel.IApplicationCritiqueDetailsModel GetApplicationCritiqueDetails(int applicationWorkflowStepId);
        /// <summary>
        /// Update the phase ReOpen date & Close date.  Dates are validated (endDate <= reOpenDate <= closeDate)
        /// </summary>
        /// <param name="endDate">The EndDate of the phase</param>
        /// <param name="reOpenDate">The new ReOpen date of the phase</param>
        /// <param name="closeDate">The new Close date of the phase<</param>
        /// <param name="meetingSessionId">Meeting session identifier</param>
        /// <param name="stageTypeId">Stage type identifier indicating the phase</param>
        /// <param name="userId">User identifier of the person making the change</param>
        /// <returns>PanelStageDateUpdateStatus enum value indicating the result of the validation/update</returns>
        PanelStageDateUpdateStatus UpdatePanelStageDates(DateTime endDate, DateTime reOpenDate, DateTime closeDate, int meetingSessionId, int stageTypeId, int userId);
        /// <summary>
        /// Sends the critique reset email.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="mailService">The mail service.</param>
        /// <param name="userId">The user identifier.</param>
        void SendCritiqueResetEmail(int applicationWorkflowId, IMailService mailService, int userId);
        /// <summary>
        /// Retrieves the PanelApplication Summary text.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>PanelApplicationSummary text</returns>
        string GetPanelSummary(int panelApplicationId);
        /// <summary>
        /// Save the panel overview paragraph.
        /// </summary>
        /// <param name="panelApplicationId">Application entity identifier</param>
        /// <param name="panelOverview">Overview content</param>
        /// <param name="userId">User identifier of submitting user</param>
        bool SavePanelOverview(int panelApplicationId, string panelOverview, int userId);
        /// <summary>
        /// Adds the panel applications.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="applicationIds">The application ids.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool AddPanelApplications(int panelId, List<int> applicationIds, int userId);
        /// <summary>
        /// Adds the batch panel applications.
        /// </summary>
        /// <param name="panelApplicationsList">The panel applications list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool AddBatchPanelApplications(List<KeyValuePair<int, List<int>>> panelApplicationsList, int userId);
        /// <summary>
        /// Mark a user as a COI.  This defaults several values & is primarily used by the Manage Application Scoring.
        /// </summary>
        /// <param name="panelApplicationId">The application identifier</param>
        /// <param name="panelUserAssignmentId">The panel user assignment</param>
        /// <param name="clientCoiTypeId">ClientCoiType entity identifier</param>
        /// <param name="comment">The comment associated with the assignment</param>
        /// <param name="userId">The user identifier making the assignment</param>
        void AssignAsCoi(int panelApplicationId, int panelUserAssignmentId, int? clientCoiTypeId, string comment, int userId);
        /// <summary>
        /// Gets the meeting session id for this session panel
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>Meeting Session Identifier</returns>
        int? GetMeetingSessionId(int sessionPanelId);
        /// <summary>
        /// Determines whether [is meeting current] [the specified session panel identifier].
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is meeting current] [the specified session panel identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsMeetingCurrent(int sessionPanelId);
        /// <summary>
        /// Retrieves the header & grid contents for the Evaluation Details modal
        /// </summary>
        /// <returns>Container of IRatingEvaluationModels</returns>
        /// <param name="userId">User entity identifier</param>
        Container<IRatingEvaluationModel> RetrieveEvaluationDetails(int userId);
        /// <summary>
        /// Determines if a specified user has an existing assignment or potential assignment 
        /// to an SRO's panel.
        /// </summary>
        /// <param name="sroUserId">User entity identifier of an SRO</param>
        /// <param name="reviewerUserInfoId">UserInfo entity identifier of target user</param>
        /// <returns>True if the user is has an assignment or potential assignment to the SRO's panel; false otherwise</returns>
        bool IsUserAssignedToSroPanel(int sroUserId, int reviewerUserInfoId);
        /// <summary>
        /// Saves a MOD comment to a discussion (container).  If a discussion container is not instantiated currently 
        /// an ApplicationStageStepDiscussion container is created to hold the comment
        /// </summary>
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier</param>
        /// <param name="applicationStageStepDiscussionEntityId">ApplicationStageStepDiscussion entity identifier</param>
        /// <param name="comment">MOD comment</param>
        /// <param name="userId">User entering comment</param>
        /// <returns>ApplicationStageStepDiscussionComment entity identifier of created entity.</returns>
        CommentTypeModel SaveModComment(int applicationStageStepEntityId, int? applicationDicussionEntityId, string comment, int userId, bool isNew);

        /// <summary>
        /// Saves the assignment with the current presentation order.
        /// </summary>
        /// <param name="clientExpertiseRatingId">ClientExpertiseRating entity identifier.</param>
        /// <param name="clientCoiTypeId">ClientCoiType entity identifier.</param>
        /// <param name="clientAssignmentTypeId">ClientAssignmentType entity identifier.</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier.</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isWorkflowDeletionOverride">if set to <c>true</c> [is workflow deletion override].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="clientAssignmentChanged">Whether the client assignment has changed.</param>
        /// <returns>Assignment status </returns>
        /// <remarks>Candidate for re-factoring to better separate expertise, assignment, and un-assignment</remarks>
        ReviewerAssignmentStatus SaveAssignmentWithCurrentPresentationOrder(int? clientExpertiseRatingId, int? clientCoiTypeId, int? clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, string comment, bool isWorkflowDeletionOverride, int userId, bool clientAssignmentChanged);
        /// <summary>
        /// Process a request for reviewer transfer between panels.
        /// </summary>
        /// <param name="theMailService">MailService</param>
        /// <param name="reviewerName">Reviewer name to transfer</param>
        /// <param name="sourcePanelId">Reviewer's SessionPanel entity identifier</param>
        /// <param name="sourcePanelName">Reviewer's SessionPanel name</param>
        /// <param name="sourcePanelAbbr">Reviewer's SessionPanel abbreviation</param>
        /// <param name="targetPanelId">Target SessionPanel entity identifier</param>
        /// <param name="comment">Comments on the transfer</param>
        /// <param name="userId">User entity identifier of the user requesting transfer</param>
        void RequestReviewerTransferProcess(IMailService theMailService, string reviewerName, int sourcePanelId, string sourcePanelName, string sourcePanelAbbr, int? targetPanelId, string comment, int userId);
        /// <summary>
        /// Process a request for reviewer release from a panels.
        /// </summary>
        /// <param name="theMailService">MailService</param>
        /// <param name="reviewerName">Reviewer name to transfer</param>
        /// <param name="sourcePanelId">Reviewer's SessionPanel entity identifier</param>
        /// <param name="sourcePanelName">Reviewer's SessionPanel name</param>
        /// <param name="sourcePanelAbbr">Reviewer's SessionPanel abbreviation</param>
        /// <param name="comment">Comments on the transfer</param>
        /// <param name="userId">User entity identifier of the user requesting transfer</param>
        void RequestReviewerReleaseProcess(IMailService theMailService, string reviewerName, int sourcePanelId, string sourcePanelName, string sourcePanelAbbr, string comment, int userId);
        /// <summary>
        /// Constructs a web model indicating if the Applications were released & the date time of their release.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>ReleasePanelModel containing an indication if the Applications were released & the date time of there release</returns>
        WebModel.IReleasePanelModel NewIsReleased(int sessionPanelId);
        /// <summary>
        /// Determines if a SessionPanel's Applications have been set up for scoring.
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>ISessionApplicationScoringSetupModel model indicating if the session's applications have been set up for scoring</returns>
        WebModel.ISessionApplicationScoringSetupModel IsSessionApplicationsScoringSetUp(int sessionPanelId);
        /// <summary>
        /// Indicates if the SessionPanel is  Online 
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>True if the SessionPanel is  Online; false otherwise</returns>
        bool IsOnline(int? sessionPanelId);
        /// <summary>
        /// Removes the application from panel.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void RemoveApplicationFromPanel(int panelApplicationId, int userId);
        /// <summary>
        /// Transfers the application to panel.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="newSessionPanelId">The new session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void TransferApplicationToPanel(int panelApplicationId, int applicationId, int newSessionPanelId, int userId);

        /// <summary>
        /// Removes the user from their assigned panel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void RemoveUserFromPanel(int panelUserAssignmentId, int userId);
        /// <summary>
        /// Adds the staff to panel.
        /// </summary>
        /// <param name="assignedUserId">The user identifier to be assigned.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="actorUserId">The user identifier of the user making the assignment.</param>
        /// <returns>
        /// Name of the participant type the user was assigned.
        /// </returns>
        /// <exception cref="ArgumentException">Supplied assigned user could not be mapped to a valid participant type. Verify mapping exists in RoleParticipantType.</exception>
        string AddStaffToPanel(int assignedUserId, int sessionPanelId, int actorUserId);
        bool HasAssignedApplications(int panelUserAssignmentId);
        /// <summary>
        /// Get Session Panel Id by Panel Application Id
        /// </summary>
        /// <param name="panelApplicationId"></param>
        /// <returns></returns>
        int GetSessionPanelIdByPanelApplicationId(int panelApplicationId);
        /// <summary>
        /// get application by param
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <param name="programYearId"></param>
        /// <param name="panelId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="awardId"></param>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        List<IApplicationsManagementModel> GetApplications(int? clientId, string fiscalYear, int? programYearId, int? panelId,
            int? receiptCycle, int? awardId, string logNumber, int userId);
     
        /// <summary>
        /// Gets the referral mapping error messsages.
        /// </summary>
        /// <param name="referralMappingData">The referral mapping data.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        List<string> GetReferralMappingUploadErrorMessages(List<ReferralMappingDataModel> referralMappingData, int programYearId, int receiptCycle,
            string programAbbreviation, string year);
        /// <summary>
        /// Gets the referral mapping release error messages.
        /// </summary>
        /// <param name="referralMappingData">The referral mapping data.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        List<string> GetReferralMappingReleaseErrorMessages(List<ReferralMappingDataModel> referralMappingData, int programYearId, int receiptCycle,
            string programAbbreviation, string year);

        /// <summary>
        /// Determines whether the user has completed all required expertise ratings
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if all expertise on the panel was complete; otherwise false;</returns>
        bool IsUserExpertiseComplete(int sessionPanelId, int userId, bool manageCritiques);
    }
}