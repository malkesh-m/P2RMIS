
-- =============================================
-- Author:		Craig Henson
-- Create date: 2/11/2013
-- Description:	Stored procedure to retrieve reviewer info for client access
-- =============================================
CREATE PROCEDURE [dbo].[ReviewerInfo] 
	-- Add the parameters for the stored procedure here
	@PanelApplicationId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     ViewSessionPanel.SessionPanelId AS PanelID, ViewApplication.LogNumber AS ApplicationID, ViewPanelUserAssignment.PanelUserAssignmentId, udfApplicationWorkflowLastStep.ApplicationWorkflowStepId, ClientParticipantType.ParticipantTypeAbbreviation AS PartType, 
                      (CASE WHEN ClientAssignmentType.AssignmentTypeId = 8 THEN 'COI' ELSE ' ' END) AS COI, 
                      (CASE WHEN ClientAssignmentType.AssignmentTypeId IN (5, 6, 9) THEN 'Yes' ELSE ' ' END) AS Critique_Required, ClientParticipantType.ParticipantTypeName AS PartTypeDesc, ClientRole.RoleName As RoleName, ViewUserInfo.FirstName, 
                      ViewUserInfo.Lastname, Prefix.PrefixName AS Prefix, ViewUserInfo.Lastname + ',' + ' ' + ViewUserInfo.FirstName AS Name, (CASE WHEN ViewUserApplicationComment.UserApplicationCommentID IS NOT NULL
                      THEN '1' ELSE '0 ' END) AS Comment, udfApplicationWorkflowLastStep.Resolution AS CritiqueSubmitted, 
                      (CASE WHEN EXISTS
                          (SELECT     'X'
                            FROM          ViewApplicationWorkflowStepElementContent INNER JOIN
							ViewApplicationWorkflowStepElement ON ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
							ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
							ViewApplicationWorkflow AS AppWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = AppWorkflow.ApplicationWorkflowId INNER JOIN
							ViewApplicationStage AS AppStage ON AppWorkflow.ApplicationStageId = AppStage.ApplicationStageId AND AppStage.ReviewStageId = 2 INNER JOIN
							ViewPanelApplication AS PanApp ON AppStage.PanelApplicationId = PanApp.PanelApplicationId
                            WHERE      (PanApp.PanelApplicationId = ViewPanelApplication.PanelApplicationId) AND (AppWorkflow.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId) AND (ViewApplicationWorkflowStepElementContent.CritiqueRevised = 1)) THEN 'meeting' ELSE 'initial' END) AS CritiquePhase, udfPanelStageLastStep.EndDate AS PhaseEnd, ViewPanelApplicationReviewerAssignment.SortOrder, ViewPanelApplication.PanelApplicationId, ViewPanelUserAssignment.UserId AS ReviewerUserId,
							CASE WHEN ViewApplicationBudget.Comments IS NULL THEN 0 ELSE 1 END AS AdminNotesExist, ViewApplication.ApplicationId AS ApplicationId1
FROM         ViewPanelApplication INNER JOIN
			ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
			ViewSessionPanel ON ViewPanelApplication.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
			ViewPanelStage ON ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId AND ViewPanelStage.ReviewStageId = 1 CROSS APPLY
			udfPanelStageLastStep(ViewPanelStage.PanelStageId) INNER JOIN
			ViewPanelUserAssignment ON ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId LEFT OUTER JOIN
			ViewPanelApplicationReviewerAssignment ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId
				AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
			ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId	LEFT OUTER JOIN
			ClientRole ON ViewPanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId INNER JOIN
			ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = 1 INNER JOIN
			ViewUser ON ViewPanelUserAssignment.UserId = ViewUser.UserID INNER JOIN
			ViewUserInfo ON ViewUser.UserID = ViewUserInfo.UserID LEFT OUTER JOIN 
			Prefix ON ViewUserInfo.PrefixId = Prefix.PrefixId INNER JOIN
			ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId LEFT OUTER JOIN
			ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId OUTER APPLY
			udfApplicationWorkflowLastStep(ViewApplicationWorkflow.ApplicationWorkflowId) LEFT OUTER JOIN
			ViewUserApplicationComment ON ViewPanelApplication.PanelApplicationId = ViewUserApplicationComment.PanelApplicationId AND ViewUserApplicationComment.CommentTypeID = 5 AND ViewUser.UserID = ViewUserApplicationComment.UserID LEFT OUTER JOIN
			ViewApplicationBudget ON ViewApplication.ApplicationId = ViewApplicationBudget.ApplicationId
WHERE ClientParticipantType.ReviewerFlag = 1 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[ReviewerInfo] TO [NetSqlAzMan_Users]
    AS [dbo];

