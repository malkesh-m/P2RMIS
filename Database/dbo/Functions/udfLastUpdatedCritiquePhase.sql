/*
Gets the last updated critique information for a given reviewer and criteria
*/
CREATE FUNCTION [dbo].[udfLastUpdatedCritiquePhase]
(
	@PanelUserAssignmentId int,
	@PanelApplicationId int,
    @ClientElementId int
)
RETURNS table
AS RETURN
SELECT TOP(1) [dbo].[ViewApplicationWorkflowStepElement].[ApplicationWorkflowStepElementId], [dbo].[ViewApplicationWorkflowStepElementContent].[ContentText],
		[dbo].[ViewApplicationWorkflowStepElementContent].[Score], [ViewApplicationWorkflowStepElementContent].Abstain, [ViewApplicationWorkflowStepElementContent].CreatedBy, [ViewApplicationWorkflowStepElementContent].CreatedDate,
		[ViewApplicationWorkflowStepElementContent].[ModifiedBy], [ViewApplicationWorkflowStepElementContent].ModifiedDate
FROM [dbo].[ViewPanelApplication] INNER JOIN 
	[dbo].[ViewApplicationStage] ON [dbo].[ViewPanelApplication].[PanelApplicationId] = [dbo].[ViewApplicationStage].[PanelApplicationId] INNER JOIN
	[dbo].[ViewApplicationWorkflow] ON [dbo].[ViewApplicationStage].[ApplicationStageId] = [dbo].[ViewApplicationWorkflow].[ApplicationStageId] INNER JOIN
	[dbo].[ViewApplicationWorkflowStep] ON [dbo].[ViewApplicationWorkflow].[ApplicationWorkflowId] = [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] INNER JOIN
	[dbo].[ViewApplicationWorkflowStepElement] ON [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowStepId] = [dbo].[ViewApplicationWorkflowStepElement].[ApplicationWorkflowStepId] INNER JOIN
	[dbo].[ViewApplicationWorkflowStepElementContent] ON [dbo].[ViewApplicationWorkflowStepElement].[ApplicationWorkflowStepElementId] = [dbo].[ViewApplicationWorkflowStepElementContent].[ApplicationWorkflowStepElementId] INNER JOIN
	[dbo].[ViewApplicationTemplateElement] ON [dbo].[ViewApplicationWorkflowStepElement].[ApplicationTemplateElementId] = [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateElementId] INNER JOIN
	[dbo].[ViewMechanismTemplateElement] ON [dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId]
WHERE [dbo].[ViewApplicationStage].[ReviewStageId] <> 3 AND [dbo].[ViewApplicationStage].[ActiveFlag] = 1 AND  [dbo].[ViewPanelApplication].[PanelApplicationId] = @PanelApplicationId AND
	[dbo].[ViewApplicationWorkflow].[PanelUserAssignmentId] = @PanelUserAssignmentId AND [dbo].[ViewMechanismTemplateElement].[ClientElementId] = @ClientElementId	
ORDER BY [dbo].[ViewApplicationStage].[StageOrder] desc, [dbo].[ViewApplicationWorkflowStep].[StepOrder] desc
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfLastUpdatedCritiquePhase] TO [NetSqlAzMan_Users]
    AS [dbo];