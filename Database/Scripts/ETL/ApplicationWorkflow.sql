--First we create workflows stage 1 and 2 for assigned reviewers (other than COI and Reader) 
INSERT INTO [dbo].[ApplicationWorkflow]
           ([WorkflowId]
           ,[ApplicationStageId]
           ,[ApplicationTemplateId]
           ,[PanelUserAssignmentId]
		   ,[ApplicationWorkflowName]
		   ,[DateAssigned]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
SELECT CASE WHEN SUM(Phase_ID) = 1 THEN (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Pre-Meeting (Legacy)' AND Workflow.ClientId = ClientMeeting.ClientId)
			WHEN SUM(Phase_ID) = 3 THEN (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Pre-Meeting' AND Workflow.ClientId = ClientMeeting.ClientId)
			WHEN SUM(Phase_ID) = 4 THEN (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Online Discussion (Legacy)' AND Workflow.ClientId = ClientMeeting.ClientId)
			ELSE (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Online Discussion' AND Workflow.ClientId = ClientMeeting.ClientId) END,
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelApplicationReviewerAssignment.CreatedDate,
			PanelApplicationReviewerAssignment.ModifiedBy,
			PanelApplicationReviewerAssignment.ModifiedDate
FROM		ViewPanelApplication PanelApplication INNER JOIN
			ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
			ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
			ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
			[P2RMIS].dbo.MTG_Phase_Member ON MeetingSession.LegacySessionId = MTG_Phase_Member.Session_ID INNER JOIN
			ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId INNER JOIN
			ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
			ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
			ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId
WHERE		ApplicationStage.ReviewStageId = 1 AND ClientAssignmentType.AssignmentTypeId NOT IN (7, 8)
			AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflow WHERE ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId)
GROUP BY PanelApplication.ApplicationId,
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			ClientMeeting.ClientId,
			PanelApplicationReviewerAssignment.CreatedDate,
			PanelApplicationReviewerAssignment.ModifiedBy,
			PanelApplicationReviewerAssignment.ModifiedDate
UNION ALL
SELECT		(SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Meeting' AND Workflow.ClientId = ClientMeeting.ClientId),
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelApplicationReviewerAssignment.CreatedDate,
			PanelApplicationReviewerAssignment.ModifiedBy,
			PanelApplicationReviewerAssignment.ModifiedDate
FROM		ViewPanelApplication PanelApplication INNER JOIN
			ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
			ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
			ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
			ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId INNER JOIN
			ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
			ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
			ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId
WHERE		ApplicationStage.ReviewStageId = 2 AND ClientAssignmentType.AssignmentTypeId NOT IN (7, 8)
			AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflow WHERE ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId)
ORDER BY ApplicationTemplateId, PanelUserAssignment.PanelUserAssignmentId 
			


--Next we create workflows for unassigned reviewers stage 2 (other than COIs) and only those that have meeting scores
INSERT INTO [dbo].[ApplicationWorkflow]
           ([WorkflowId]
           ,[ApplicationStageId]
           ,[ApplicationTemplateId]
		   ,[PanelUserAssignmentId]
           ,[ApplicationWorkflowName]
		   ,[DateAssigned]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
SELECT (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Meeting' AND Workflow.ClientId = ClientMeeting.ClientId),
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.CreatedDate,
			PanelUserAssignment.ModifiedBy,
			PanelUserAssignment.ModifiedDate
FROM         ClientAssignmentType INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON 
                      ClientAssignmentType.ClientAssignmentTypeId = PanelApplicationReviewerAssignment.ClientAssignmentTypeId RIGHT OUTER JOIN
                      ViewPanelUserAssignment PanelUserAssignment INNER JOIN
                      ViewSessionPanel SessionPanel ON PanelUserAssignment.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
                      ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
					  ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
                      ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
                      ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
                      ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
                      ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId ON 
                      PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId AND 
                      PanelApplicationReviewerAssignment.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
					  ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
WHERE      (ApplicationStage.ReviewStageId = 2) AND (ClientAssignmentType.AssignmentTypeId = 7 OR
                      ClientAssignmentType.AssignmentTypeId IS NULL) AND ClientParticipantType.ReviewerFlag = 1 AND EXISTS (Select 'X' FROM [P2RMIS].dbo.PRG_Panel_Scores WHERE LOG_NO = [Application].LogNumber)
					  AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflow WHERE ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId)