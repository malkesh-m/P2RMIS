/*
Removes a phase from the panel
*/
CREATE PROCEDURE [dbo].[uspRemovePhaseFromPanel]
	@SessionPanelId int,
	@StepTypeId int
AS
BEGIN
DECLARE @PreliminaryId int = 5,
@RevisedId int = 6,
@OnlineScoringId int = 7,
@PreStageId int = 1,
@MeetingStageId int = 2

IF (@StepTypeId NOT IN (@PreliminaryId, @RevisedId, @OnlineScoringId))
	RETURN 'StepTypeId specified not supported. Please try again.'
ELSE
BEGIN
	--Soft delete the ApplicationStageStep
	UPDATE ApplicationStageStep SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationStageStep INNER JOIN
	ApplicationStage ON ApplicationStageStep.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	PanelStageStep ON ApplicationStageStep.PanelStageStepId = PanelStageStep.PanelStageStepId INNER JOIN
	PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE PanelApplication.SessionPanelId = @SessionPanelId AND PanelStageStep.StepTypeId = @StepTypeId AND ApplicationStageStep.DeletedFlag = 0
	--Soft delete from PanelStageStep
	UPDATE PanelStageStep SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
	FROM PanelStageStep INNER JOIN
	PanelStage ON PanelStageStep.PanelStageId = PanelStage.PanelStageId
	WHERE PanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.StepTypeId = @StepTypeId AND PanelStageStep.DeletedFlag = 0
	--Update step order as necessary
	UPDATE PanelStageStep SET StepOrder = SubQuery.StepRank
	FROM PanelStageStep INNER JOIN
	(Select PanelStageStep2.PanelStageStepId, DENSE_RANK() OVER (Partition By PanelStageStep2.PanelStageId Order By PanelStageStep2.StepOrder) AS StepRank
	FROM PanelStageStep PanelStageStep2 INNER JOIN
		PanelStage PanelStage2 ON PanelStageStep2.PanelStageId = PanelStage2.PanelStageId
	WHERE PanelStageStep2.DeletedFlag = 0)  SubQuery ON PanelStageStep.PanelStageStepId = SubQuery.PanelStageStepId INNER JOIN
	PanelStage ON PanelStageStep.PanelStageId = PanelStage.PanelStageId
	WHERE PanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.StepTypeId = @StepTypeId AND PanelStageStep.DeletedFlag = 0
	
	--Soft delete all associated ApplicationWorkflowStep data
	UPDATE ApplicationWorkflowStepElementContent SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStepElementContent INNER JOIN
		ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
		ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
		ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationWorkflowStep.StepTypeId = @StepTypeId AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElementContent.DeletedFlag = 0
	UPDATE ApplicationWorkflowStepElement SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStepElement INNER JOIN
		ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
		ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationWorkflowStep.StepTypeId = @StepTypeId AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElement.DeletedFlag = 0
	UPDATE ApplicationWorkflowStepAssignment SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStepAssignment INNER JOIN
		ApplicationWorkflowStep ON ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
		ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationWorkflowStep.StepTypeId = @StepTypeId AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepAssignment.DeletedFlag = 0
	UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStep INNER JOIN
	ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStep.StepTypeId = @StepTypeId AND
	ApplicationWorkflowStep.DeletedFlag = 0
	--Update step orders
	UPDATE ApplicationWorkflowStep SET StepOrder = SubQuery.StepRank
	FROM ApplicationWorkflowStep INNER JOIN
		(Select ApplicationWorkflowStep2.ApplicationWorkflowStepId, DENSE_RANK() OVER (Partition By ApplicationWorkflowStep2.ApplicationWorkflowId Order By ApplicationWorkflowStep2.StepOrder) AS StepRank
		FROM ApplicationWorkflowStep ApplicationWorkflowStep2 INNER JOIN
			ApplicationWorkflow ApplicationWorkflow2 ON ApplicationWorkflowStep2.ApplicationWorkflowId = ApplicationWorkflow2.ApplicationWorkflowId INNER JOIN
			ApplicationStage ApplicationStage2 ON ApplicationWorkflow2.ApplicationStageId = ApplicationStage2.ApplicationStageId INNER JOIN
			PanelApplication PanelApplication2 ON ApplicationStage2.PanelApplicationId = PanelApplication2.PanelApplicationId
		WHERE PanelApplication2.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStep2.StepTypeId = @StepTypeId AND
			ApplicationWorkflowStep2.DeletedFlag = 0) 
	SubQuery ON ApplicationWorkflowStep.ApplicationWorkflowStepId = SubQuery.ApplicationWorkflowStepId INNER JOIN
	ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStep.StepTypeId = @StepTypeId AND
	ApplicationWorkflowStep.DeletedFlag = 0
END
END
