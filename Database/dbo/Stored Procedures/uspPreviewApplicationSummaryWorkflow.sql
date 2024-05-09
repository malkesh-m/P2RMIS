-- =============================================
-- Author:		Craig Henson
-- Create date: 9/24/2014
-- Description:	This stored procedure populates the necessary data for an application to begin a summary workflow however does not insert into physical tables.
-- IMPORTANT: This stored procedure should closely mirror the editor query. Changes here should be made there and vice versa. 
-- =============================================
CREATE PROCEDURE [dbo].[uspPreviewApplicationSummaryWorkflow] 
	-- For now this is initiated using the legacy "Log Number". Eventually this will be replaced by ApplicationId
	@PanelApplicationId int
AS
BEGIN
	DECLARE @WorkflowId int = 2,
	@UserId int = 10
	BEGIN TRANSACTION previewSummary
	EXEC uspBeginApplicationSummaryWorkflow @PanelApplicationId, @UserId, @WorkflowId
	BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		SELECT ElementName = clientelement.ElementDescription,
                    ElementInstructions = Mte.InstructionText,
                    ElementSortOrder = Mte.SortOrder,
                    ElementScoreFlag = Mte.ScoreFlag,
                    ElementScoreType = Css.ScoreType,
                    ElementOverallFlag = Mte.OverallFlag,
                    ElementTextFlag = Mte.TextFlag,
                    ElementContentText = Awsec.ContentText,
                    ElementContentTextNoMarkup = Awsec.ContentTextNoMarkup,
                    ElementContentScore = Awsec.Score,
                    ElementContentAverageScore = AvgScores.AvgScore,
                    ElementContentAdjectivalLabel = Cssa.ScoreLabel,
                    ElementScaleHighValue = Css.HighValue,
                    ElementScaleLowValue = Css.LowValue,
                    ElementScoreStandardDeviation =  AvgScores.StDev,
                    ReviewerAssignmentOrder = CASE WHEN srd.CustomOrder IS NULL THEN 99 ELSE srd.CustomOrder END,
                    ReviewerAssignmentType = srd.DisplayName,
                    DiscussionNoteFlag = Ate.DiscussionNoteFlag,
                    IsOverview = CASE WHEN clientelement.ElementTypeId = 2 THEN 1 ELSE 0 END
         FROM [ViewApplication] app INNER JOIN
         ViewPanelApplication panapp ON app.ApplicationId = panapp.ApplicationId INNER JOIN
         ViewSessionPanel panel ON panapp.SessionPanelId = panel.SessionPanelId INNER JOIN
         ViewApplicationStage appstage ON panapp.PanelApplicationId = appstage.PanelApplicationId AND appstage.ReviewStageId = 3 INNER JOIN
         ViewApplicationWorkflow appworkflow ON appstage.ApplicationStageId = appworkflow.ApplicationStageId INNER JOIN
         ViewApplicationWorkflowStep appworkflowstep ON appworkflow.ApplicationWorkflowId = appworkflowstep.ApplicationWorkflowId INNER JOIN
         ViewApplicationWorkflowStepElement awse ON appworkflowstep.ApplicationWorkflowStepId = awse.ApplicationWorkflowStepId LEFT OUTER JOIN
         ViewApplicationWorkflowStepElementContent awsec ON awse.ApplicationWorkflowStepElementId = awsec.ApplicationWorkflowStepElementId INNER JOIN
         ViewApplicationTemplateElement ate ON awse.ApplicationTemplateElementId = ate.ApplicationTemplateElementId INNER JOIN
         ViewMechanismTemplateElement mte ON ate.MechanismTemplateElementId = mte.MechanismTemplateElementId INNER JOIN
         ClientElement clientelement ON mte.ClientElementId = clientelement.ClientElementId LEFT OUTER JOIN
         ViewPanelApplicationReviewerAssignment para ON ate.PanelApplicationReviewerAssignmentId = para.PanelApplicationReviewerAssignmentId LEFT OUTER JOIN
         ViewPanelUserAssignment pua ON para.PanelUserAssignmentId = pua.PanelUserAssignmentId OUTER APPLY
		 udfGetSummaryReviewerDescription(app.ProgramMechanismId, pua.ClientParticipantTypeId, pua.ClientRoleId, para.SortOrder) srd OUTER APPLY
		 udfLastUpdatedCritiquePhaseAverage(panapp.PanelApplicationId, clientelement.ClientElementId) AS AvgScores LEFT OUTER JOIN
         ViewMechanismTemplateElementScoring mtes ON mte.MechanismTemplateElementId = mtes.MechanismTemplateElementId AND appworkflowstep.StepTypeId = mtes.StepTypeId LEFT OUTER JOIN
         ClientScoringScale css ON mtes.ClientScoringId = css.ClientScoringId LEFT OUTER JOIN
         ClientScoringScaleAdjectival cssa ON css.ClientScoringId = cssa.ClientScoringId AND awsec.Score = cssa.NumericEquivalent
	WHERE panapp.PanelApplicationId = @PanelApplicationId AND appworkflowstep.StepOrder = 1
	ORDER BY mte.SortOrder, ate.DiscussionNoteFlag, srd.CustomOrder
	END
	ROLLBACK TRANSACTION previewSummary
	
	
END
GO


GRANT EXECUTE
    ON OBJECT::[dbo].[uspPreviewApplicationSummaryWorkflow] TO [NetSqlAzMan_Users]
    AS [dbo];
