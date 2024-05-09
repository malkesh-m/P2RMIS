CREATE FUNCTION [dbo].[udfFinalCritiquePhaseAverage]
(
	@PanelApplicationId int,
    @ClientElementId int
)
RETURNS table
AS RETURN


SELECT round(CAST(AVG(InnerQuery.Score) AS Decimal(18,5)), 1) AS AvgScore, round(round(CAST(STDEV(InnerQuery.Score) AS Decimal(18, 5)), 2),1) AS StDev
FROM
(
	SELECT     ViewApplicationWorkflowStepElementContent.Score, DENSE_RANK() OVER (PARTITION BY [ViewApplicationWorkflow].PanelUserAssignmentId ORDER BY [dbo].[ViewApplicationStage].[StageOrder] desc, [dbo].[ViewApplicationWorkflowStep].[StepOrder] desc) AS Ranking
FROM         ViewPanelApplication INNER JOIN
                      ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId INNER JOIN
                      ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewApplicationWorkflowStepElement ON 
                      ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
                      ViewApplicationWorkflowStepElementContent ON 
                      ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId INNER JOIN
                      ViewApplicationTemplateElement ON 
                      ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
                      ViewMechanismTemplateElement ON 
                      ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId INNER JOIN
                      ClientScoringScale ON ViewApplicationWorkflowStepElement.ClientScoringId = ClientScoringScale.ClientScoringId
WHERE         (ViewPanelApplication.PanelApplicationId = @PanelApplicationId) AND (ViewMechanismTemplateElement.ClientElementId = @ClientElementId) AND (ViewApplicationStage.ActiveFlag = 1)  AND ViewApplicationStage.ReviewStageId IN (1,2)
) InnerQuery
WHERE Ranking = 1


GO
GRANT SELECT
    ON OBJECT::[dbo].[udfFinalCritiquePhaseAverage] TO [NetSqlAzMan_Users]
    AS [dbo];

