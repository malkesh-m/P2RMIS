CREATE PROCEDURE [dbo].[uspChangePanelSession]
	@SessionPanelId int,
	@MeetingSessionId int,
	@UserId int = 10
AS
BEGIN
	SET XACT_ABORT ON
	DECLARE @StepsAdded TABLE (
		StepTypeId int
	);
	DECLARE @StepsRemoved TABLE (
		StepTypeId int
	);
	DECLARE @CurrentDateTime datetime2(0);
	SELECT @CurrentDateTime = dbo.GetP2rmisDateTime();

	INSERT INTO @StepsAdded (StepTypeId)
	SELECT SessionPhase.StepTypeId
	FROM SessionPhase
	WHERE DeletedFlag = 0 AND SessionPhase.MeetingSessionId = @MeetingSessionId
	EXCEPT
	SELECT ViewPanelStageStep.StepTypeId
	FROM ViewPanelStageStep
	INNER JOIN ViewPanelStage ON ViewPanelStageStep.PanelStageId = ViewPanelStage.PanelStageId 
	WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND ViewPanelStage.ReviewStageId IN (1,2);
	
	INSERT INTO @StepsRemoved (StepTypeId)
	SELECT ViewPanelStageStep.StepTypeId
	FROM ViewPanelStageStep
	INNER JOIN ViewPanelStage ON ViewPanelStageStep.PanelStageId = ViewPanelStage.PanelStageId 
	WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND ViewPanelStage.ReviewStageId IN (1,2)
	EXCEPT
	SELECT SessionPhase.StepTypeId
	FROM SessionPhase
	WHERE DeletedFlag = 0 AND SessionPhase.MeetingSessionId = @MeetingSessionId;

	--First update the direct panel reference
	UPDATE SessionPanel SET MeetingSessionId = @MeetingSessionId, ModifiedBy = @UserId, ModifiedDate = @CurrentDateTime
	WHERE SessionPanelId = @SessionPanelId;

	--If any step(s) are being added
	IF EXISTS (SELECT 1 FROM @StepsAdded)
	BEGIN
		IF EXISTS (SELECT 1 FROM @StepsAdded WHERE StepTypeId = 8)
		BEGIN
			INSERT INTO [dbo].[PanelStage]
			   ([SessionPanelId]
			   ,[ReviewStageId]
			   ,[StageOrder]
			   ,[WorkflowId]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
			SELECT TOP(1) ViewSessionPanel.SessionPanelId, 2, 2, ViewWorkflow.WorkflowId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM ViewSessionPanel
			INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
			INNER JOIN ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
			INNER JOIN ViewWorkflow ON ViewClientMeeting.ClientId = ViewWorkflow.ClientId AND ViewWorkflow.ReviewStageId = 2
			WHERE ViewSessionPanel.SessionPanelId = @SessionPanelId AND NOT EXISTS (SELECT 'X' FROM ViewPanelStage WHERE SessionPanelId = @SessionPanelId AND ReviewStageId = 2); 
		END
		--Add step at panel level
		INSERT INTO PanelStageStep
		([PanelStageId]
			,[StepTypeId]
			,[StepName]
			,[StepOrder]
			,[StartDate]
			,[EndDate]
			,[ReOpenDate]
			,[ReCloseDate]
			,[CreatedBy]
			,[CreatedDate]
			,[ModifiedBy]
			,[ModifiedDate])
		SELECT ViewPanelStage.PanelStageId, SessionPhase.StepTypeId, StepType.StepTypeName, 
		--For future proof, this should come from SessionPhase, but looks like it's not being set in the DB so we use static logic for the order for now
		CASE WHEN StepType.StepTypeId IN (5,8) THEN 1 WHEN StepType.StepTypeId = 6 THEN 2 WHEN StepType.StepTypeId = 7 THEN 3 END,
		SessionPhase.StartDate, SessionPhase.EndDate, SessionPhase.ReopenDate, SessionPhase.CloseDate, @UserId, @CurrentDateTime,
		@UserId, @CurrentDateTime
		FROM SessionPhase
		INNER JOIN @StepsAdded StepsAdded ON SessionPhase.StepTypeId = StepsAdded.StepTypeId
		INNER JOIN StepType ON StepsAdded.StepTypeId = StepType.StepTypeId
		INNER JOIN ViewPanelStage ON StepType.ReviewStageId = ViewPanelStage.ReviewStageId
		WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND SessionPhase.MeetingSessionId = @MeetingSessionId AND SessionPhase.DeletedFlag = 0;
		--Add step at application stage step level
		INSERT INTO [dbo].[ApplicationStageStep]
           ([ApplicationStageId]
           ,[PanelStageStepId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT ViewApplicationStage.ApplicationStageId, ViewPanelStageStep.PanelStageStepId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
		FROM ViewSessionPanel
		INNER JOIN ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId
		INNER JOIN ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId
		INNER JOIN ViewPanelStage ON ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId AND ViewApplicationStage.ReviewStageId = ViewPanelStage.ReviewStageId
		INNER JOIN ViewPanelStageStep ON ViewPanelStage.PanelStageId = ViewPanelStageStep.PanelStageId
		WHERE ViewSessionPanel.SessionPanelId = @SessionPanelId 
			AND NOT EXISTS (SELECT 'X' FROM ViewApplicationStageStep WHERE ApplicationStageId = ViewApplicationStage.ApplicationStageId AND PanelStageStepId = ViewPanelStageStep.PanelStageStepId);
		--Add step to any critiques, associated elements, and (just in case this is done at the last second) copy critiques if previous phase submitted
		INSERT INTO [dbo].[ApplicationWorkflowStep]
           ([ApplicationWorkflowId]
           ,[StepTypeId]
           ,[StepName]
           ,[Active]
           ,[StepOrder]
           ,[Resolution]
           ,[ResolutionDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT ViewApplicationWorkflow.ApplicationWorkflowId, ViewPanelStageStep.StepTypeId, ViewPanelStageStep.StepName, CASE WHEN ViewPanelStageStep.StepTypeId = 7 THEN 0 ELSE 1 END,
		ViewPanelStageStep.StepOrder, 0, NULL, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
		FROM ViewSessionPanel
		INNER JOIN ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId
		INNER JOIN ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId
		INNER JOIN ViewPanelStage ON ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId AND ViewApplicationStage.ReviewStageId = ViewPanelStage.ReviewStageId
		INNER JOIN ViewPanelStageStep ON ViewPanelStage.PanelStageId = ViewPanelStageStep.PanelStageId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId
		WHERE ViewSessionPanel.SessionPanelId = @SessionPanelId
			AND NOT EXISTS (SELECT 'X' FROM ViewApplicationWorkflowStep WHERE ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId AND StepTypeId = ViewPanelStageStep.StepTypeId);

		INSERT INTO [dbo].[ApplicationWorkflowStepElement]
           ([ApplicationWorkflowStepId]
           ,[ApplicationTemplateElementId]
           ,[AccessLevelId]
           ,[ClientScoringId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT ViewApplicationWorkflowStep.ApplicationWorkflowStepId, ViewApplicationTemplateElement.ApplicationTemplateElementId, 1, ViewMechanismTemplateElementScoring.ClientScoringId,
		@UserId, @CurrentDateTime, @UserId, @CurrentDateTime
		FROM ViewSessionPanel
		INNER JOIN ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId
		INNER JOIN ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId
		INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId
		INNER JOIN ViewApplicationTemplate ON ViewApplicationStage.ApplicationStageId = ViewApplicationTemplate.ApplicationStageId
		INNER JOIN ViewApplicationTemplateElement ON ViewApplicationTemplate.ApplicationTemplateId = ViewApplicationTemplateElement.ApplicationTemplateId
		INNER JOIN ViewMechanismTemplateElementScoring ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElementScoring.MechanismTemplateElementId AND ViewApplicationWorkflowStep.StepTypeId = ViewMechanismTemplateElementScoring.StepTypeId
		WHERE ViewSessionPanel.SessionPanelId = @SessionPanelId
			AND NOT EXISTS (SELECT 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
							AND ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId);

		INSERT INTO [dbo].[ApplicationWorkflowStepElementContent]
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[ContentTextNoMarkup]
           ,[Abstain]
           ,[CritiqueRevised]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT CurElement.ApplicationWorkflowStepElementId, PrevContent.Score, PrevContent.ContentText, PrevContent.ContentTextNoMarkup, PrevContent.Abstain, 0,
		@UserId, @CurrentDateTime, @UserId, @CurrentDateTime
		FROM ViewApplicationWorkflowStepElement CurElement
		INNER JOIN ViewApplicationWorkflowStep CurStep ON CurElement.ApplicationWorkflowStepId = CurStep.ApplicationWorkflowStepId
		INNER JOIN ViewApplicationWorkflow ON CurStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationWorkflowStep PrevStep ON ViewApplicationWorkflow.ApplicationWorkflowId = PrevStep.ApplicationWorkflowId AND CurStep.StepOrder - 1 = PrevStep.StepOrder
		INNER JOIN ViewApplicationWorkflowStepElement PrevElement ON PrevStep.ApplicationWorkflowStepId = PrevElement.ApplicationWorkflowStepId AND CurElement.ApplicationTemplateElementId = PrevElement.ApplicationTemplateElementId
		INNER JOIN ViewApplicationWorkflowStepElementContent PrevContent ON PrevElement.ApplicationWorkflowStepElementId = PrevContent.ApplicationWorkflowStepElementId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
		WHERE ViewPanelApplication.SessionPanelId = @SessionPanelId AND ViewApplicationStage.ReviewStageId = 1 AND PrevStep.Resolution = 1 AND CurStep.StepTypeId IN (SELECT StepTypeId FROM @StepsAdded);
	END

		
	--If any step(s) are being removed
	IF EXISTS (SELECT 1 FROM @StepsRemoved)
	BEGIN
		--soft delete all data associated with the step being removed
		UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
		FROM ApplicationWorkflowStepElementContent
		INNER JOIN ViewApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId
		INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
		WHERE ViewPanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElementContent.DeletedFlag = 0 AND ViewApplicationWorkflowStep.StepTypeId IN (SELECT StepTypeId FROM @StepsRemoved);

		UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
		FROM ApplicationWorkflowStepElement 
		INNER JOIN ViewApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
		WHERE ViewPanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElement.DeletedFlag = 0 AND ViewApplicationWorkflowStep.StepTypeId IN (SELECT StepTypeId FROM @StepsRemoved);

		UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
		FROM ApplicationWorkflowStep
		INNER JOIN ViewApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
		WHERE ViewPanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStep.DeletedFlag = 0 AND ApplicationWorkflowStep.StepTypeId IN (SELECT StepTypeId FROM @StepsRemoved);

		UPDATE ApplicationStageStep SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
		FROM ApplicationStageStep
		INNER JOIN ViewApplicationStage ON ApplicationStageStep.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
		INNER JOIN ViewPanelStageStep ON ApplicationStageStep.PanelStageStepId = ViewPanelStageStep.PanelStageStepId
		WHERE ViewPanelApplication.SessionPanelId = @SessionPanelId AND ApplicationStageStep.DeletedFlag = 0 AND ViewPanelStageStep.StepTypeId IN (SELECT StepTypeId FROM @StepsRemoved);

		UPDATE PanelStageStep SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
		FROM PanelStageStep 
		INNER JOIN ViewPanelStage ON PanelStageStep.PanelStageId = ViewPanelStage.PanelStageId
		WHERE ViewPanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.DeletedFlag = 0 AND PanelStageStep.StepTypeId IN (SELECT StepTypeId FROM @StepsRemoved);
	END

	--Finally ensure phase dates are matching the new session
	UPDATE SessionPanel SET StartDate = COALESCE(ScoringPhase.StartDate, FinalPhase.StartDate), EndDate = COALESCE(ScoringPhase.EndDate, FinalPhase.EndDate)
	FROM SessionPanel
	LEFT JOIN SessionPhase ScoringPhase ON SessionPanel.MeetingSessionId = ScoringPhase.MeetingSessionId AND ScoringPhase.StepTypeId = 8 AND ScoringPhase.DeletedFlag = 0
	LEFT JOIN SessionPhase FinalPhase ON SessionPanel.MeetingSessionId = FinalPhase.MeetingSessionId AND FinalPhase.StepTypeId = 7 AND FinalPhase.DeletedFlag = 0
	WHERE SessionPanel.SessionPanelId = @SessionPanelId

	UPDATE PanelStageStep SET StartDate = SessionPhase.StartDate, EndDate = SessionPhase.EndDate, ReOpenDate = SessionPhase.ReopenDate, ReCloseDate = SessionPhase.CloseDate, ModifiedBy = @UserId, ModifiedDate = @CurrentDateTime
	FROM PanelStageStep
	INNER JOIN ViewPanelStage ON PanelStageStep.PanelStageId = ViewPanelStage.PanelStageId
	INNER JOIN ViewSessionPanel ON ViewPanelStage.SessionPanelId = ViewSessionPanel.SessionPanelId
	INNER JOIN SessionPhase ON PanelStageStep.StepTypeId = SessionPhase.StepTypeId
	WHERE PanelStageStep.DeletedFlag = 0 AND ViewSessionPanel.SessionPanelId = @SessionPanelId AND SessionPhase.MeetingSessionId = @MeetingSessionId AND SessionPhase.DeletedFlag = 0;

END
