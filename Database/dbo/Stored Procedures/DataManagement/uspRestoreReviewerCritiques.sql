-- =============================================
-- Author:		Craig Henson
-- Create date: 9/3/2019
-- Description:	Restores critiques (pre-meeting and meeting) from a reviewer who was unassigned from an application
-- Remarks: Certain situations will still require manually entering critique data such as unassigned criteria if the reviewer was unassigned when scoring
-- =============================================
CREATE PROCEDURE [dbo].[uspRestoreReviewerCritiques]
	-- Add the parameters for the stored procedure here
	@PreMeetingWorkflowIdToDelete int, --Required
	@PreMeetingWorkflowIdToRestore int, --Required
	@MeetingWorkflowIdToDelete int = NULL, --Optional, only specify if a deleted meeting workflow needs restored (reviewer unassigned after scoring)
	@MeetingWorkflowIdToRestore int = NULL, --Optional, only specify if a deleted meeting workflow needs restored (reviewer unassigned after scoring)
	@AuthorUserId int = 10 --Optional, defaults to webmaster for modified/deleted
AS
BEGIN
	DECLARE @CurrentDate datetime2(0),
	@PreDeletedDateTime datetime2(0),
	@MeetingDeletedDateTime datetime2(0),
	@PanelApplicationId int,
	@PanelUserAssignmentId int
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT @CurrentDate = dbo.GetP2rmisDateTime();
	
	SELECT TOP(1) @PreDeletedDateTime = DeletedDate
	FROM ApplicationWorkflow
	WHERE ApplicationWorkflow.ApplicationWorkflowId = @PreMeetingWorkflowIdToRestore;

	SELECT TOP(1) @MeetingDeletedDateTime = DeletedDate
	FROM ApplicationWorkflow
	WHERE ApplicationWorkflow.ApplicationWorkflowId = @MeetingWorkflowIdToRestore;

	SELECT TOP(1) @PanelApplicationId = PanelApplicationId, @PanelUserAssignmentId = PanelUserAssignmentId
	FROM ViewApplicationStage 
	INNER JOIN	ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId
	WHERE ViewApplicationWorkflow.ApplicationWorkflowId = @PreMeetingWorkflowIdToDelete
    --We are essentially swapping the old workflow and all child items in place of the new workflow in order to restore the previous state
	--First remove all new workflow data
	UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 1, DeletedDate = @CurrentDate, DeletedBy = @AuthorUserId
	WHERE ApplicationWorkflowStepElementId IN
	(SELECT ApplicationWorkflowStepElementId FROM ViewApplicationWorkflowStepElement
	INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId IN (@PreMeetingWorkflowIdToDelete, @MeetingWorkflowIdToDelete)) AND DeletedFlag = 0;

	UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 1, DeletedDate = @CurrentDate, DeletedBy = @AuthorUserId
	WHERE ApplicationWorkflowStepElementId IN
	(SELECT ApplicationWorkflowStepElementId FROM ViewApplicationWorkflowStepElement
	INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId IN (@PreMeetingWorkflowIdToDelete, @MeetingWorkflowIdToDelete)) AND DeletedFlag = 0;

	UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 1, DeletedDate = @CurrentDate, DeletedBy = @AuthorUserId
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId IN (@PreMeetingWorkflowIdToDelete, @MeetingWorkflowIdToDelete)) AND DeletedFlag = 0;

	UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 1, DeletedDate = @CurrentDate, DeletedBy = @AuthorUserId
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId IN (@PreMeetingWorkflowIdToDelete, @MeetingWorkflowIdToDelete)) AND DeletedFlag = 0;

	UPDATE ApplicationWorkflowStep SET DeletedFlag = 1, DeletedDate = @CurrentDate, DeletedBy = @AuthorUserId
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId IN (@PreMeetingWorkflowIdToDelete, @MeetingWorkflowIdToDelete)) AND DeletedFlag = 0;

	UPDATE ApplicationWorkflow SET DeletedFlag = 1, DeletedDate = @CurrentDate, DeletedBy = @AuthorUserId
	WHERE ApplicationWorkflowId IN (@PreMeetingWorkflowIdToDelete, @MeetingWorkflowIdToDelete) AND DeletedFlag = 0;

	--Now restore the workflow, we match up on deleteddate so as to not restore anything extra (critiques that were reset, etc.)
	UPDATE ApplicationWorkflow SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowId IN (@PreMeetingWorkflowIdToRestore, @MeetingWorkflowIdToRestore) AND DeletedFlag = 1;

	UPDATE ApplicationWorkflowStep SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ApplicationWorkflowStep
	WHERE ApplicationWorkflowStep.ApplicationWorkflowId = @PreMeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @PreDeletedDateTime;

	UPDATE ApplicationWorkflowStep SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ApplicationWorkflowStep
	WHERE ApplicationWorkflowStep.ApplicationWorkflowId = @MeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @MeetingDeletedDateTime;

	UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @PreMeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @PreDeletedDateTime;

	UPDATE ApplicationWorkflowStepAssignment SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @MeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @MeetingDeletedDateTime;
	
	UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @PreMeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @PreDeletedDateTime;

	UPDATE ApplicationWorkflowStepWorkLog SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @MeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @MeetingDeletedDateTime;

	UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @PreMeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @PreDeletedDateTime;

	UPDATE ApplicationWorkflowStepElement SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepId IN
	(SELECT ApplicationWorkflowStepId FROM ViewApplicationWorkflowStep
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @MeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @MeetingDeletedDateTime;

	UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepElementId IN
	(SELECT ApplicationWorkflowStepElementId FROM ViewApplicationWorkflowStepElement
	INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @PreMeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @PreDeletedDateTime;

	UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 0, DeletedDate = NULL, DeletedBy = NULL
	WHERE ApplicationWorkflowStepElementId IN
	(SELECT ApplicationWorkflowStepElementId FROM ViewApplicationWorkflowStepElement
	INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
	WHERE ViewApplicationWorkflowStep.ApplicationWorkflowId = @MeetingWorkflowIdToRestore) AND DeletedFlag = 1 AND DeletedDate = @MeetingDeletedDateTime;
	--Finally, if the application was already activated while the reviewer was already COI/unassigned, mark their scores as abstained or push the critique text forward
	IF (@MeetingWorkflowIdToRestore IS NULL AND EXISTS (SELECT 'X' FROM ViewApplicationStage WHERE ReviewStageId = 2 AND ActiveFlag = 1 AND PanelApplicationId = @PanelApplicationId))
	BEGIN
		--If workflow does not exist at all (they were a COI when activated) then create it, and mark abstained
		IF NOT EXISTS (SELECT 'X' FROM ViewApplicationWorkflow INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId WHERE ViewApplicationWorkflow.PanelUserAssignmentId = @PanelUserAssignmentId AND ViewApplicationStage.PanelApplicationId = @PanelApplicationId AND ViewApplicationStage.ReviewStageId = 2)
		BEGIN
			EXEC dbo.uspBeginScoringWorkflow @PanelApplicationId, @AuthorUserId;
			UPDATE ApplicationWorkflowStepElementContent SET Abstain = 1, Score = NULL
			WHERE ApplicationWorkflowStepElementId IN
			(SELECT ApplicationWorkflowStepElementId
			FROM ViewApplicationWorkflowStepElement
			INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
			INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
			INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
			WHERE ViewApplicationWorkflow.PanelUserAssignmentId = @PanelUserAssignmentId AND ViewApplicationStage.PanelApplicationId = @PanelApplicationId AND ViewApplicationStage.ReviewStageId = 2)
		END
		--Otherwise the workflow exists (they scored) and we just need to copy critique text from previous phase
		ELSE
		BEGIN
			UPDATE ApplicationWorkflowStepElementContent
			SET ContentText = ViewApplicationWorkflowStepElementContent.ContentText
			FROM ViewApplicationWorkflowStepElement INNER JOIN --Current
			ViewApplicationTemplateElement AS ApplicationTemplateElementCurrent ON ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElementCurrent.ApplicationTemplateElementId INNER JOIN --Current
			ViewMechanismTemplateElement AS MechanismTemplateElementCurrent ON ApplicationTemplateElementCurrent.MechanismTemplateElementId = MechanismTemplateElementCurrent.MechanismTemplateElementId INNER JOIN
			ViewMechanismTemplate AS MechanismTemplateCurrent ON MechanismTemplateElementCurrent.MechanismTemplateId = MechanismTemplateCurrent.MechanismTemplateId INNER JOIN
			ViewApplicationWorkflowStep AS ApplicationWorkflowStepCurrent ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStepCurrent.ApplicationWorkflowStepId INNER JOIN --Current
			ViewApplicationWorkflow AS ApplicationWorkflowCurrent ON ApplicationWorkflowStepCurrent.ApplicationWorkflowId = ApplicationWorkflowCurrent.ApplicationWorkflowId INNER JOIN --Current
			ViewApplicationStage AS ApplicationStageCurrent ON ApplicationWorkflowCurrent.ApplicationStageId = ApplicationStageCurrent.ApplicationStageId INNER JOIN --Current
			ViewApplicationStage AS ApplicationStagePrevious ON ApplicationStageCurrent.PanelApplicationId = ApplicationStagePrevious.PanelApplicationId AND 1 = ApplicationStagePrevious.ReviewStageId INNER JOIN --Previous
			ViewApplicationWorkflow AS ApplicationWorkflowPrevious ON ApplicationStagePrevious.ApplicationStageId = ApplicationWorkflowPrevious.ApplicationStageId AND ApplicationWorkflowCurrent.PanelUserAssignmentId = ApplicationWorkflowPrevious.PanelUserAssignmentId CROSS APPLY --Previous
			udfApplicationWorkflowLastStep(ApplicationWorkflowPrevious.ApplicationWorkflowId) AS ApplicationWorkflowStepPrevious INNER JOIN --Previous
			ViewMechanismTemplate AS MechanismTemplatePrevious ON MechanismTemplateCurrent.ProgramMechanismId = MechanismTemplatePrevious.ProgramMechanismId AND ApplicationStagePrevious.ReviewStageId = MechanismTemplatePrevious.ReviewStageId INNER JOIN --Previous
			ViewMechanismTemplateElement AS MechanismTemplateElementPrevious ON MechanismTemplatePrevious.MechanismTemplateId = MechanismTemplateElementPrevious.MechanismTemplateId AND MechanismTemplateElementCurrent.ClientElementId = MechanismTemplateElementPrevious.ClientElementId INNER JOIN
			ViewApplicationTemplate AS ApplicationTemplatePrevious ON ApplicationStagePrevious.ApplicationStageId = ApplicationTemplatePrevious.ApplicationStageId INNER JOIN --Previous
			ViewApplicationTemplateElement AS ApplicationTemplateElementPrevious ON MechanismTemplateElementPrevious.MechanismTemplateElementId = ApplicationTemplateElementPrevious.MechanismTemplateElementId AND ApplicationTemplatePrevious.ApplicationTemplateId = ApplicationTemplateElementPrevious.ApplicationTemplateId INNER JOIN --Previous
			ViewApplicationWorkflowStepElement AS ApplicationWorkflowStepElementPrevious ON ApplicationWorkflowStepPrevious.ApplicationWorkflowStepId = ApplicationWorkflowStepElementPrevious.ApplicationWorkflowStepId AND ApplicationTemplateElementPrevious.ApplicationTemplateElementId = ApplicationWorkflowStepElementPrevious.ApplicationTemplateElementId INNER JOIN --Previous
			ViewApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElementPrevious.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId INNER JOIN --Previous
			ApplicationWorkflowStepElementContent ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
			WHERE ApplicationWorkflowPrevious.ApplicationWorkflowId = @PreMeetingWorkflowIdToRestore AND ApplicationWorkflowStepElementContent.DeletedFlag = 0
		END
	END
END
