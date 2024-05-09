
CREATE TRIGGER [PrgCritiquesSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRG_Critiques]
FOR DELETE
AS
BEGIN
	SET NOCOUNT ON

		--Soft delete all data under the ApplicationWorkflow for the critique that was deleted
		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepElementContent] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON deleted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
		PanelApplicationReviewerAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
		WHERE ApplicationWorkflowStepElementContent.DeletedFlag = 0
		
		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepElement] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON deleted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
		PanelApplicationReviewerAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId
		WHERE ApplicationWorkflowStepElement.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepWorkLog] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON deleted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
		PanelApplicationReviewerAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepWorkLog ApplicationWorkflowStepWorkLog ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId
		WHERE ApplicationWorkflowStepWorkLog.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepAssignment] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON deleted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
		PanelApplicationReviewerAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepAssignment ApplicationWorkflowStepAssignment ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId 
		WHERE ApplicationWorkflowStepAssignment.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStep] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON deleted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
		PanelApplicationReviewerAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
		WHERE ApplicationWorkflowStep.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflow] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON deleted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
		PanelApplicationReviewerAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId
		WHERE ApplicationWorkflow.DeletedFlag = 0
	
END
