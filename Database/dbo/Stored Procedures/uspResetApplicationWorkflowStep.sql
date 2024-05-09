CREATE PROCEDURE [dbo].[uspResetApplicationWorkflowStep]
	@ApplicationWorkflowStepId int = 0,
	@UID int
AS
	DECLARE @IsLastStep BIT = 0 --Whether the step being reset is the last step in the workflow
	/*
	STEPS
	1: Mark resolution date for step requested as null
	2: Mark application workflow completed date as null (just in case it wasn't)
	3: Move application workflow step element content back to newly open step
	4: Add application workflow step work log entry
	*/
	UPDATE ApplicationWorkflowStep
	SET Resolution = 0, ResolutionDate = NULL, ModifiedBy = @UID, ModifiedDate = dbo.GetP2rmisDateTime()
	WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId

	UPDATE ApplicationWorkflow
	SET DateClosed = NULL, ModifiedBy = @UID, ModifiedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflow INNER JOIN
	ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
	WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId
	 
	 SELECT @IsLastStep = CASE WHEN ApplicationWorkflowStepNext.ApplicationWorkflowStepId IS NOT NULL THEN 0 ELSE 1 END
	 FROM ApplicationWorkflowStep LEFT OUTER JOIN
	 ApplicationWorkflowStep ApplicationWorkflowStepNext ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflowStepNext.ApplicationWorkflowId AND
	 ApplicationWorkflowStep.StepOrder + 1 = ApplicationWorkflowStepNext.StepOrder AND ApplicationWorkflowStepNext.Active = 1
	 WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId

	 IF @IsLastStep = 0
	 BEGIN
		 UPDATE ApplicationWorkflowStepElementContent
		 SET Score = ApplicationWorkflowStepElementContent2.Score, ContentText = ApplicationWorkflowStepElementContent2.ContentText,
		 ModifiedBy = ApplicationWorkflowStepElementContent2.ModifiedBy, ModifiedDate = ApplicationWorkflowStepElementContent2.ModifiedDate,
		 Abstain = CASE WHEN ApplicationWorkflowStepElementContent2.Score IS NULL AND ApplicationWorkflowStepElementContent2.ContentText = 'n/a' THEN 1 ELSE 0 END
		 FROM ApplicationWorkflowStep INNER JOIN
		 ApplicationWorkflowStep ApplicationWorkflowStep2 ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflowStep2.ApplicationWorkflowId AND
		 ApplicationWorkflowStep.StepOrder + 1 = ApplicationWorkflowStep2.StepOrder INNER JOIN
		 ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
		 ApplicationWorkflowStepElement ApplicationWorkflowStepElement2 ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement2.ApplicationTemplateElementId AND
		 ApplicationWorkflowStep2.ApplicationWorkflowStepId = ApplicationWorkflowStepElement2.ApplicationWorkflowStepId INNER JOIN
		 ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId INNER JOIN
		 ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent2 ON ApplicationWorkflowStepElement2.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent2.ApplicationWorkflowStepElementId
		 WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId AND
		 ApplicationWorkflowStepElementContent2.DeletedFlag = 0
		 --Soft delete the next step contents
		 UPDATE ApplicationWorkflowStepElementContent2
		 SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = @UID
		 FROM ApplicationWorkflowStep INNER JOIN
		 ApplicationWorkflowStep ApplicationWorkflowStep2 ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflowStep2.ApplicationWorkflowId AND
		 ApplicationWorkflowStep.StepOrder + 1 = ApplicationWorkflowStep2.StepOrder INNER JOIN
		 ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
		 ApplicationWorkflowStepElement ApplicationWorkflowStepElement2 ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement2.ApplicationTemplateElementId AND
		 ApplicationWorkflowStep2.ApplicationWorkflowStepId = ApplicationWorkflowStepElement2.ApplicationWorkflowStepId INNER JOIN
		 ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId INNER JOIN
		 ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent2 ON ApplicationWorkflowStepElement2.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent2.ApplicationWorkflowStepElementId
		 WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId
		
		--Update check-in date for next step work log
		UPDATE ApplicationWorkflowStepWorkLog 
		SET CheckInDate = dbo.GetP2rmisDateTime(), ModifiedBy = @UID, ModifiedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowStep INNER JOIN
		ApplicationWorkflowStep ApplicationWorkflowStepNext ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWOrkflowStepNext.ApplicationWorkflowId AND
		ApplicationWorkflowStep.StepOrder + 1 = ApplicationWorkflowStepNext.StepOrder INNER JOIN
		ApplicationWorkflowStepWorkLog ON ApplicationWorkflowStepNext.ApplicationWorkflowStepId = ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId
		WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId AND
		ApplicationWorkflowStepWorkLog.CheckInDate IS NULL
	
	END
	--Add check-out date for current step

	INSERT INTO ApplicationWorkflowStepWorkLog
	([ApplicationWorkflowStepId]
           ,[UserId]
		   ,[CheckInUserId]
           ,[CheckOutDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationWorkflowStepAssignment.UserId, ApplicationWorkflowStepAssignment.UserId, dbo.GetP2rmisDateTime(),
		@UID, dbo.GetP2rmisDateTime(), @UID, dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowStep INNER JOIN
		ApplicationWorkflowStepAssignment ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId
		WHERE ApplicationWorkflowStep.ApplicationWorkflowStepId = @ApplicationWorkflowStepId

GO

	GRANT EXECUTE
    ON OBJECT::[dbo].[uspResetApplicationWorkflowStep] TO [NetSqlAzMan_Users], [web-p2rmis]
    AS [dbo];