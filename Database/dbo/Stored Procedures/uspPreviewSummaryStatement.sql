/*
This stored procedure allows a summary workflow to be previewed as currently configured and rolled back as necessary
*/

CREATE PROCEDURE [dbo].[uspPreviewSummaryStatement]
	@PanelApplicationId int
AS
BEGIN
DECLARE @WorkflowId int = 2,
	@UserId int = 10,
	@StoredProcedureName varchar(50),
	@Sql varchar(100),
	@ApplicationWorkflowId int

	--First get the stored procedure name we need to execute to format the end output
	SELECT TOP(1) @StoredProcedureName = ClientSummaryTemplate.StoredProcedureName
	FROM [ViewPanelApplication] 
	INNER JOIN [ViewApplicationReviewStatus] ON [ViewPanelApplication].PanelApplicationId = [ViewApplicationReviewStatus].PanelApplicationId
	INNER JOIN [ViewApplication] ON [ViewPanelApplication].ApplicationId = [ViewApplication].ApplicationId
	INNER JOIN [ViewProgramMechanismSummaryStatement] ON [ViewApplication].ProgramMechanismId = [ViewProgramMechanismSummaryStatement].ProgramMechanismId AND [ViewApplicationReviewStatus].ReviewStatusId = [ViewProgramMechanismSummaryStatement].ReviewStatusId
	INNER JOIN [ClientSummaryTemplate] ON [ViewProgramMechanismSummaryStatement].ClientSummaryTemplateId = ClientSummaryTemplate.ClientSummaryTemplateId
	WHERE [ViewPanelApplication].PanelApplicationId = @PanelApplicationId
	--Now execute the load procedure in rollback mode
	BEGIN TRANSACTION previewSummary
	EXEC uspBeginApplicationSummaryWorkflow @PanelApplicationId, @UserId, @WorkflowId
	BEGIN
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		--Get the new ApplicationWorkflowId
		SELECT TOP(1) @ApplicationWorkflowId = [ViewApplicationWorkflow].ApplicationWorkflowId
		FROM [ViewPanelApplication]
		INNER JOIN [ViewApplicationStage] ON [ViewPanelApplication].PanelApplicationId = [ViewApplicationStage].PanelApplicationId
		INNER JOIN [ViewApplicationWorkflow] ON [ViewApplicationStage].ApplicationStageId = [ViewApplicationWorkflow].ApplicationStageId
		WHERE [ViewPanelApplication].PanelApplicationId = @PanelApplicationId AND [ViewApplicationStage].ReviewStageId = 3
		--Build and execute query to call stored procedure
		IF (@ApplicationWorkflowId > 0 AND LEN(@StoredProcedureName) > 1)
		BEGIN
			SET @Sql = 'EXEC ' + @StoredProcedureName + ' ' + CAST(@ApplicationWorkflowId AS varchar(20)) + ', ' + CAST(@PanelApplicationId AS varchar(20))
			EXECUTE(@Sql)
		END
	END
	--Finally roll back the transaction since we should already have the results
	ROLLBACK TRANSACTION previewSummary
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspPreviewSummaryStatement] TO [NetSqlAzMan_Users]
    AS [dbo];