/*
Gets the last updated critique information for a given application and criteria
*/
CREATE FUNCTION [dbo].[udfLastUpdatedCritiquePhaseAverage]
(
	@PanelApplicationId int,
    @ClientElementId int
)
RETURNS table
AS RETURN
SELECT round(CAST(AVG(InnerQuery.Score) AS Decimal(18,5)), 1) AS AvgScore, round(round(CAST(STDEV(InnerQuery.Score) AS Decimal(18, 5)), 2),1) AS StDev
FROM
(
	SELECT 	ViewApplicationWorkflowStepElementContent.Score, DENSE_RANK() OVER (PARTITION BY [ViewApplicationWorkflow].PanelUserAssignmentId ORDER BY [dbo].[ViewApplicationStage].[StageOrder] desc, [dbo].[ViewApplicationWorkflowStep].[StepOrder] desc) AS Ranking
	FROM [dbo].[ViewPanelApplication] INNER JOIN 
	[dbo].[ViewApplicationStage] ON [dbo].[ViewPanelApplication].[PanelApplicationId] = [dbo].[ViewApplicationStage].[PanelApplicationId] INNER JOIN
	[dbo].[ViewApplicationWorkflow] ON [dbo].[ViewApplicationStage].[ApplicationStageId] = [dbo].[ViewApplicationWorkflow].[ApplicationStageId] INNER JOIN
	[dbo].[ViewApplicationWorkflowStep] ON [dbo].[ViewApplicationWorkflow].[ApplicationWorkflowId] = [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] INNER JOIN
	[dbo].[ViewApplicationWorkflowStepElement] ON [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowStepId] = [dbo].[ViewApplicationWorkflowStepElement].[ApplicationWorkflowStepId] INNER JOIN
	[dbo].[ViewApplicationWorkflowStepElementContent] ON [dbo].[ViewApplicationWorkflowStepElement].[ApplicationWorkflowStepElementId] = [dbo].[ViewApplicationWorkflowStepElementContent].[ApplicationWorkflowStepElementId] INNER JOIN
	[dbo].[ViewApplicationTemplateElement] ON [dbo].[ViewApplicationWorkflowStepElement].[ApplicationTemplateElementId] = [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateElementId] INNER JOIN
	[dbo].[ViewMechanismTemplateElement] ON [dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId]
	WHERE [dbo].[ViewApplicationStage].[ReviewStageId] <> 3 AND [dbo].[ViewApplicationStage].[ActiveFlag] = 1 AND  [dbo].[ViewPanelApplication].[PanelApplicationId] = @PanelApplicationId AND
	[dbo].[ViewMechanismTemplateElement].[ClientElementId] = @ClientElementId	
) InnerQuery
WHERE Ranking = 1
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfLastUpdatedCritiquePhaseAverage] TO [NetSqlAzMan_Users]
    AS [dbo];