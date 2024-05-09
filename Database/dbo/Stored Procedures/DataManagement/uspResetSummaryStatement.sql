CREATE PROC [dbo].[uspResetSummaryStatement] @LOGNUMBER VARCHAR(20) AS
BEGIN


UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER)
UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER)
UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER)
UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER)
UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER)
UPDATE ApplicationWorkflow SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowId IN
(Select ApplicationWorkflowId FROM ApplicationWorkflow
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER)
UPDATE ApplicationTemplateElement SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationTemplateElementId IN
(Select ApplicationTemplateElementId FROM ApplicationTemplateElement
INNER JOIN ApplicationTemplate ON ApplicationTemplateElement.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER)
UPDATE ApplicationTemplate SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10 
FROM ApplicationTemplate 
INNER JOIN ViewApplicationStage ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ApplicationTemplate.DeletedFlag = 0 AND ViewApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER
UPDATE ApplicationStage SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10
FROM ApplicationStage
INNER JOIN ViewPanelApplication ON ApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ApplicationStage.DeletedFlag = 0 AND ApplicationStage.ReviewStageId = 3 AND ViewApplication.LogNumber = @LOGNUMBER

END
