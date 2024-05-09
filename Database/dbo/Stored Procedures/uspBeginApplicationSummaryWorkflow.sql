-- =============================================
-- Author:		Craig Henson
-- Create date: 7/30/2014
-- Description:	This stored procedure populates the necessary data for an application to begin a summary workflow.
-- IMPORTANT: This stored procedure should closely mirror uspPreviewApplicationSummaryWorkflow. Changes here should be made there and vice versa. 
-- =============================================
CREATE PROCEDURE [dbo].[uspBeginApplicationSummaryWorkflow] 
	-- For now this is initiated using the legacy "Log Number". Eventually this will be replaced by ApplicationId
	@PanelApplicationId int,
	@UserId int,
	@WorkflowId int = 2
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Declare variables to be used in stored procedure
	DECLARE
		@ApplicationStageId int = 0,
		@SummaryStageId int = 3,
		@MechanismId int,
		@ClientId int,
		@PanelId int,
		@Triaged bit,
		@ApplicationTemplateId int,
		@ApplicationWorkflowId int,
		@ApplicationId int,
		@MechanismTemplateId int,
		@WorkflowPermissionSchemeId int = NULL,
		@OverviewElementTypeId int = 2,
		@ProgramMechanismId int,
		@CurrentDateTime datetime2(0) = dbo.GetP2rmisDateTime(),
		@ReviewStatusId int,
		@IsPriorityOne bit,
		@ClientReviewStepTypeId int = 3,
		@ClientReviewOtherStepTypeId int = 9,
		@UnassignedCommentElementTypeId int = 4,
		@PriorityOneStatusId int = 3
    -- Check if log number has a summary stage associated with it
	SELECT @ApplicationStageId = ISNULL([dbo].[ViewApplicationStage].[ApplicationStageId], 0), @ApplicationId = [dbo].[ViewApplication].[ApplicationId]
	FROM [dbo].[ViewApplication] INNER JOIN 
	[dbo].[ViewPanelApplication] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewPanelApplication].[ApplicationId] LEFT OUTER JOIN
	dbo.[ViewApplicationStage] ON [dbo].[ViewPanelApplication].[PanelApplicationId] = [dbo].[ViewApplicationStage].[PanelApplicationId] AND [dbo].[ViewApplicationStage].[ReviewStageId] = @SummaryStageId
	WHERE [dbo].[ViewPanelApplication].[PanelApplicationId] = @PanelApplicationId
	--If it doesn't exist, insert into application stage table
	IF @ApplicationStageId = 0
	BEGIN
		INSERT INTO dbo.[ApplicationStage] ([dbo].[ViewApplicationStage].[PanelApplicationId], [dbo].[ViewApplicationStage].[ReviewStageId], [dbo].[ViewApplicationStage].[StageOrder], [dbo].[ViewApplicationStage].[ActiveFlag], [dbo].[ViewApplicationStage].[AssignmentVisibilityFlag], [dbo].[ViewApplicationStage].[CreatedBy], [dbo].[ViewApplicationStage].[CreatedDate], [dbo].[ViewApplicationStage].[ModifiedBy], [dbo].[ViewApplicationStage].[ModifiedDate]) 
			SELECT [dbo].[ViewPanelApplication].[PanelApplicationId], @SummaryStageId, (SELECT MAX([dbo].[ViewApplicationStage].[StageOrder]) + 1 FROM [dbo].[ViewApplicationStage] WHERE [dbo].[ViewApplicationStage].[PanelApplicationId] = [dbo].[ViewPanelApplication].[PanelApplicationId]) AS StageOrder, 1, 1, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM dbo.ViewApplication INNER JOIN
				dbo.ViewPanelApplication ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewPanelApplication].[ApplicationId]
			WHERE [dbo].[ViewPanelApplication].[PanelApplicationId] = @PanelApplicationId
		SELECT @ApplicationStageId = SCOPE_IDENTITY()
	END
	--Check whether the application has been marked priority one
	SELECT @IsPriorityOne = CASE WHEN ViewApplicationReviewStatus.ApplicationReviewStatusId IS NOT NULL THEN 1 ELSE 0 END
	FROM ViewApplicationReviewStatus INNER JOIN
		ViewApplicationStage ON ViewApplicationReviewStatus.PanelApplicationId = ViewApplicationStage.PanelApplicationId
	WHERE ViewApplicationStage.ApplicationStageId = @ApplicationStageId AND ViewApplicationReviewStatus.ReviewStatusId = @PriorityOneStatusId

	--Pick up client, mechanism and review panel information from legacy database
	SELECT     @ClientId = [dbo].[ClientAwardType].[ClientId], @ProgramMechanismId = [dbo].[ViewProgramMechanism].[ProgramMechanismId], 
		@MechanismId = [dbo].[ViewProgramMechanism].[ProgramMechanismId], @MechanismTemplateId = [dbo].[ViewMechanismTemplate].[MechanismTemplateId],
		@ReviewStatusId = [dbo].[ViewApplicationReviewStatus].[ReviewStatusId]
	FROM         [dbo].[ViewApplication] INNER JOIN
					[dbo].[ViewPanelApplication] ON [ViewApplication].ApplicationId = ViewPanelApplication.ApplicationId INNER JOIN
                      [dbo].[ViewProgramMechanism] ON [dbo].[ViewApplication].[ProgramMechanismId] = [dbo].[ViewProgramMechanism].[ProgramMechanismId] INNER JOIN
					  [dbo].[ViewProgramYear] ON [dbo].[ViewProgramMechanism].[ProgramYearId] = [dbo].[ViewProgramYear].[ProgramYearId] INNER JOIN
					  [dbo].[ClientAwardType] ON [dbo].[ViewProgramMechanism].[ClientAwardTypeId] = [dbo].[ClientAwardType].[ClientAwardTypeId] INNER JOIN
					  [dbo].[ViewApplicationReviewStatus] ON [dbo].[ViewPanelApplication].[PanelApplicationId] = [dbo].[ViewApplicationReviewStatus].[PanelApplicationId] INNER JOIN
					  [dbo].[ReviewStatus] ON [dbo].[ViewApplicationReviewStatus].[ReviewStatusId] = [dbo].[ReviewStatus].[ReviewStatusId] AND [dbo].[ReviewStatus].[ReviewStatusTypeId] = 1 LEFT OUTER JOIN
					  [dbo].[ViewMechanismTemplate] ON [dbo].[ViewProgramMechanism].[ProgramMechanismId] = [dbo].[ViewMechanismTemplate].[ProgramMechanismId] AND [dbo].[ViewApplicationReviewStatus].[ReviewStatusId] = [dbo].[ViewMechanismTemplate].[ReviewStatusId] 
					  AND [dbo].[ViewMechanismTemplate].[ReviewStageId] = @SummaryStageId
	WHERE     ([dbo].[ViewPanelApplication].[PanelApplicationId] = @PanelApplicationId)
	
	--If mechanism template doesn't exist, get it from another stored procedure
	IF @MechanismTemplateId IS NULL
		BEGIN
				EXEC @MechanismTemplateId = [dbo].[uspAddOtherSummaryMechanismTemplate] @ProgramMechanismId, @ReviewStatusId;
		END
		
	--Begin setup (for these we use a transaction, if error we want to roll back everything to avoid mess, this is kind of experimental)
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	SET XACT_ABORT ON;
	BEGIN TRANSACTION;	

	--Now construct the application template and get the Id
	INSERT INTO [dbo].[ApplicationTemplate] ([dbo].[ViewApplicationTemplate].[ApplicationId], [dbo].[ViewApplicationTemplate].[ApplicationStageId], [dbo].[ViewApplicationTemplate].[MechanismTemplateId], [dbo].[ViewApplicationTemplate].[CreatedBy], [dbo].[ViewApplicationTemplate].[CreatedDate], [dbo].[ViewApplicationTemplate].[ModifiedBy], [dbo].[ViewApplicationTemplate].[ModifiedDate])
	VALUES (@ApplicationId, @ApplicationStageId, @MechanismTemplateId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime)
	SELECT @ApplicationTemplateId = SCOPE_IDENTITY()
	/*Populate the elements for the newly created template
	Currently the business rule is as follows:
	-Each review criteria is replicated for each assigned reviewer
    -The rest of the elements are as defined in the mechanism template
    */
	--Refactor AssignmentId to the new DB ID To be done when Summary Management/Processing queries are refactored completely from old data structure
	INSERT INTO [dbo].[ApplicationTemplateElement] ([dbo].[ViewApplicationTemplateElement].[ApplicationTemplateId], [dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId], [dbo].[ViewApplicationTemplateElement].[PanelApplicationReviewerAssignmentId], [dbo].[ViewApplicationTemplateElement].[DiscussionNoteFlag], [dbo].[ViewApplicationTemplateElement].[CreatedBy], [dbo].[ViewApplicationTemplateElement].[CreatedDate], [dbo].[ViewApplicationTemplateElement].[ModifiedBy], [dbo].[ViewApplicationTemplateElement].[ModifiedDate])
	--Standard criteria
	SELECT     @ApplicationTemplateId, [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId], [dbo].[ViewPanelApplicationReviewerAssignment].[PanelApplicationReviewerAssignmentId], 0, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM         [dbo].[ViewApplication] INNER JOIN
						  [dbo].[ViewApplicationTemplate] INNER JOIN
						  [dbo].[ViewMechanismTemplateElement] ON [dbo].[ViewApplicationTemplate].[MechanismTemplateId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateId] INNER JOIN
						  [dbo].[ClientElement] ON [dbo].[ViewMechanismTemplateElement].[ClientElementId] = [dbo].[ClientElement].[ClientElementId] INNER JOIN
						  [dbo].[ElementType] ON [dbo].[ClientElement].[ElementTypeId] = [dbo].[ElementType].[ElementTypeId] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationTemplate].[ApplicationId] INNER JOIN
						  [dbo].[ViewPanelApplication] AS PanelApplication ON PanelApplication.ApplicationID = [dbo].[ViewApplication].[ApplicationID] INNER JOIN
						  [dbo].[ViewPanelApplicationReviewerAssignment] ON PanelApplication.PanelApplicationId = [dbo].[ViewPanelApplicationReviewerAssignment].[PanelApplicationId] INNER JOIN
						  ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId
	WHERE     ([dbo].[ViewApplicationTemplate].[ApplicationStageId] = @ApplicationStageId) AND ([dbo].[ElementType].[ElementTypeId] = 1) AND (ClientAssignmentType.AssignmentTypeId in (5, 6, 9))
	UNION ALL
	--Overview, unassigned reviewer comments (if exists)
	SELECT     @ApplicationTemplateId, [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId], NULL AS Expr1, 0, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM         [dbo].[ViewApplicationTemplate] INNER JOIN
						  [dbo].[ViewMechanismTemplateElement] ON [dbo].[ViewApplicationTemplate].[MechanismTemplateId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateId] INNER JOIN
						  [dbo].[ClientElement] ON [dbo].[ViewMechanismTemplateElement].[ClientElementId] = [dbo].[ClientElement].[ClientElementId] INNER JOIN
						  [dbo].[ElementType] ON [dbo].[ClientElement].[ElementTypeId] = [dbo].[ElementType].[ElementTypeId]
	WHERE     ([dbo].[ViewApplicationTemplate].[ApplicationStageId] = @ApplicationStageId) AND ([dbo].[ElementType].[ElementTypeId] <> 1)
	UNION ALL
	--Discussion notes (if standard workflow only right now)
	SELECT     @ApplicationTemplateId, [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId], NULL, 1, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM         [dbo].[ViewApplication] INNER JOIN
						  [dbo].[ViewApplicationTemplate] INNER JOIN
						  [dbo].[ViewMechanismTemplateElement] ON [dbo].[ViewApplicationTemplate].[MechanismTemplateId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateId] INNER JOIN
						  [dbo].[ClientElement] ON [dbo].[ViewMechanismTemplateElement].[ClientElementId] = [dbo].[ClientElement].[ClientElementId] INNER JOIN
						  [dbo].[ElementType] ON [dbo].[ClientElement].[ElementTypeId] = [dbo].[ElementType].[ElementTypeId] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationTemplate].[ApplicationId] 
	WHERE     ([dbo].[ViewApplicationTemplate].[ApplicationStageId] = @ApplicationStageId) AND ([dbo].[ElementType].[ElementTypeId] = 1) AND ([dbo].[ViewMechanismTemplateElement].[TextFlag] = 1)
	--Create the application instance of the workflow and it's steps
	INSERT INTO [dbo].[ApplicationWorkflow] ([dbo].[ViewApplicationWorkflow].[WorkflowId], [dbo].[ViewApplicationWorkflow].[ApplicationStageId], [dbo].[ViewApplicationWorkflow].[ApplicationTemplateId], [dbo].[ViewApplicationWorkflow].[ApplicationWorkflowName], [dbo].[ViewApplicationWorkflow].[DateAssigned], [dbo].[ViewApplicationWorkflow].[DateClosed], [dbo].[ViewApplicationWorkflow].[CreatedBy], [dbo].[ViewApplicationWorkflow].[CreatedDate], [dbo].[ViewApplicationWorkflow].[ModifiedBy], [dbo].[ViewApplicationWorkflow].[ModifiedDate])
	SELECT     [dbo].[Workflow].[WorkflowId], @ApplicationStageId, @ApplicationTemplateId, [dbo].[Workflow].[WorkflowName], @CurrentDateTime, NULL, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM         [dbo].[Workflow]
	WHERE [dbo].[Workflow].[WorkflowId] = @WorkflowId
	SELECT @ApplicationWorkflowId = SCOPE_IDENTITY()
	--If step is client review and application is priority one
	INSERT INTO [dbo].[ApplicationWorkflowStep] ([dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId], [dbo].[ViewApplicationWorkflowStep].[StepTypeId], [dbo].[ViewApplicationWorkflowStep].[StepName], [dbo].[ViewApplicationWorkflowStep].[Active], [dbo].[ViewApplicationWorkflowStep].[StepOrder], [dbo].[ViewApplicationWorkflowStep].[Resolution], [dbo].[ViewApplicationWorkflowStep].[ResolutionDate], [dbo].[ViewApplicationWorkflowStep].[CreatedBy], [dbo].[ViewApplicationWorkflowStep].[CreatedDate], [dbo].[ViewApplicationWorkflowStep].[ModifiedBy], [dbo].[ViewApplicationWorkflowStep].[ModifiedDate])
	SELECT      @ApplicationWorkflowId, [dbo].[WorkflowStep].[StepTypeId], [dbo].[WorkflowStep].[StepName], CASE WHEN @IsPriorityOne = 1 AND (WorkflowStep.StepTypeId = @ClientReviewStepTypeId OR WorkflowStep.StepTypeId = @ClientReviewOtherStepTypeId) THEN 1 ELSE [dbo].[WorkflowStep].[ActiveDefault] END, [dbo].[WorkflowStep].[StepOrder], 0, NULL, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM         [dbo].[WorkflowStep]
	WHERE [dbo].[WorkflowStep].[WorkflowId] = @WorkflowId AND [dbo].[WorkflowStep].DeletedFlag = 0;
	/* Setup ApplicationWorkflowStepElements */

	
	INSERT INTO [dbo].[ApplicationWorkflowStepElement] ([dbo].[ApplicationWorkflowStepElement].[ApplicationWorkflowStepId], [dbo].[ApplicationWorkflowStepElement].[ApplicationTemplateElementId], [dbo].[ApplicationWorkflowStepElement].[AccessLevelId], [dbo].[ApplicationWorkflowStepElement].[ClientScoringId], [dbo].[ApplicationWorkflowStepElement].[CreatedBy], [dbo].[ApplicationWorkflowStepElement].[CreatedDate], [dbo].[ApplicationWorkflowStepElement].[ModifiedBy], [dbo].[ApplicationWorkflowStepElement].[ModifiedDate])
	SELECT     [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowStepId], [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateElementId], 1, [dbo].[ViewMechanismTemplateElementScoring].[ClientScoringId],
                      @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM [dbo].[ViewApplicationWorkflow] INNER JOIN
	[dbo].[ViewApplicationWorkflowStep] ON [dbo].[ViewApplicationWorkflow].[ApplicationWorkflowId] = [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] INNER JOIN
	[dbo].[ViewApplicationTemplateElement] ON [dbo].[ViewApplicationWorkflow].[ApplicationTemplateId] = [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateId] LEFT OUTER JOIN
	[dbo].[ViewMechanismTemplateElementScoring] ON [dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId] = [dbo].[ViewMechanismTemplateElementScoring].[MechanismTemplateElementId] AND
	[dbo].[ViewApplicationWorkflowStep].[StepTypeId] = [dbo].[ViewMechanismTemplateElementScoring].[StepTypeId]
	WHERE [dbo].[ViewApplicationWorkflow].[ApplicationWorkflowId] = @ApplicationWorkflowId;
	
	--Pull in overview data from UserApplicationSummary
	INSERT INTO [dbo].[ApplicationWorkflowStepElementContent] ([dbo].[ApplicationWorkflowStepElementContent].[ApplicationWorkflowStepElementId], [dbo].[ApplicationWorkflowStepElementContent].[ContentText], [dbo].[ApplicationWorkflowStepElementContent].[CreatedBy], [dbo].[ApplicationWorkflowStepElementContent].[CreatedDate], [dbo].[ApplicationWorkflowStepElementContent].[ModifiedBy], [dbo].[ApplicationWorkflowStepElementContent].[ModifiedDate])
	SELECT [dbo].[ApplicationWorkflowStepElement].[ApplicationWorkflowStepElementId], [dbo].[PanelApplicationSummary].[SummaryText], @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM [dbo].[ApplicationWorkflowStepElement] INNER JOIN
	[dbo].[ViewApplicationWorkflowStep] ON [dbo].[ApplicationWorkflowStepElement].[ApplicationWorkflowStepId] = [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowStepId] INNER JOIN
	[dbo].[ViewApplicationWorkflow] ON [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] = [dbo].[ViewApplicationWorkflow].[ApplicationWorkflowId] INNER JOIN
	[dbo].[ViewApplicationStage] ON [dbo].[ViewApplicationWorkflow].[ApplicationStageId] = [dbo].[ViewApplicationStage].[ApplicationStageId] INNER JOIN
	[dbo].[PanelApplicationSummary] ON [dbo].[ViewApplicationStage].[PanelApplicationId] = [dbo].[PanelApplicationSummary].[PanelApplicationId] INNER JOIN
	[dbo].[ViewApplicationTemplateElement] ON [dbo].[ApplicationWorkflowStepElement].[ApplicationTemplateElementId] = [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateElementId] INNER JOIN
	[dbo].[ViewMechanismTemplateElement] ON [dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId] INNER JOIN
	[dbo].[ClientElement] ON [dbo].[ViewMechanismTemplateElement].[ClientElementId] = [dbo].[ClientElement].[ClientElementId]
	WHERE  ([dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] = @ApplicationWorkflowId) AND ([dbo].[ViewApplicationWorkflowStep].[StepOrder] = 1) AND ([dbo].[ClientElement].[ElementTypeId] = @OverviewElementTypeId);

	--Pull in unassigned reviewer comments (they are concatenated into a single box)
	INSERT INTO [dbo].[ApplicationWorkflowStepElementContent] ([dbo].[ApplicationWorkflowStepElementContent].[ApplicationWorkflowStepElementId], [dbo].[ApplicationWorkflowStepElementContent].[ContentText], [dbo].[ApplicationWorkflowStepElementContent].[CreatedBy], [dbo].[ApplicationWorkflowStepElementContent].[CreatedDate], [dbo].[ApplicationWorkflowStepElementContent].[ModifiedBy], [dbo].[ApplicationWorkflowStepElementContent].[ModifiedDate])
	SELECT [dbo].[ApplicationWorkflowStepElement].[ApplicationWorkflowStepElementId], 
	STUFF((SELECT N'<br />--<br />' + Comments FROM UserApplicationComment WHERE PanelApplicationId = ViewApplicationStage.PanelApplicationId AND CommentTypeID = 5 FOR XML PATH, TYPE).value('.[1]', 'nvarchar(max)'), 1, 14, ''), 
	@UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM [dbo].[ApplicationWorkflowStepElement] INNER JOIN
	[dbo].[ViewApplicationWorkflowStep] ON [dbo].[ApplicationWorkflowStepElement].[ApplicationWorkflowStepId] = [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowStepId] INNER JOIN
	[dbo].[ViewApplicationWorkflow] ON [dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] = [dbo].[ViewApplicationWorkflow].[ApplicationWorkflowId] INNER JOIN
	[dbo].[ViewApplicationStage] ON [dbo].[ViewApplicationWorkflow].[ApplicationStageId] = [dbo].[ViewApplicationStage].[ApplicationStageId] INNER JOIN
	[dbo].[ViewApplicationTemplateElement] ON [dbo].[ApplicationWorkflowStepElement].[ApplicationTemplateElementId] = [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateElementId] INNER JOIN
	[dbo].[ViewMechanismTemplateElement] ON [dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId] INNER JOIN
	[dbo].[ClientElement] ON [dbo].[ViewMechanismTemplateElement].[ClientElementId] = [dbo].[ClientElement].[ClientElementId]
	WHERE  ([dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] = @ApplicationWorkflowId) AND ([dbo].[ViewApplicationWorkflowStep].[StepOrder] = 1) AND ([dbo].[ClientElement].[ElementTypeId] = @UnassignedCommentElementTypeId);
	
	--Pull in reviewer critiques from legacy P2RMIS for first step content
			INSERT INTO [dbo].[ApplicationWorkflowStepElementContent] ([dbo].[ApplicationWorkflowStepElementContent].[ApplicationWorkflowStepElementId], [dbo].[ApplicationWorkflowStepElementContent].[Score], [dbo].[ApplicationWorkflowStepElementContent].[ContentText], [dbo].[ApplicationWorkflowStepElementContent].[ContentTextNoMarkup], [dbo].[ApplicationWorkflowStepElementContent].[Abstain], [dbo].[ApplicationWorkflowStepElementContent].[CreatedBy], [dbo].[ApplicationWorkflowStepElementContent].[CreatedDate], [dbo].[ApplicationWorkflowStepElementContent].[ModifiedBy], [dbo].[ApplicationWorkflowStepElementContent].[ModifiedDate])
			SELECT ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, Critique.Score, Critique.ContentText, Critique.ContentText, Critique.Abstain, Critique.CreatedBy, dbo.GetP2rmisDateTime(), Critique.ModifiedBy, dbo.GetP2rmisDateTime()
			FROM         dbo.[ViewApplicationWorkflowStep] INNER JOIN
			[ViewApplicationWorkflowStepElement] ON ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
			[ViewApplicationTemplateElement] ON ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
			[ViewMechanismTemplateElement] ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId INNER JOIN
			[ViewApplicationWorkflow] ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId INNER JOIN
			[ViewApplicationStage] ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
			[ViewPanelApplicationReviewerAssignment] ON ViewApplicationTemplateElement.PanelApplicationReviewerAssignmentId = ViewPanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId CROSS APPLY
			[udfLastUpdatedCritiquePhase](ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId, ViewApplicationStage.PanelApplicationId, ViewMechanismTemplateElement.ClientElementId) AS Critique
			WHERE     ([dbo].[ViewApplicationWorkflowStep].[ApplicationWorkflowId] = @ApplicationWorkflowId) AND ([dbo].[ViewApplicationWorkflowStep].[StepOrder] = 1);
	COMMIT TRANSACTION;
END

GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspBeginApplicationSummaryWorkflow] TO [NetSqlAzMan_Users]
    AS [dbo];
