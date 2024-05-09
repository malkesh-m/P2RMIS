/*
Disabled - No use case for transferring critique information from 1.0 to 2.0 in real time

CREATE TRIGGER [PrgCriteriaEvalSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Criteria_Eval
FOR INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON
	--This handles pre-meeting critiques only
	--DELETE will be handled with PRG_Critiques sync trigger since criteria eval must be deleted in batch (soft delete all children)
	--UPDATE
	IF EXISTS (Select * FROM inserted INNER JOIN
	[$(P2RMIS)].dbo.PRG_Criteria_Eval PRG_Criteria_Eval ON inserted.Criteria_Eval_ID = PRG_Criteria_Eval.Criteria_Eval_ID INNER JOIN
	[$(P2RMIS)].dbo.PRG_Critiques PRG_Critiques ON inserted.Critique_ID = PRG_Critiques.Critique_ID INNER JOIN
	[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PRG_Critiques.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
	[$(DatabaseName)].dbo.PanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId AND
	ApplicationStage.ReviewStageId = 1 AND PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND
	CASE inserted.Scoring_Phase WHEN 'initial' THEN 5 WHEN 'revised' THEN 6 WHEN 'meeting' THEN 7 END = ApplicationWorkflowStep.StepTypeId INNER JOIN
	[$(DatabaseName)].dbo.MechanismTemplateElement MechanismTemplateElement ON inserted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationTemplate ApplicationTemplate ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationTemplateElement ApplicationTemplateElement ON MechanismTemplateElement.MechanismTemplateElementId = ApplicationTemplateElement.MechanismTemplateElementId AND
	ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId AND
	ApplicationTemplateElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement.ApplicationTemplateElementId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	WHERE ApplicationWorkflowStepElementContent.DeletedFlag = 0)

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepElementContent]
		SET [Score] = inserted.Criteria_Score
		  ,[ContentText] = CAST(PRG_Criteria_Eval.Criteria_Txt AS nvarchar(max))
		  ,[Abstain] = CASE WHEN inserted.Criteria_Score IS NULL AND CAST(PRG_Criteria_Eval.Criteria_Txt AS nvarchar(max)) = 'n/a' THEN 1 ELSE 0 END
		  ,[ModifiedBy] = VUN.UserId
		  ,[ModifiedDate] = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(P2RMIS)].dbo.PRG_Criteria_Eval PRG_Criteria_Eval ON inserted.Criteria_Eval_ID = PRG_Criteria_Eval.Criteria_Eval_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_Critiques PRG_Critiques ON inserted.Critique_ID = PRG_Critiques.Critique_ID INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PRG_Critiques.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.PanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId AND
		ApplicationStage.ReviewStageId = 1 AND PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND
		CASE inserted.Scoring_Phase WHEN 'initial' THEN 5 WHEN 'revised' THEN 6 WHEN 'meeting' THEN 7 END = ApplicationWorkflowStep.StepTypeId INNER JOIN
		[$(DatabaseName)].dbo.MechanismTemplateElement MechanismTemplateElement ON inserted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationTemplate ApplicationTemplate ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationTemplateElement ApplicationTemplateElement ON MechanismTemplateElement.MechanismTemplateElementId = ApplicationTemplateElement.MechanismTemplateElementId AND
		ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId AND
		ApplicationTemplateElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement.ApplicationTemplateElementId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE ApplicationWorkflowStepElementContent.DeletedFlag = 0
	ELSE
		INSERT INTO [$(DatabaseName)].[dbo].[ApplicationWorkflowStepElementContent]
			   ([ApplicationWorkflowStepElementId]
			   ,[Score]
			   ,[ContentText]
			   ,[Abstain]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		SELECT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, inserted.Criteria_Score, CAST(PRG_Criteria_Eval.Criteria_Txt AS nvarchar(max)),
		CASE WHEN inserted.Criteria_Score IS NULL AND CAST(PRG_Criteria_Eval.Criteria_Txt AS nvarchar(max)) = 'n/a' THEN 1 ELSE 0 END, VUN.UserId,
		inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(P2RMIS)].dbo.PRG_Criteria_Eval PRG_Criteria_Eval ON inserted.Criteria_Eval_ID = PRG_Criteria_Eval.Criteria_Eval_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_Critiques PRG_Critiques ON inserted.Critique_ID = PRG_Critiques.Critique_ID INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PRG_Critiques.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.PanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId AND
		ApplicationStage.ReviewStageId = 1 AND PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND
		CASE inserted.Scoring_Phase WHEN 'initial' THEN 5 WHEN 'revised' THEN 6 WHEN 'meeting' THEN 7 END = ApplicationWorkflowStep.StepTypeId INNER JOIN
		[$(DatabaseName)].dbo.MechanismTemplateElement MechanismTemplateElement ON inserted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationTemplate ApplicationTemplate ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationTemplateElement ApplicationTemplateElement ON MechanismTemplateElement.MechanismTemplateElementId = ApplicationTemplateElement.MechanismTemplateElementId AND
		ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId AND
		ApplicationTemplateElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement.ApplicationTemplateElementId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE ApplicationWorkflowStep.DeletedFlag = 0
END

*/