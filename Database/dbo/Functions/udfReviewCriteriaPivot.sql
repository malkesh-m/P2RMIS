CREATE FUNCTION [dbo].[udfReviewCriteriaPivot]
(
	@ApplicationWorkflowStepId int,
	@OverallEval int,
	@SortOrder int = NULL
)
RETURNS TABLE
AS
RETURN
	SELECT ApplicationWorkflowStepElementId, MechanismTemplateElementId, Score
	FROM (
	SELECT ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, OverallFlag, CASE WHEN ViewMechanismTemplateElement.OverallFlag = 1 THEN NULL ELSE DENSE_RANK() OVER (Partition By ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId Order By ViewMechanismTemplateElement.OverallFlag, ViewMechanismTemplateElement.SortOrder, ViewMechanismTemplateElement.MechanismTemplateElementId) END AS Position, ViewMechanismTemplateElement.MechanismTemplateElementId, ViewApplicationWorkflowStepElementContent.Score
	FROM ViewApplicationWorkflowStepElement
	JOIN ViewApplicationTemplateElement ON ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId
	JOIN ViewMechanismTemplateElement ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId
	LEFT JOIN ViewApplicationWorkflowStepElementContent ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
	WHERE ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = @ApplicationWorkflowStepId AND ViewMechanismTemplateElement.ScoreFlag = 1
	) AS InnerQ
	WHERE OverallFlag = @OverallEval AND (@SortOrder IS NULL OR Position = @SortOrder)
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfReviewCriteriaPivot] TO [NetSqlAzMan_Users]
    AS [dbo];
