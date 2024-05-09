DECLARE @PanelApplicationId int = 271589
----Soft Delete Meeting Critiques

UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId)

UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId)


UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId)


UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId)

UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId)

UPDATE ApplicationWorkflow SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowId IN
(Select ApplicationWorkflowId FROM ApplicationWorkflow
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId)

UPDATE ApplicationTemplateElement SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationTemplateElementId IN
(Select ApplicationTemplateElementId FROM ApplicationTemplateElement
INNER JOIN ApplicationTemplate ON ApplicationTemplateElement.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId)


UPDATE ApplicationTemplate SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 
FROM ApplicationTemplate 
INNER JOIN ViewApplicationStage ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
WHERE ApplicationTemplate.DeletedFlag = 0 AND ViewApplicationStage.ReviewStageId = 2 AND ViewPanelApplication.PanelApplicationId = @PanelApplicationId

---UPDATE ApplicationReviewStatus SET DeletedFlag = 0,  DeletedBy= NULL, 
                      ---DeletedDate = NULL

---Where DeletedDate > '10/20/2016'

UPDATE    dbo.ApplicationReviewStatus
SET              ReviewStatusId =2
WHERE     (PanelApplicationId = @PanelApplicationId) AND (DeletedFlag = 0)





