CREATE FUNCTION [dbo].[udfApplicationWorkflowLastStep]
(
	@ApplicationWorkflowId int
)
RETURNS TABLE
AS
RETURN
SELECT ApplicationWorkflowId, ApplicationStageId, WorkflowId, ApplicationWorkflowStepId, StepName, StepOrder, Resolution
FROM
(
SELECT     ApplicationWorkflow.ApplicationWorkflowId, ApplicationWorkflow.ApplicationStageId, ApplicationWorkflow.WorkflowId, ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationWorkflowStep.StepName, 
ApplicationWorkflowStep.StepOrder, DENSE_RANK() OVER (PARTITION BY PanelApplicationId, WorkflowId ORDER BY StepOrder DESC) AS Ranking, ApplicationWorkflowStep.Resolution
FROM         ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
					  ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId
WHERE     (ApplicationWorkflowStep.Active = 1) AND (ApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowId) AND (ApplicationWorkflowStep.DeletedFlag = 0)
) Sub
Where Ranking = 1
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfApplicationWorkflowLastStep] TO [NetSqlAzMan_Users]
    AS [dbo];

