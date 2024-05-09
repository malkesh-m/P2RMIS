DECLARE @LogNumber nvarchar (10) 

UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN dbo.Application ON dbo.ApplicationWorkflow.ApplicationId = dbo.Application.ApplicationId AND 
dbo.ApplicationTemplate.ApplicationId = dbo.Application.ApplicationId AND dbo.ViewPanelApplication.ApplicationId = dbo.Application.ApplicationId
WHERE (ViewApplicationStage.ReviewStageId = 3) AND (dbo.Application.LogNumber = @LogNumber))


UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepElementId IN
(SELECT     dbo.ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId
FROM         dbo.ApplicationWorkflowStepElement INNER JOIN
dbo.ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
dbo.ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
dbo.ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
dbo.ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
WHERE     (ViewApplicationStage.ReviewStageId = 3) AND (dbo.ViewApplication.LogNumber = @LogNumber))


UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(SELECT     dbo.ApplicationWorkflowStep.ApplicationWorkflowStepId
FROM         dbo.ApplicationWorkflowStep INNER JOIN
dbo.ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
dbo.ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
dbo.ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
WHERE     (ViewApplicationStage.ReviewStageId = 3) AND (dbo.ViewApplication.LogNumber = @LogNumber))


UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(SELECT     dbo.ApplicationWorkflowStep.ApplicationWorkflowStepId
FROM         dbo.ApplicationWorkflowStep INNER JOIN
dbo.ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
dbo.ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
dbo.ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
WHERE     (ViewApplicationStage.ReviewStageId = 3) AND (dbo.ViewApplication.LogNumber = @LogNumber))


UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowStepId IN
(SELECT     dbo.ApplicationWorkflowStep.ApplicationWorkflowStepId
FROM         dbo.ApplicationWorkflowStep INNER JOIN
dbo.ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
dbo.ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
dbo.ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
WHERE     (ViewApplicationStage.ReviewStageId = 3) AND (dbo.ViewApplication.LogNumber = @LogNumber))


UPDATE ApplicationWorkflow SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationWOrkflowId IN
(SELECT     dbo.ApplicationWorkflow.ApplicationWorkflowId, dbo.ViewApplication.LogNumber
FROM         dbo.ApplicationWorkflow INNER JOIN
dbo.ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
dbo.ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
WHERE     (ViewApplicationStage.ReviewStageId = 3) AND (dbo.ViewApplication.LogNumber = @LogNumber))


UPDATE ApplicationTemplateElement SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 WHERE DeletedFlag = 0 AND ApplicationTemplateElementId IN
(SELECT     dbo.ApplicationTemplateElement.ApplicationTemplateElementId
FROM         dbo.ApplicationTemplateElement INNER JOIN
dbo.ApplicationTemplate ON ApplicationTemplateElement.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
dbo.ViewApplicationStage ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
WHERE     (ViewApplicationStage.ReviewStageId = 3) AND (dbo.ViewApplication.LogNumber = @LogNumber))


UPDATE ApplicationTemplate SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10 
FROM         dbo.ApplicationTemplate INNER JOIN
dbo.ViewApplicationStage ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
dbo.ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
WHERE     (ViewApplicationStage.ReviewStageId = 3) AND (ApplicationTemplate.DeletedFlag = 0) AND (dbo.ViewApplication.LogNumber = @LogNumber)


