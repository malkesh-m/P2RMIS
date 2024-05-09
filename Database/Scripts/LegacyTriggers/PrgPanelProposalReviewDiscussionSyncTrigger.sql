/*
CREATE TRIGGER [PrgPanelProposalReviewDiscussionSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion
FOR INSERT
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (Select * From inserted INNER JOIN
	[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON inserted.PRG_Part_Id = PanelUserAssignment.LegacyParticipantId INNER JOIN 
	[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
	[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId AND
	PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
	[$(DatabaseName)].dbo.ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = 1 INNER JOIN
	[$(DatabaseName)].dbo.ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
	[$(DatabaseName)].dbo.ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND
	ApplicationWorkflowStep.StepTypeId = 7
	WHERE ApplicationWorkflowStep.Active = 0)
	BEGIN
	--WHEN record is added, we need to ensure the final phase step is set to active for this application
	UPDATE [$(DatabaseName)].dbo.ApplicationWorkflowStep
	SET Active = 1, Resolution = 0, ResolutionDate = NULL
	FROM inserted INNER JOIN
	[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON inserted.PRG_Part_Id = PanelUserAssignment.LegacyParticipantId INNER JOIN 
	[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
	[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId AND
	PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
	[$(DatabaseName)].dbo.ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = 1 INNER JOIN
	[$(DatabaseName)].dbo.ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND
	ApplicationWorkflowStep.StepTypeId = 7
	WHERE ApplicationWorkflowStep.DeletedFlag = 0
	--Then pull content from previous step to final step
	INSERT INTO [$(DatabaseName)].dbo.ApplicationWorkflowStepElementContent
		 ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[Abstain]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, ApplicationWorkflowStepElementContent.Score, ApplicationWorkflowStepElementContent.ContentText,
		ApplicationWorkflowStepElementContent.Abstain, ApplicationWorkflowStepElementContent.CreatedBy, ApplicationWorkflowStepElementContent.CreatedDate,
		ApplicationWorkflowStepElementContent.ModifiedBy, ApplicationWorkflowStepElementContent.ModifiedDate
	FROM 
		inserted INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON inserted.PRG_Part_Id = PanelUserAssignment.LegacyParticipantId INNER JOIN 
		[$(DatabaseName)].dbo.ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId AND
		PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = 1 INNER JOIN
		[$(DatabaseName)].dbo.ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
		[$(DatabaseName)].dbo.ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND
		ApplicationWorkflowStep.StepTypeId = 7 INNER JOIN
		[$(DatabaseName)].dbo.ViewApplicationWorkflowStep ApplicationWorkflowStepPrev ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflowStepPrev.ApplicationWorkflowId AND
		ApplicationWorkflowStep.StepOrder - 1 = ApplicationWorkflowStepPrev.StepOrder INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElementPrev ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationWorkflowStepElementPrev.ApplicationTemplateElementId AND
		ApplicationWorkflowStepPrev.ApplicationWorkflowStepId = ApplicationWorkflowStepElementPrev.ApplicationWorkflowStepId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElementPrev.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
	GROUP BY ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, ApplicationWorkflowStepElementContent.Score, ApplicationWorkflowStepElementContent.ContentText,
		ApplicationWorkflowStepElementContent.Abstain, ApplicationWorkflowStepElementContent.CreatedBy, ApplicationWorkflowStepElementContent.CreatedDate,
		ApplicationWorkflowStepElementContent.ModifiedBy, ApplicationWorkflowStepElementContent.ModifiedDate
	END
END
*/