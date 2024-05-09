CREATE PROCEDURE [dbo].[uspResetPanelToNotStarted]
	@SessionPanelId int,
	@DeleteAllCritiques bit = 0
AS
BEGIN
DECLARE
	@CurrentDateTime datetime2(0) = dbo.GetP2rmisDateTime();
--Reset panel dates to future
	UPDATE SessionPanel SET StartDate = DATEADD(day, 10, @CurrentDateTime), EndDate = DATEADD(day, 12, @CurrentDateTime)
	WHERE DeletedFlag = 0 AND SessionPanelId = @SessionPanelId;

	UPDATE MeetingSession SET StartDate = DATEADD(day, 10, @CurrentDateTime), EndDate = DATEADD(day, 12, @CurrentDateTime)
	FROM MeetingSession INNER JOIN
		ViewSessionPanel ON MeetingSession.MeetingSessionId = ViewSessionPanel.MeetingSessionId
	WHERE MeetingSession.DeletedFlag = 0 AND SessionPanelId = @SessionPanelId;

	UPDATE ClientMeeting SET StartDate = DATEADD(day, 10, @CurrentDateTime), EndDate = DATEADD(day, 12, @CurrentDateTime)
	FROM ClientMeeting INNER JOIN 
		ViewMeetingSession MeetingSession ON ClientMeeting.ClientMeetingId = MeetingSession.ClientMeetingId INNER JOIN
		ViewSessionPanel ON MeetingSession.MeetingSessionId = ViewSessionPanel.MeetingSessionId
	WHERE SessionPanelId = @SessionPanelId;

	UPDATE PanelStageStep SET StartDate = DATEADD(day, 10, @CurrentDateTime), EndDate = DATEADD(day, 12, @CurrentDateTime), ReOpenDate = NULL, ReCloseDate = NULL
	FROM PanelStageStep INNER JOIN
		ViewPanelStage ON PanelStageStep.PanelStageId = ViewPanelStage.PanelStageId
	WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.DeletedFlag = 0 AND PanelStageStep.StepTypeId = 8

	UPDATE PanelStageStep SET StartDate = DATEADD(day, 7, @CurrentDateTime), EndDate = DATEADD(day, 9, @CurrentDateTime), ReOpenDate = NULL, ReCloseDate = NULL
	FROM PanelStageStep INNER JOIN
		ViewPanelStage ON PanelStageStep.PanelStageId = ViewPanelStage.PanelStageId
	WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.DeletedFlag = 0 AND PanelStageStep.StepTypeId = 7

	UPDATE PanelStageStep SET StartDate = DATEADD(day, 4, @CurrentDateTime), EndDate = DATEADD(day, 6, @CurrentDateTime), ReOpenDate = NULL, ReCloseDate = NULL
	FROM PanelStageStep INNER JOIN
		ViewPanelStage ON PanelStageStep.PanelStageId = ViewPanelStage.PanelStageId
	WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.DeletedFlag = 0 AND PanelStageStep.StepTypeId = 6

	UPDATE PanelStageStep SET StartDate = @CurrentDateTime, EndDate = DATEADD(day, 3, @CurrentDateTime), ReOpenDate = NULL, ReCloseDate = NULL
	FROM PanelStageStep INNER JOIN
		ViewPanelStage ON PanelStageStep.PanelStageId = ViewPanelStage.PanelStageId
	WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.DeletedFlag = 0 AND PanelStageStep.StepTypeId = 5
--Unrelease assignments
	UPDATE ApplicationStage SET AssignmentVisibilityFlag = 0, AssignmentReleaseDate = NULL
	FROM ApplicationStage INNER JOIN
		ViewPanelApplication ON ApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
	WHERE ViewPanelApplication.SessionPanelId = @SessionPanelId AND ApplicationStage.DeletedFlag = 0
--Delete critiques
	IF (@DeleteAllCritiques = 1)
	BEGIN
		UPDATE ApplicationWorkflowStepElementContent SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowStepElementContent INNER JOIN
			ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
			ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
			ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
			ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
			PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
		WHERE PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElementContent.DeletedFlag = 0
		UPDATE ApplicationWorkflowStepElement SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowStepElement INNER JOIN
			ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
			ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
			ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
			PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
		WHERE PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElement.DeletedFlag = 0
		UPDATE ApplicationWorkflowStepAssignment SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowStepAssignment INNER JOIN
			ApplicationWorkflowStep ON ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
			ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
			ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
			PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
		WHERE PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepAssignment.DeletedFlag = 0
		UPDATE ApplicationWorkflowStepWorkLog SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = @CurrentDateTime
		FROM ApplicationWorkflowStepWorkLog INNER JOIN
			ApplicationWorkflowStep ON ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
			ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
			ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
			PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
		WHERE PanelApplication.SessionPanelId = @SessionPanelId AND	ApplicationWorkflowStepWorkLog.DeletedFlag = 0
		UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowStep INNER JOIN
			ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
			ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
			PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
		WHERE PanelApplication.SessionPanelId = @SessionPanelId AND	ApplicationWorkflowStep.DeletedFlag = 0
		UPDATE ApplicationWorkflow SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflow INNER JOIN
			ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
			PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
		WHERE PanelApplication.SessionPanelId = @SessionPanelId AND	ApplicationWorkflow.DeletedFlag = 0
			
	END
END
