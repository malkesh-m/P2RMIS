CREATE PROCEDURE [dbo].[uspRemovePanelApplication]
	@PanelApplicationId int,
	@UserId int
AS
BEGIN
--ensure entire transaction rolls back in case of error
SET XACT_ABORT ON;
DECLARE @CurrentDateTime DATETIME2(0) = dbo.GetP2rmisDateTime(),
		@ApplicationWorkflowId int
	
	--Remove all data related to panel application and children in reverse order
	--First looping through all related ApplicationWorkflow information
	DECLARE ApplicationWorkflowCursor CURSOR FOR
		SELECT ViewApplicationWorkflow.ApplicationWorkflowId
		FROM ViewApplicationWorkflow
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationId
	OPEN ApplicationWorkflowCursor;
	FETCH NEXT FROM ApplicationWorkflowCursor INTO @ApplicationWorkflowId;			
	WHILE @@FETCH_STATUS=0
	BEGIN
		EXEC uspRemoveApplicationWorkflow @ApplicationWorkflowId, @UserId;
		FETCH NEXT FROM ApplicationWorkflowCursor INTO @ApplicationWorkflowId;	
	END
	CLOSE ApplicationWorkflowCursor;
	DEALLOCATE ApplicationWorkflowCursor;
	--Proceed with the rest of PanelApplicationData
	UPDATE assdc
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationStageStepDiscussionComment assdc
	INNER JOIN ViewApplicationStageStepDiscussion assd ON assdc.ApplicationStageStepDiscussionId = assd.ApplicationStageStepDiscussionId
	INNER JOIN ViewApplicationStageStep ass ON assd.ApplicationStageStepId = ass.ApplicationStageStepId
	INNER JOIN ViewApplicationStage [as] ON ass.ApplicationStageId = [as].ApplicationStageId
	WHERE [as].PanelApplicationId = @PanelApplicationId AND assdc.DeletedFlag = 0;
	UPDATE assd
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationStageStepDiscussion assd
	INNER JOIN ViewApplicationStageStep ass ON assd.ApplicationStageStepId = ass.ApplicationStageStepId
	INNER JOIN ViewApplicationStage [as] ON ass.ApplicationStageId = [as].ApplicationStageId
	WHERE [as].PanelApplicationId = @PanelApplicationId AND assd.DeletedFlag = 0;
	UPDATE ass
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationStageStep ass
	INNER JOIN ViewApplicationStage [as] ON ass.ApplicationStageId = [as].ApplicationStageId
	WHERE [as].PanelApplicationId = @PanelApplicationId AND ass.DeletedFlag = 0;
	UPDATE ate
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationTemplateElement ate
	INNER JOIN ViewApplicationTemplate [at] on ate.ApplicationTemplateId = [at].ApplicationTemplateId
	INNER JOIN ViewApplicationStage [as] ON [at].ApplicationStageId = [as].ApplicationStageId
	WHERE [as].PanelApplicationId = @PanelApplicationId AND ate.DeletedFlag = 0;
	UPDATE [at]
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationTemplate [at]
	INNER JOIN ViewApplicationStage [as] ON [at].ApplicationStageId = [as].ApplicationStageId
	WHERE [as].PanelApplicationId = @PanelApplicationId AND [at].DeletedFlag = 0;
	UPDATE ApplicationStage
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE ApplicationStage.PanelApplicationId = @PanelApplicationId AND ApplicationStage.DeletedFlag = 0;
	UPDATE PanelApplicationSummary
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE PanelApplicationSummary.PanelApplicationId = @PanelApplicationId AND DeletedFlag = 0;
	UPDATE ApplicationReviewStatus
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE ApplicationReviewStatus.PanelApplicationId = @PanelApplicationId AND DeletedFlag = 0;
	UPDATE paracd
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM PanelApplicationReviewerCoiDetail paracd 
	INNER JOIN ViewPanelApplicationReviewerExpertise pare on paracd.PanelApplicationReviewerExpertiseId = pare.PanelApplicationReviewerExpertiseId
	WHERE pare.PanelApplicationId = @PanelApplicationId AND paracd.DeletedFlag = 0;
	UPDATE PanelApplicationReviewerExpertise
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE PanelApplicationId = @PanelApplicationId AND DeletedFlag = 0;
	UPDATE PanelApplicationReviewerAssignment
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE PanelApplicationId = @PanelApplicationId AND DeletedFlag = 0;
	UPDATE PanelApplication
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE PanelApplicationId = @PanelApplicationId;
END
GO
	GRANT EXECUTE
    ON OBJECT::[dbo].[uspRemovePanelApplication] TO [NetSqlAzMan_Users]
    AS [dbo];