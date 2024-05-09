CREATE FUNCTION [dbo].[udfApplicationWorkflowActiveStep]
(
	@ApplicationWorkflowId int
)
RETURNS TABLE
AS
RETURN
SELECT ApplicationWorkflowId, ApplicationStageId, WorkflowId, ApplicationWorkflowStepId, StepName, StepOrder
FROM
(
SELECT     ApplicationWorkflow.ApplicationWorkflowId, ApplicationWorkflow.ApplicationStageId, ApplicationWorkflow.WorkflowId, ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationWorkflowStep.StepName, ApplicationWorkflowStep.StepOrder, DENSE_RANK() OVER (PARTITION BY ApplicationStage.PanelApplicationId, WorkflowId ORDER BY StepOrder) AS Ranking
FROM         ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
					  ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId
WHERE     (ApplicationWorkflowStep.Active = 1) AND (ApplicationWorkflowStep.Resolution = 0) AND (ApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowId)
) Sub
Where Ranking = 1
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfApplicationWorkflowActiveStep] TO [NetSqlAzMan_Users]
    AS [dbo];