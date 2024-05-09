/*
	- This stored procedure is used for updating a criteria before or after assignment release.
	- MechanismTemplateElementId can be pre-meeting or meeting. If adjusting for both, stored proc should be ran for each ID
	- Parameter value should be left as NULL (default) if nothing is changing
	- Critiques are not automatically reset.  SROs should reset as necessary or stored procedure for reseting critiques in the db.
	- Can also be used to propagate changes for ScoringTemplate after assignment release.  Update MechanismScoringTemplate to the new template, then run this with ScoreFlag = 1 to apply to each criteria that needs updates to it's scoring scale
*/
CREATE PROCEDURE [dbo].[uspUpdateMechanismCriteria]
	@MechanismTemplateElementId int,
	@ScoreFlag bit = NULL,
	@TextFlag bit = NULL,
	@OverallFlag bit = NULL,
	@ClientElementId int = NULL,
	@ExecutingUserId int = 10
AS
BEGIN
	--Ensure any partial transactions are cleaned up and don't continue processing on error
	SET XACT_ABORT ON;

	DECLARE @CurrentDateTime datetime2(0)

	SELECT @CurrentDateTime = dbo.GetP2rmisDateTime();
	--If anything was updated off MechanismTemplateElement
	IF (@ScoreFlag IS NOT NULL OR @TextFlag IS NOT NULL OR @OverallFlag IS NOT NULL OR @ClientElementId IS NOT NULL)
	BEGIN
		UPDATE MechanismTemplateElement SET ScoreFlag = ISNULL(@ScoreFlag, ScoreFlag), TextFlag = ISNULL(@TextFlag, TextFlag), OverallFlag = ISNULL(@OverallFlag, OverallFlag), ClientElementId = ISNULL(@ClientElementId, ClientElementId), ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
		FROM MechanismTemplateElement
		WHERE MechanismTemplateElementId = @MechanismTemplateElementId AND DeletedFlag = 0;
		--If unscored ensure any associated scores are NULL. To preserve original we create an archival copy, then mark the live version score NULL. Any existing MechanismTemplateElementScoring records need soft deleted
		IF (@ScoreFlag = 0)
		BEGIN
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
			   ,[ModifiedDate]
			   ,[DeletedFlag]
			   ,[DeletedBy]
			   ,[DeletedDate])
			SELECT [ViewApplicationWorkflowStepElementContent].[ApplicationWorkflowStepElementId]
			   ,[ViewApplicationWorkflowStepElementContent].[Score]
			   ,[ViewApplicationWorkflowStepElementContent].[ContentText]
			   ,[ViewApplicationWorkflowStepElementContent].[ContentTextNoMarkup]
			   ,[ViewApplicationWorkflowStepElementContent].[Abstain]
			   ,[ViewApplicationWorkflowStepElementContent].[CritiqueRevised]
			   ,[ViewApplicationWorkflowStepElementContent].[CreatedBy]
			   ,[ViewApplicationWorkflowStepElementContent].[CreatedDate]
			   ,[ViewApplicationWorkflowStepElementContent].[ModifiedBy]
			   ,[ViewApplicationWorkflowStepElementContent].[ModifiedDate]
			   ,1
			   ,@ExecutingUserId
			   ,@CurrentDateTime
			FROM [ViewApplicationWorkflowStepElementContent]
				INNER JOIN [ViewApplicationWorkflowStepElement] ON [ViewApplicationWorkflowStepElementContent].ApplicationWorkflowStepElementId = [ViewApplicationWorkflowStepElement].ApplicationWorkflowStepElementId
				INNER JOIN [ViewApplicationTemplateElement] ON [ViewApplicationWorkflowStepElement].ApplicationTemplateElementId = [ViewApplicationTemplateElement].ApplicationTemplateElementId
			WHERE [ViewApplicationTemplateElement].MechanismTemplateElementId = @MechanismTemplateElementId;

			UPDATE [ApplicationWorkflowStepElementContent] SET Score = NULL, ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
			FROM [ApplicationWorkflowStepElementContent]
				INNER JOIN [ViewApplicationWorkflowStepElement] ON [ApplicationWorkflowStepElementContent].ApplicationWorkflowStepElementId = [ViewApplicationWorkflowStepElement].ApplicationWorkflowStepElementId
				INNER JOIN [ViewApplicationTemplateElement] ON [ViewApplicationWorkflowStepElement].ApplicationTemplateElementId = [ViewApplicationTemplateElement].ApplicationTemplateElementId
			WHERE [ViewApplicationTemplateElement].MechanismTemplateElementId = @MechanismTemplateElementId AND [ApplicationWorkflowStepElementContent].DeletedFlag = 0;

			UPDATE [MechanismTemplateElementScoring] SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @ExecutingUserId
			WHERE MechanismTemplateElementId = @MechanismTemplateElementId AND DeletedFlag = 0;

			UPDATE [ApplicationWorkflowStepElement] SET ClientScoringId = NULL, ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
			FROM [ApplicationWorkflowStepElement]
				INNER JOIN ViewApplicationTemplateElement ON [ApplicationWorkflowStepElement].ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId
			WHERE ViewApplicationTemplateElement.MechanismTemplateElementId = @MechanismTemplateElementId AND [ApplicationWorkflowStepElement].DeletedFlag = 0;
		END

		--If now scored ensure the client scoring scale exists and update the references to it
		IF (@ScoreFlag = 1)
		BEGIN
			IF NOT EXISTS (SELECT 'X' FROM ViewMechanismTemplateElementScoring WHERE MechanismTemplateElementId = @MechanismTemplateElementId)
				INSERT INTO [dbo].[MechanismTemplateElementScoring]
				   ([MechanismTemplateElementId]
				   ,[ClientScoringId]
				   ,[StepTypeId]
				   ,[CreatedBy]
				   ,[CreatedDate]
				   ,[ModifiedBy]
				   ,[ModifiedDate])
				 SELECT ViewMechanismTemplateElement.MechanismTemplateElementId, CASE WHEN ViewMechanismTemplateElement.OverallFlag = 1 THEN ScoringTemplatePhase.OverallClientScoringId ELSE ScoringTemplatePhase.CriteriaClientScoringId END,
					ScoringTemplatePhase.StepTypeId, @ExecutingUserId, @CurrentDateTime, @ExecutingUserId, @CurrentDateTime
				 FROM ViewMechanismTemplateElement 
				 INNER JOIN ViewMechanismTemplate ON ViewMechanismTemplateElement.MechanismTemplateId = ViewMechanismTemplate.MechanismTemplateId
				 INNER JOIN MechanismScoringTemplate ON ViewMechanismTemplate.ProgramMechanismId = MechanismScoringTemplate.ProgramMechanismId
				 INNER JOIN StepType ON ViewMechanismTemplate.ReviewStageId = StepType.ReviewStageId
				 INNER JOIN ScoringTemplate ON MechanismScoringTemplate.ScoringTemplateId = ScoringTemplate.ScoringTemplateId
				 INNER JOIN ScoringTemplatePhase ON ScoringTemplate.ScoringTemplateId = ScoringTemplatePhase.ScoringTemplateId AND StepType.StepTypeId = ScoringTemplatePhase.StepTypeId
				 WHERE ViewMechanismTemplateElement.MechanismTemplateElementId = @MechanismTemplateElementId AND MechanismScoringTemplate.DeletedFlag = 0;
			ELSE
				UPDATE MechanismTemplateElementScoring SET ClientScoringId = CASE WHEN ViewMechanismTemplateElement.OverallFlag = 1 THEN ScoringTemplatePhase.OverallClientScoringId ELSE ScoringTemplatePhase.CriteriaClientScoringId END, ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
				FROM MechanismTemplateElementScoring
				INNER JOIN ViewMechanismTemplateElement ON MechanismTemplateElementScoring.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId
				INNER JOIN ViewMechanismTemplate ON ViewMechanismTemplateElement.MechanismTemplateId = ViewMechanismTemplate.MechanismTemplateId
				INNER JOIN MechanismScoringTemplate ON ViewMechanismTemplate.ProgramMechanismId = MechanismScoringTemplate.ProgramMechanismId
				INNER JOIN ScoringTemplate ON MechanismScoringTemplate.ScoringTemplateId = ScoringTemplate.ScoringTemplateId
				INNER JOIN ScoringTemplatePhase ON ScoringTemplate.ScoringTemplateId = ScoringTemplatePhase.ScoringTemplateId AND MechanismTemplateElementScoring.StepTypeId = ScoringTemplatePhase.StepTypeId
				WHERE ViewMechanismTemplateElement.MechanismTemplateElementId = @MechanismTemplateElementId AND MechanismScoringTemplate.DeletedFlag = 0 AND MechanismTemplateElementScoring.DeletedFlag = 0;

			UPDATE [ApplicationWorkflowStepElement] SET ClientScoringId = ViewMechanismTemplateElementScoring.ClientScoringId, ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
			FROM [ApplicationWorkflowStepElement]
				INNER JOIN ViewApplicationWorkflowStep ON [ApplicationWorkflowStepElement].ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
				INNER JOIN ViewApplicationTemplateElement ON [ApplicationWorkflowStepElement].ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId
				INNER JOIN ViewMechanismTemplateElement ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId
				INNER JOIN ViewMechanismTemplateElementScoring ON ViewMechanismTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElementScoring.MechanismTemplateElementId AND ViewApplicationWorkflowStep.StepTypeId = ViewMechanismTemplateElementScoring.StepTypeId
			WHERE ViewApplicationTemplateElement.MechanismTemplateElementId = @MechanismTemplateElementId AND [ApplicationWorkflowStepElement].DeletedFlag = 0;
		END
		--If now no text, ensure any associated text provided is NULL.  To preserve original data, archive and update without text
		IF (@TextFlag = 0)
		BEGIN
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
			   ,[ModifiedDate]
			   ,[DeletedFlag]
			   ,[DeletedBy]
			   ,[DeletedDate])
			SELECT [ViewApplicationWorkflowStepElementContent].[ApplicationWorkflowStepElementId]
			   ,[ViewApplicationWorkflowStepElementContent].[Score]
			   ,[ViewApplicationWorkflowStepElementContent].[ContentText]
			   ,[ViewApplicationWorkflowStepElementContent].[ContentTextNoMarkup]
			   ,[ViewApplicationWorkflowStepElementContent].[Abstain]
			   ,[ViewApplicationWorkflowStepElementContent].[CritiqueRevised]
			   ,[ViewApplicationWorkflowStepElementContent].[CreatedBy]
			   ,[ViewApplicationWorkflowStepElementContent].[CreatedDate]
			   ,[ViewApplicationWorkflowStepElementContent].[ModifiedBy]
			   ,[ViewApplicationWorkflowStepElementContent].[ModifiedDate]
			   ,1
			   ,@ExecutingUserId
			   ,@CurrentDateTime
			FROM [ViewApplicationWorkflowStepElementContent]
				INNER JOIN [ViewApplicationWorkflowStepElement] ON [ViewApplicationWorkflowStepElementContent].ApplicationWorkflowStepElementId = [ViewApplicationWorkflowStepElement].ApplicationWorkflowStepElementId
				INNER JOIN [ViewApplicationTemplateElement] ON [ViewApplicationWorkflowStepElement].ApplicationTemplateElementId = [ViewApplicationTemplateElement].ApplicationTemplateElementId
			WHERE [ViewApplicationTemplateElement].MechanismTemplateElementId = @MechanismTemplateElementId;

			UPDATE [ApplicationWorkflowStepElementContent] SET ContentText = NULL, ContentTextNoMarkup = NULL, ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
			FROM [ApplicationWorkflowStepElementContent]
				INNER JOIN [ViewApplicationWorkflowStepElement] ON [ApplicationWorkflowStepElementContent].ApplicationWorkflowStepElementId = [ViewApplicationWorkflowStepElement].ApplicationWorkflowStepElementId
				INNER JOIN [ViewApplicationTemplateElement] ON [ViewApplicationWorkflowStepElement].ApplicationTemplateElementId = [ViewApplicationTemplateElement].ApplicationTemplateElementId
			WHERE [ViewApplicationTemplateElement].MechanismTemplateElementId = @MechanismTemplateElementId AND [ApplicationWorkflowStepElementContent].DeletedFlag = 0;
		END
		--If overall designation changed, ensure correct scoring scale is specified
		IF (@OverallFlag = 0 OR @OverallFlag = 1)
		BEGIN
				UPDATE MechanismTemplateElementScoring SET ClientScoringId = CASE WHEN ViewMechanismTemplateElement.OverallFlag = 1 THEN ScoringTemplatePhase.OverallClientScoringId ELSE ScoringTemplatePhase.CriteriaClientScoringId END, ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
				FROM MechanismTemplateElementScoring
				INNER JOIN ViewMechanismTemplateElement ON MechanismTemplateElementScoring.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId
				INNER JOIN ViewMechanismTemplate ON ViewMechanismTemplateElement.MechanismTemplateId = ViewMechanismTemplate.MechanismTemplateId
				INNER JOIN MechanismScoringTemplate ON ViewMechanismTemplate.ProgramMechanismId = MechanismScoringTemplate.ProgramMechanismId
				INNER JOIN ScoringTemplate ON MechanismScoringTemplate.ScoringTemplateId = ScoringTemplate.ScoringTemplateId
				INNER JOIN ScoringTemplatePhase ON ScoringTemplate.ScoringTemplateId = ScoringTemplatePhase.ScoringTemplateId AND MechanismTemplateElementScoring.StepTypeId = ScoringTemplatePhase.StepTypeId
				WHERE ViewMechanismTemplateElement.MechanismTemplateElementId = @MechanismTemplateElementId AND MechanismScoringTemplate.DeletedFlag = 0 AND MechanismTemplateElementScoring.DeletedFlag = 0;

				UPDATE [ApplicationWorkflowStepElement] SET ClientScoringId = ViewMechanismTemplateElementScoring.ClientScoringId, ModifiedBy = @ExecutingUserId, ModifiedDate = @CurrentDateTime
				FROM [ApplicationWorkflowStepElement]
					INNER JOIN ViewApplicationWorkflowStep ON [ApplicationWorkflowStepElement].ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
					INNER JOIN ViewApplicationTemplateElement ON [ApplicationWorkflowStepElement].ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId
					INNER JOIN ViewMechanismTemplateElement ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId
					INNER JOIN ViewMechanismTemplateElementScoring ON ViewMechanismTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElementScoring.MechanismTemplateElementId AND ViewApplicationWorkflowStep.StepTypeId = ViewMechanismTemplateElementScoring.StepTypeId
				WHERE ViewApplicationTemplateElement.MechanismTemplateElementId = @MechanismTemplateElementId AND [ApplicationWorkflowStepElement].DeletedFlag = 0;
		END

	END
	ELSE
		PRINT 'No changes made. You must supply at least one non-null optional parameter to make an update'
END