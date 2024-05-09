
DECLARE @PanelApplicationId int = 271589
----Soft Delete Revised Critiques

UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(SELECT     dbo.ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId
FROM         dbo.ApplicationWorkflowStepElement INNER JOIN
dbo.ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
dbo.ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
dbo.ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
dbo.ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE (ViewApplicationStage.ReviewStageId = 1 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId AND dbo.ApplicationWorkflowStep.StepTypeId = 6))



UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE (ViewApplicationStage.ReviewStageId = 1) AND (ViewPanelApplication.PanelApplicationId = @PanelApplicationId) AND (dbo.ApplicationWorkflowStep.StepTypeId = 6))


UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE (ViewApplicationStage.ReviewStageId = 1) AND (ViewPanelApplication.PanelApplicationId = @PanelApplicationId) AND (dbo.ApplicationWorkflowStep.StepTypeId = 6))


UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE (ViewApplicationStage.ReviewStageId = 1) AND (ViewPanelApplication.PanelApplicationId = @PanelApplicationId) AND (dbo.ApplicationWorkflowStep.StepTypeId = 6))


/*

UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select dbo.ViewApplicationWorkflowStep.ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
INNER JOIN viewApplicationWorkflow ON viewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN viewApplicationTemplate ON viewApplicationWorkflow.ApplicationTemplateId = ViewApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 1 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId AND ViewApplicationWorkflowStep.StepTypeId = 6)

*/


