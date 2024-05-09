/*
	This stored procedure transfers an application to a new panel including assignments,
	expertise, and critiques by copying then soft deleting (as opposed to updating which is prone to issues with triggers)

	REVIEWERS MUST EXIST ON THE NEW PANEL IN ORDER FOR DATA TO TRANSFER
*/
CREATE PROCEDURE [dbo].[uspTransferApplicationToNewPanelWithData]
	@PanelApplicationId int,
	@NewPanelId int
AS
DECLARE
	@UserId int = 10,
	@NewPanelApplicationId int
	
	UPDATE PanelApplication
	SET SessionPanelId = @NewPanelId
	WHERE PanelApplicationId = @PanelApplicationId AND DeletedFlag = 0;

	--Update expertise, assignments, and critiques to point to reviewers assignment on new panel
	UPDATE PanelApplicationReviewerExpertise
	SET PanelUserAssignmentId = NewPanelUserAssignment.PanelUserAssignmentId
	FROM PanelApplicationReviewerExpertise INNER JOIN
	ViewPanelUserAssignment ON PanelApplicationReviewerExpertise.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId INNER JOIN
	ViewPanelUserAssignment NewPanelUserAssignment ON ViewPanelUserAssignment.UserId = NewPanelUserAssignment.UserId
	WHERE PanelApplicationReviewerExpertise.PanelApplicationId = @PanelApplicationId AND NewPanelUserAssignment.SessionPanelId = @NewPanelId 
		AND PanelApplicationReviewerExpertise.DeletedFlag = 0;

	UPDATE PanelApplicationReviewerAssignment
	SET PanelUserAssignmentId = NewPanelUserAssignment.PanelUserAssignmentId
	FROM PanelApplicationReviewerAssignment INNER JOIN
	ViewPanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId INNER JOIN
	ViewPanelUserAssignment NewPanelUserAssignment ON ViewPanelUserAssignment.UserId = NewPanelUserAssignment.UserId
	WHERE PanelApplicationReviewerAssignment.PanelApplicationId = @PanelApplicationId AND NewPanelUserAssignment.SessionPanelId = @NewPanelId 
		AND PanelApplicationReviewerAssignment.DeletedFlag = 0;

	UPDATE ApplicationWorkflow
	SET PanelUserAssignmentId = NewPanelUserAssignment.PanelUserAssignmentId
	FROM ApplicationWorkflow INNER JOIN
	ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId INNER JOIN
	ViewPanelUserAssignment NewPanelUserAssignment ON ViewPanelUserAssignment.UserId = NewPanelUserAssignment.UserId
	WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationId AND NewPanelUserAssignment.SessionPanelId = @NewPanelId 
		AND ApplicationWorkflow.DeletedFlag = 0;

	--Soft delete data belonging to reviewers not on the destination panel (this should remove in 1.0 as well)
	UPDATE PanelApplicationReviewerExpertise
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelApplicationId = @PanelApplicationId 
	AND NOT EXISTS (Select 'X' FROM ViewPanelUserAssignment WHERE ViewPanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerExpertise.PanelUserAssignmentId
					AND ViewPanelUserAssignment.SessionPanelId = @NewPanelId);
	
	UPDATE PanelApplicationReviewerAssignment
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelApplicationId = @PanelApplicationId 
	AND NOT EXISTS (Select 'X' FROM ViewPanelUserAssignment WHERE ViewPanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId
					AND ViewPanelUserAssignment.SessionPanelId = @NewPanelId);

	UPDATE ApplicationWorkflow
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = @UserId
	FROM ApplicationWorkflow INNER JOIN
		ViewApplicationStage ON ApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
	WHERE ApplicationWorkflow.DeletedFlag = 0 AND ViewApplicationStage.PanelApplicationId = @PanelApplicationId
	AND NOT EXISTS (Select 'X' FROM ViewPanelUserAssignment WHERE ViewPanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId
					AND ViewPanelUserAssignment.SessionPanelId = @NewPanelId);

	/* OLD CODE WANT TO KEEP AS IT MAY BE USABLE LATER
	--Add application to new panel
	INSERT INTO [dbo].[PanelApplication]
           ([SessionPanelId]
           ,[ApplicationId]
           ,[ReviewOrder]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT @NewPanelId
           ,[ApplicationId]
           ,[ReviewOrder]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
	FROM ViewPanelApplication
	WHERE ViewPanelApplication.PanelApplicationId = @PanelApplicationId;
	SELECT @NewPanelApplicationId = SCOPE_IDENTITY();
	INSERT INTO [dbo].[ApplicationStage]
           ([PanelApplicationId]
           ,[ReviewStageId]
           ,[StageOrder]
           ,[ActiveFlag]
           ,[AssignmentVisibilityFlag]
           ,[AssignmentReleaseDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	 SELECT @NewPanelApplicationId
           ,[ReviewStageId]
           ,[StageOrder]
           ,[ActiveFlag]
           ,[AssignmentVisibilityFlag]
           ,[AssignmentReleaseDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
	FROM ViewApplicationStage
	WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationId;
	INSERT INTO [dbo].[ApplicationReviewStatus]
           ([ApplicationId]
           ,[PanelApplicationId]
           ,[ReviewStatusId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
    SELECT [ApplicationId]
           ,@NewPanelApplicationId
           ,[ReviewStatusId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
	FROM ViewApplicationReviewStatus
	WHERE ViewApplicationReviewStatus.PanelApplicationId = @PanelApplicationId;
	INSERT INTO [dbo].[ApplicationTemplate]
           ([ApplicationId]
           ,[ApplicationStageId]
           ,[MechanismTemplateId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ViewApplicationTemplate.[ApplicationId]
           ,ApplicationStageNew.ApplicationStageId
           ,ViewApplicationTemplate.[MechanismTemplateId]
           ,ViewApplicationTemplate.[CreatedBy]
           ,ViewApplicationTemplate.[CreatedDate]
           ,ViewApplicationTemplate.[ModifiedBy]
           ,ViewApplicationTemplate.[ModifiedDate]
	FROM ViewApplicationTemplate INNER JOIN
	ViewApplicationStage ON ViewApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
	ViewApplicationStage ApplicationStageNew ON ViewApplicationStage.ReviewStageId = ApplicationStageNew.ReviewStageId
	WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationId AND ApplicationStageNew.PanelApplicationId = @NewPanelApplicationId;
	INSERT INTO [dbo].[ApplicationTemplateElement]
           ([ApplicationTemplateId]
           ,[MechanismTemplateElementId]
           ,[PanelApplicationReviewerAssignmentId]
           ,[DiscussionNoteFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ViewApplicationTemplateElement.[ApplicationTemplateId]
           ,ViewApplicationTemplateElement.[MechanismTemplateElementId]
           ,ViewApplicationTemplateElement.[PanelApplicationReviewerAssignmentId]
           ,ViewApplicationTemplateElement.[DiscussionNoteFlag]
           ,ViewApplicationTemplateElement.[CreatedBy]
           ,ViewApplicationTemplateElement.[CreatedDate]
           ,ViewApplicationTemplateElement.[ModifiedBy]
           ,ViewApplicationTemplateElement.[ModifiedDate]
	FROM ViewApplicationTemplateElement INNER JOIN
	ViewApplicationTemplate ON ViewApplicationTemplateElement.ApplicationTemplateId = ViewApplicationTemplate.ApplicationTemplateId INNER JOIN
	ViewApplicationStage ON ViewApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
	ViewApplicationStage ApplicationStageNew ON ViewApplicationStage.ReviewStageId = ApplicationStageNew.ReviewStageId
	WHERE ViewApplicationStage.PanelApplicationId = @PanelApplicationId AND ApplicationStageNew.PanelApplicationId = @NewPanelApplicationId;

	
	/*ApplicationStageStepDiscussion*/

	--Copy expertise to new panel and soft delete expertise from old panel
	/*PanelApplicationReviewerExpertise*/
	/*PanelApplicationReviewerCoiDetail*/
	--Copy assignments to new panel and soft delete old
	/*PanelApplicationReviewerAssignment*/
	--Copy critiques to new panel and soft delete old
	/*ApplicationWorkflow*/
	/*ApplicationWorkflowStep*/
	/*ApplicationWorkflowStepAssignment*/
	/*ApplicationWorkflowStepWorkLog*/
	/*ApplicationWorkflowStepElement*/
	/*ApplicationWorkflowStepElementContent*/
	--Remove remaining data from old panel
	/*First part or reverse order*/

	*/