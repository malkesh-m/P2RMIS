CREATE PROCEDURE [dbo].[uspBeginScoringWorkflow]
	@panelApplicationId int = 0,
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Declare variables to be used in stored procedure
	DECLARE

		@ApplicationTemplateMeetingId int,
		@ApplicationWorkflowMeetingId int,
		@ApplicationWorkflowStepAssignmentId int,
		@ApplicationWorkflowStepElementCount int = 0,
		@ApplicationWorkflowStepElementContentCount int = 0,
		@ApplicationTemplateElementCount int = 0,
		@CurrentDateTime datetime2(0) = dbo.GetP2rmisDateTime(),
		@PreReviewStage int = 1,
		@MeetingReviewStage int = 2, 
		@StepTypeId int = 4,
		@AccessLevelId int = 1,
		@PanelApplicationReviewerAssignmentId int,
		@PanelUserAssignmentId int,
		@AssignmentId int, 
		@CoiId int = 8,
		@ReaderId int = 7
	
	--First set the active flag for the ApplicationStage to true
	UPDATE ApplicationStage
	SET ActiveFlag = 1, ModifiedBy = @userId, ModifiedDate = @CurrentDateTime
	WHERE PanelApplicationId = @panelApplicationId AND ReviewStageId = @MeetingReviewStage

	-- Check if application template exists in new database and grab the old template (this only really needs to run once per panel application)
	SELECT @ApplicationTemplateMeetingId = ViewApplicationTemplate.ApplicationTemplateId, @ApplicationTemplateElementCount = SUM(CASE WHEN ViewApplicationTemplateElement.ApplicationTemplateElementId IS NOT NULL THEN 1 ELSE 0 END)
	FROM ViewPanelApplication INNER JOIN
	ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = @MeetingReviewStage LEFT OUTER JOIN
	ViewApplicationTemplate ON ViewApplicationStage.ApplicationStageId = ViewApplicationTemplate.ApplicationStageId LEFT OUTER JOIN
	ViewApplicationTemplateElement ON ViewApplicationTemplate.ApplicationTemplateId = ViewApplicationTemplateElement.ApplicationTemplateId
	WHERE ViewPanelApplication.PanelApplicationId = @panelApplicationId
	GROUP BY ViewApplicationTemplate.ApplicationTemplateId;
	IF @ApplicationTemplateMeetingId IS NULL
	BEGIN
			--Repeat for meeting template
			INSERT INTO dbo.[ApplicationTemplate] (ApplicationId, ApplicationStageId, MechanismTemplateId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [Application].ApplicationId, [ApplicationStage].ApplicationStageId, MechanismTemplate.MechanismTemplateId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	dbo.[ViewPanelApplication] AS PanelApplication INNER JOIN
					dbo.[ViewApplicationStage] AS ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = @MeetingReviewStage INNER JOIN
					dbo.[ViewApplication] AS Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					dbo.[ViewProgramMechanism] AS ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
					dbo.[ViewMechanismTemplate] AS MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND MechanismTemplate.ReviewStageId = @MeetingReviewStage
			WHERE PanelApplication.PanelApplicationId = @panelApplicationId;
		SELECT @ApplicationTemplateMeetingId = SCOPE_IDENTITY()
	END
	IF @ApplicationTemplateElementCount = 0
	BEGIN
		INSERT INTO dbo.[ApplicationTemplateElement] (ApplicationTemplateId, MechanismTemplateElementId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [ApplicationTemplate].ApplicationTemplateId, [MechanismTemplateElement].MechanismTemplateElementId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	dbo.ViewApplicationTemplate AS ApplicationTemplate INNER JOIN 
					dbo.ViewMechanismTemplate AS MechanismTemplate ON ApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
					dbo.ViewMechanismTemplateElement AS MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId
			WHERE	ApplicationTemplate.ApplicationTemplateId = @ApplicationTemplateMeetingId;
	END
	--Loop through a cursor of all panel users who should have a workflow set up for the application
	--Cursor is probably unnecessary here however using one for maximum code reuse from uspBeginReviewerWorkflow
	DECLARE PanelUserCursor CURSOR FOR
				SELECT PanelUserAssignment.PanelUserAssignmentId, PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId, AssignmentType.AssignmentTypeId, ApplicationWorkflow.ApplicationWorkflowId, 
				SUM(CASE WHEN ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId IS NOT NULL THEN 1 ELSE 0 END), SUM(CASE WHEN ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementContentId IS NOT NULL THEN 1 ELSE 0 END),
				ViewApplicationWorkflowStepAssignment.ApplicationWorkflowStepAssignmentId
				FROM ViewPanelApplication AS PanelApplication INNER JOIN
				ViewPanelUserAssignment AS PanelUserAssignment ON PanelApplication.SessionPanelId = PanelUserAssignment.SessionPanelId INNER JOIN
				ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
				ViewApplicationStage AS ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = @MeetingReviewStage	LEFT OUTER JOIN
				ViewPanelApplicationReviewerAssignment AS PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
				ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId LEFT OUTER JOIN
				AssignmentType ON ClientAssignmentType.AssignmentTypeId = AssignmentType.AssignmentTypeId LEFT OUTER JOIN
				ViewApplicationWorkflow AS ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId LEFT OUTER JOIN
				ViewApplicationWorkflowStep AS ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId LEFT OUTER JOIN
				ViewApplicationWorkflowStepAssignment ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepAssignment.ApplicationWorkflowStepId LEFT OUTER JOIN
				ViewApplicationWorkflowStepElement AS ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId LEFT OUTER JOIN
				ViewApplicationWorkflowStepElementContent AS ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
				WHERE PanelApplication.PanelApplicationId = @panelApplicationId AND ClientParticipantType.ReviewerFlag = 1 AND (AssignmentType.AssignmentTypeId IS NULL OR AssignmentType.AssignmentTypeId <> 8)
				GROUP BY PanelUserAssignment.PanelUserAssignmentId, PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId, AssignmentType.AssignmentTypeId, ApplicationWorkflow.ApplicationWorkflowId, ViewApplicationWorkflowStepAssignment.ApplicationWorkflowStepAssignmentId
		OPEN PanelUserCursor;
		-- Get the first record from cursor
		FETCH NEXT FROM PanelUserCursor INTO @PanelUserAssignmentId, @PanelApplicationReviewerAssignmentId, @AssignmentId, @ApplicationWorkflowMeetingId, @ApplicationWorkflowStepElementCount, @ApplicationWorkflowStepElementContentCount, @ApplicationWorkflowStepAssignmentId;			
		WHILE @@FETCH_STATUS=0
		BEGIN				
		IF (@ApplicationWorkflowMeetingId IS NULL)
		BEGIN
			--Repeat to set up this assigned reviewers meeting stage workflow
			INSERT INTO dbo.[ApplicationWorkflow] (WorkflowId, ApplicationStageId, ApplicationTemplateId, PanelUserAssignmentId,
			ApplicationWorkflowName, DateAssigned, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT PanelStage.WorkflowId, ApplicationStage.ApplicationStageId, @ApplicationTemplateMeetingId, PanelUserAssignment.PanelUserAssignmentId,
			'Meeting Critique', @CurrentDateTime, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM ViewPanelUserAssignment AS PanelUserAssignment INNER JOIN
				ViewUserInfo AS UserInfo ON PanelUserAssignment.UserId = UserInfo.UserId INNER JOIN
				ViewApplicationStage AS ApplicationStage ON @MeetingReviewStage = ApplicationStage.ReviewStageId INNER JOIN
				ViewPanelApplication AS PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
				ViewPanelStage AS PanelStage ON PanelUserAssignment.SessionPanelId = PanelStage.SessionPanelId AND @MeetingReviewStage = PanelStage.ReviewStageId
			WHERE PanelApplication.PanelApplicationId = @panelApplicationId AND PanelUserAssignment.PanelUserAssignmentId = @PanelUserAssignmentId;
			SELECT @ApplicationWorkflowMeetingId = SCOPE_IDENTITY()
			--And populate application workflow steps
			INSERT INTO dbo.[ApplicationWorkflowStep] (ApplicationWorkflowId, StepTypeId, StepName, [Active], StepOrder, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			SELECT @ApplicationWorkflowMeetingId, WorkflowStep.StepTypeId, WorkflowStep.StepName, WorkflowStep.ActiveDefault, WorkflowStep.StepOrder, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM ViewApplicationWorkflow AS ApplicationWorkflow INNER JOIN
			ViewWorkflowStep AS WorkflowStep ON ApplicationWorkflow.WorkflowId = WorkflowStep.WorkflowId
			WHERE ApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowMeetingId;	
		END
		IF (@ApplicationWorkflowStepElementCount = 0)
		BEGIN
			--Insert elements for all steps that have been set up
			INSERT INTO dbo.[ApplicationWorkflowStepElement] (ApplicationWorkflowStepId, ApplicationTemplateElementId, ClientScoringId, AccessLevelId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, MechanismTemplateElementScoring.ClientScoringId, @AccessLevelId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM         ViewApplicationWorkflow AS ApplicationWorkflow INNER JOIN
							  ViewApplicationWorkflowStep AS ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
							  ViewApplicationTemplateElement AS ApplicationTemplateElement ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
							  ViewMechanismTemplateElement AS MechanismTemplateElement ON 
							  ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId LEFT OUTER JOIN
							  ViewMechanismTemplateElementScoring AS MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND 
							  ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId
			WHERE ApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowMeetingId AND (@AssignmentId IS NOT NULL AND @AssignmentId NOT IN (@CoiId, @ReaderId) OR MechanismTemplateElement.ScoreFlag = 1); 
		END
		IF (@ApplicationWorkflowStepElementContentCount = 0 AND @PanelApplicationReviewerAssignmentId IS NOT NULL)
		BEGIN
			--Insert content (scores only for meeting stage)
			INSERT INTO [dbo].[ApplicationWorkflowStepElementContent]
			   ([ApplicationWorkflowStepElementId]
			   ,[ContentText]
			   ,[Abstain]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
			SELECT ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, ViewApplicationWorkflowStepElementContent.ContentText, ViewApplicationWorkflowStepElementContent.Abstain, ViewApplicationWorkflowStepElementContent.CreatedBy,
				@CurrentDateTime, ViewApplicationWorkflowStepElementContent.ModifiedBy, @CurrentDateTime
			FROM ViewApplicationWorkflowStepElement INNER JOIN --Current
			ViewApplicationTemplateElement AS ApplicationTemplateElementCurrent ON ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElementCurrent.ApplicationTemplateElementId INNER JOIN --Current
			ViewMechanismTemplateElement AS MechanismTemplateElementCurrent ON ApplicationTemplateElementCurrent.MechanismTemplateElementId = MechanismTemplateElementCurrent.MechanismTemplateElementId INNER JOIN
			ViewMechanismTemplate AS MechanismTemplateCurrent ON MechanismTemplateElementCurrent.MechanismTemplateId = MechanismTemplateCurrent.MechanismTemplateId INNER JOIN
			ViewApplicationWorkflowStep AS ApplicationWorkflowStepCurrent ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStepCurrent.ApplicationWorkflowStepId INNER JOIN --Current
			ViewApplicationWorkflow AS ApplicationWorkflowCurrent ON ApplicationWorkflowStepCurrent.ApplicationWorkflowId = ApplicationWorkflowCurrent.ApplicationWorkflowId INNER JOIN --Current
			ViewApplicationStage AS ApplicationStageCurrent ON ApplicationWorkflowCurrent.ApplicationStageId = ApplicationStageCurrent.ApplicationStageId INNER JOIN --Current
			ViewApplicationStage AS ApplicationStagePrevious ON ApplicationStageCurrent.PanelApplicationId = ApplicationStagePrevious.PanelApplicationId AND @PreReviewStage = ApplicationStagePrevious.ReviewStageId INNER JOIN --Previous
			ViewApplicationWorkflow AS ApplicationWorkflowPrevious ON ApplicationStagePrevious.ApplicationStageId = ApplicationWorkflowPrevious.ApplicationStageId AND ApplicationWorkflowCurrent.PanelUserAssignmentId = ApplicationWorkflowPrevious.PanelUserAssignmentId CROSS APPLY --Previous
			udfApplicationWorkflowLastStep(ApplicationWorkflowPrevious.ApplicationWorkflowId) AS ApplicationWorkflowStepPrevious INNER JOIN --Previous
			ViewMechanismTemplate AS MechanismTemplatePrevious ON MechanismTemplateCurrent.ProgramMechanismId = MechanismTemplatePrevious.ProgramMechanismId AND ApplicationStagePrevious.ReviewStageId = MechanismTemplatePrevious.ReviewStageId INNER JOIN --Previous
			ViewMechanismTemplateElement AS MechanismTemplateElementPrevious ON MechanismTemplatePrevious.MechanismTemplateId = MechanismTemplateElementPrevious.MechanismTemplateId AND MechanismTemplateElementCurrent.ClientElementId = MechanismTemplateElementPrevious.ClientElementId INNER JOIN
			ViewApplicationTemplate AS ApplicationTemplatePrevious ON ApplicationStagePrevious.ApplicationStageId = ApplicationTemplatePrevious.ApplicationStageId INNER JOIN --Previous
			ViewApplicationTemplateElement AS ApplicationTemplateElementPrevious ON MechanismTemplateElementPrevious.MechanismTemplateElementId = ApplicationTemplateElementPrevious.MechanismTemplateElementId AND ApplicationTemplatePrevious.ApplicationTemplateId = ApplicationTemplateElementPrevious.ApplicationTemplateId INNER JOIN --Previous
			ViewApplicationWorkflowStepElement AS ApplicationWorkflowStepElementPrevious ON ApplicationWorkflowStepPrevious.ApplicationWorkflowStepId = ApplicationWorkflowStepElementPrevious.ApplicationWorkflowStepId AND ApplicationTemplateElementPrevious.ApplicationTemplateElementId = ApplicationWorkflowStepElementPrevious.ApplicationTemplateElementId INNER JOIN --Previous
			ViewApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElementPrevious.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId --Previous
			WHERE ApplicationWorkflowCurrent.ApplicationWorkflowId = @ApplicationWorkflowMeetingId
		END
		IF (@ApplicationWorkflowStepAssignmentId IS NULL)
		BEGIN
			--Insert assignments for reviewer at the step level
			INSERT INTO dbo.[ApplicationWorkflowStepAssignment] (ApplicationWorkflowStepId, UserId, AssignmentId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
			SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, CASE WHEN ClientParticipantType.ConsumerFlag = 1 THEN 6 ELSE 5 END, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM ViewApplicationWorkflow AS ApplicationWorkflow INNER JOIN
			ViewPanelUserAssignment AS PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
			ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
			ViewApplicationStage AS ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
			ViewApplicationWorkflowStep AS ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
			WHERE ApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowMeetingId;
		END
		FETCH NEXT FROM PanelUserCursor INTO @PanelUserAssignmentId, @PanelApplicationReviewerAssignmentId, @AssignmentId, @ApplicationWorkflowMeetingId, @ApplicationWorkflowStepElementCount, @ApplicationWorkflowStepElementContentCount, @ApplicationWorkflowStepAssignmentId;			
	END
	CLOSE PanelUserCursor
	DEALLOCATE PanelUserCursor
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspBeginScoringWorkflow] TO [NetSqlAzMan_Users], [web-p2rmis]
    AS [dbo];
