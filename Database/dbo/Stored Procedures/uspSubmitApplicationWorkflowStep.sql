CREATE PROCEDURE [dbo].[uspSubmitApplicationWorkflowStep]
	@ApplicationWorkflowStepId int = 0, 
	@UID int
AS
	/*
	STEPS
	1: Mark resolution date for step requested
	2: Check if final step
	3: If final step, mark application workflow complete
	4: Add application workflow step work log entry
	5: Else, push content to next step
	*/
	DECLARE @IsLastStep BIT = 0 --Whether the step being submitted is the last step in the workflow
	UPDATE ApplicationWorkflowStep
	SET Resolution = 1, ResolutionDate = dbo.GetP2rmisDateTime(), ModifiedBy = @UID, ModifiedDate = dbo.GetP2rmisDateTime()
	WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId
	 
	 SELECT @IsLastStep = CASE WHEN ApplicationWorkflowStepNext.ApplicationWorkflowStepId IS NOT NULL THEN 0 ELSE 1 END
	 FROM ApplicationWorkflowStep LEFT OUTER JOIN
	 ApplicationWorkflowStep ApplicationWorkflowStepNext ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflowStepNext.ApplicationWorkflowId AND
	 ApplicationWorkflowStep.StepOrder + 1 = ApplicationWorkflowStepNext.StepOrder AND ApplicationWorkflowStepNext.Active = 1
	 WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId

	 --Add check-in date for step being submitted
	UPDATE ApplicationWorkflowStepWorkLog 
	SET CheckInDate = dbo.GetP2rmisDateTime(), ModifiedBy = @UID, ModifiedDate = dbo.GetP2rmisDateTime()
	WHERE ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId = @ApplicationWorkflowStepId AND
	ApplicationWorkflowStepWorkLog.CheckInDate IS NULL

	 IF @IsLastStep = 0
	 BEGIN
		 --Push contents to next step
		 INSERT INTO ApplicationWorkflowStepElementContent
		 ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[Abstain]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		 SELECT ApplicationWorkflowStepElement2.ApplicationWorkflowStepElementId, ApplicationWorkflowStepElementContentCurrent.Score, ApplicationWorkflowStepElementContentCurrent.ContentText, 
		CASE WHEN ApplicationWorkflowStepElementContentCurrent.Score IS NULL AND ApplicationWorkflowStepElementContentCurrent.ContentText = 'n/a' THEN 1 ELSE 0 END,
		 ApplicationWorkflowStepElementContentCurrent.CreatedBy, dbo.GetP2rmisDateTime(), ApplicationWorkflowStepElementContentCurrent.ModifiedBy, dbo.GetP2rmisDateTime()
		 FROM ApplicationWorkflowStep INNER JOIN
		 ApplicationWorkflowStep ApplicationWorkflowStep2 ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflowStep2.ApplicationWorkflowId AND
		 ApplicationWorkflowStep.StepOrder + 1 = ApplicationWorkflowStep2.StepOrder INNER JOIN
		 ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
		 ApplicationWorkflowStepElement ApplicationWorkflowStepElement2 ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement2.ApplicationTemplateElementId AND
		 ApplicationWorkflowStep2.ApplicationWorkflowStepId = ApplicationWorkflowStepElement2.ApplicationWorkflowStepId INNER JOIN
		 ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContentCurrent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContentCurrent.ApplicationWorkflowStepElementId
		 WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId AND ApplicationWorkflowStepElementContentCurrent.DeletedFlag = 0
		 --Create work log entry for next step
		 INSERT INTO ApplicationWorkflowStepWorkLog
		([ApplicationWorkflowStepId]
           ,[UserId]
		   ,[CheckInUserId]
           ,[CheckOutDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT NextStep.ApplicationWorkflowStepId, ApplicationWorkflowStepAssignment.UserId, ApplicationWorkflowStepAssignment.UserId, dbo.GetP2rmisDateTime(),
		@UID, dbo.GetP2rmisDateTime(), @UID, dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowStep INNER JOIN
		ApplicationWorkflowStep NextStep ON ApplicationWorkflowStep.ApplicationWorkflowId = NextStep.ApplicationWorkflowId AND ApplicationWorkflowStep.StepOrder + 1 = NextStep.StepOrder INNER JOIN
		ApplicationWorkflowStepAssignment ON NextStep.ApplicationWorkflowStepId = ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId
		WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId
	END
	ELSE
	BEGIN
		UPDATE ApplicationWorkflow
		SET DateClosed = dbo.GetP2rmisDateTime(), ModifiedBy = @UID, ModifiedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflow INNER JOIN
		ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
		WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId
	END

	GO

	GRANT EXECUTE
    ON OBJECT::[dbo].[uspSubmitApplicationWorkflowStep] TO [NetSqlAzMan_Users], [web-p2rmis]
    AS [dbo];