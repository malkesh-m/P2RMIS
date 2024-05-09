-- =============================================
-- Author:		Craig Henson
-- Create date: 1/30/2020
-- Description:	This stored procedure is used to recover an application that was removed from a panel including related data.  See  parameter comments
--	for more information on how to properly call to avoid restoring too much or not enough associated data.
CREATE PROCEDURE [dbo].[uspRestorePanelApplicationRemovedFromPanel]
	@PanelApplicationIdToRestore int, --ID currently soft deleted that needs restored
	@DateTimeToRestoreFrom datetime2(0), /*Date time the record was approximately removed from the panel. 
	Give some padding (few hours prior at least) from the date reviewers were unassigned, this is to ensure we don't restore assignments and other data associated with the application 
	that was purposefully unassigned or deleted prior to the application being removed from the panel. E.g. restoring reviewer SRO had intentionally unassigned days before  */
	@PanelApplicationIdToRemove int NULL --Optional, if app had been re-assigned this will remove the new assignment when restoring previous
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		UPDATE PanelApplication SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
		WHERE PanelApplicationId = @PanelApplicationIdToRemove;

		UPDATE PanelApplication SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE PanelApplicationReviewerAssignment SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE PanelApplicationReviewerExpertise SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE UserApplicationComment SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE PanelApplicationSummary SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE PanelApplicationReviewerCoiDetail SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM PanelApplicationReviewerCoiDetail
		INNER JOIN ViewPanelApplicationReviewerExpertise ON ViewPanelApplicationReviewerExpertise.PanelApplicationReviewerExpertiseId = PanelApplicationReviewerCoiDetail.PanelApplicationReviewerExpertiseId
		WHERE ViewPanelApplicationReviewerExpertise.PanelApplicationId = @PanelApplicationIdToRestore AND PanelApplicationReviewerCoiDetail.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationStage SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationTemplate SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationTemplate
		INNER JOIN ViewApplicationStage ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationTemplate.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationTemplateElement SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationTemplateElement
		INNER JOIN ViewApplicationTemplate ON ApplicationTemplateElement.ApplicationTemplateId = ViewApplicationTemplate.ApplicationTemplateId
		INNER JOIN ViewApplicationStage ON ViewApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationTemplateElement.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationReviewStatus SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		WHERE PanelApplicationId = @PanelApplicationIdToRestore AND DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationReviewStatus SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
		WHERE PanelApplicationId = @PanelApplicationIdToRemove;

		UPDATE ApplicationWorkflow SET DeletedFlag = 0, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflow
		INNER JOIN ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationWorkflow.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationWorkflowSummaryStatement SET DeletedFlag = 0, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
		FROM ApplicationWorkflowSummaryStatement
		INNER JOIN ViewApplicationWorkflow ON ApplicationWorkflowSummaryStatement.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationWorkflowSummaryStatement.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationWorkflowStep SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationWorkflowStep
		INNER JOIN ViewApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationWorkflowStep.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationWorkflowStepAssignment
		INNER JOIN ViewApplicationWorkflowStep ON ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationWorkflowStepAssignment.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationWorkflowStepWorkLog
		INNER JOIN ViewApplicationWorkflowStep ON ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationWorkflowStepWorkLog.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationWorkflowStepElement
		INNER JOIN ViewApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationWorkflowStepElement.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationWorkflowStepElementContent
		INNER JOIN ViewApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId
		INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
		INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
		INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationWorkflowStepElementContent.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationStageStep SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationStageStep
		INNER JOIN ViewApplicationStage ON ApplicationStageStep.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationStageStep.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationStageStepDiscussion SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationStageStepDiscussion
		INNER JOIN ViewApplicationStageStep ON ApplicationStageStepDiscussion.ApplicationStageStepId = ViewApplicationStageStep.ApplicationStageStepId
		INNER JOIN ViewApplicationStage ON ViewApplicationStageStep.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationStageStepDiscussion.DeletedDate >= @DateTimeToRestoreFrom;

		UPDATE ApplicationStageStepDiscussionComment SET DeletedFlag = 0, DeletedBy = NULL, DeletedDate = NULL
		FROM ApplicationStageStepDiscussionComment
		INNER JOIN ViewApplicationStageStepDiscussion ON ApplicationStageStepDiscussionComment.ApplicationStageStepDiscussionId = ViewApplicationStageStepDiscussion.ApplicationStageStepDiscussionId
		INNER JOIN ViewApplicationStageStep ON ViewApplicationStageStepDiscussion.ApplicationStageStepId = ViewApplicationStageStep.ApplicationStageStepId
		INNER JOIN ViewApplicationStage ON ViewApplicationStageStep.ApplicationStageId = ViewApplicationStage.ApplicationStageId
		WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationIdToRestore AND ApplicationStageStepDiscussionComment.DeletedDate >= @DateTimeToRestoreFrom;
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH
END