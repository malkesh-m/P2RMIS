
-- =============================================
-- Author:		Craig Henson
-- Create date: 2/11/2015
-- Description:	This stored procedure populates the necessary data for an application to begin an assigned reviewer workflow for a single reviewer.
-- =============================================
CREATE PROCEDURE [dbo].[uspBeginReviewerWorkflow] 
	@PanelApplicationReviewerAssignmentId int,
	@UserId int
	--@ReviewStageId int ***This stored procedure could be improved by giving it a singular ReviewStage. The question would be how/when the template gets set up for Meeting.
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Declare variables to be used in stored procedure
	DECLARE
		@ApplicationTemplatePreId int,
		@ApplicationWorkflowPreId int,
		@ApplicationTemplateMeetingId int,
		@ApplicationWorkflowMeetingId int,
		@ApplicationTemplateCount int,
		@MechanismTemplateId int,
		@CurrentDateTime datetime2(0) = dbo.GetP2rmisDateTime(),
		@PreReviewStage int = 1,
		@MeetingReviewStage int = 2, 
		@StepTypeId int = 4,
		@AccessLevelId int = 1
    -- Check if application template exists in new database
	SELECT @ApplicationTemplatePreId = ApplicationTemplatePre.ApplicationTemplateId, @ApplicationTemplateMeetingId = ApplicationTemplateMeeting.ApplicationTemplateId
	FROM (SELECT ApplicationTemplate.ApplicationTemplateId, PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId, ApplicationStage.ReviewStageId
		FROM dbo.[ViewPanelApplicationReviewerAssignment] PanelApplicationReviewerAssignment INNER JOIN
			dbo.[ViewApplicationStage] ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			dbo.[ApplicationTemplate] ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId
		WHERE PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId = @PanelApplicationReviewerAssignmentId AND ApplicationTemplate.DeletedFlag = 0 AND
			ApplicationStage.ReviewStageId = @PreReviewStage) ApplicationTemplatePre LEFT OUTER JOIN 
	(SELECT ApplicationTemplate.ApplicationTemplateId, PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId, ApplicationStage.ReviewStageId
		FROM dbo.[ViewPanelApplicationReviewerAssignment] PanelApplicationReviewerAssignment INNER JOIN
			dbo.[ViewApplicationStage] ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			dbo.[ApplicationTemplate] ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId
		WHERE PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId = @PanelApplicationReviewerAssignmentId AND ApplicationTemplate.DeletedFlag = 0 AND
			ApplicationStage.ReviewStageId = @MeetingReviewStage) ApplicationTemplateMeeting ON ApplicationTemplatePre.PanelApplicationReviewerAssignmentId = ApplicationTemplateMeeting.PanelApplicationReviewerAssignmentId
	
	
	--If it doesn't exist, insert application template as well as populate it's elements
	IF @ApplicationTemplatePreId IS NULL
	BEGIN
		INSERT INTO dbo.[ApplicationTemplate] (ApplicationId, ApplicationStageId, MechanismTemplateId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [Application].ApplicationId, [ApplicationStage].ApplicationStageId, MechanismTemplate.MechanismTemplateId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	dbo.[ViewPanelApplicationReviewerAssignment] PanelApplicationReviewerAssignment INNER JOIN
					dbo.[ViewPanelApplication] PanelApplication ON PanelApplicationReviewerAssignment.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
					dbo.[ViewApplicationStage] ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = @PreReviewStage INNER JOIN
					dbo.[ViewApplication] [Application] ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					dbo.[ViewProgramMechanism] ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
					dbo.[MechanismTemplate] ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND MechanismTemplate.ReviewStageId = @PreReviewStage
			WHERE [PanelApplicationReviewerAssignmentId] = @PanelApplicationReviewerAssignmentId AND MechanismTemplate.DeletedFlag = 0; 
		SELECT @ApplicationTemplatePreId = SCOPE_IDENTITY()
		INSERT INTO dbo.[ApplicationTemplateElement] (ApplicationTemplateId, MechanismTemplateElementId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [ApplicationTemplate].ApplicationTemplateId, [MechanismTemplateElement].MechanismTemplateElementId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	ViewApplicationTemplate ApplicationTemplate INNER JOIN 
					ViewMechanismTemplate MechanismTemplate ON ApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
					MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId
			WHERE	ApplicationTemplate.ApplicationTemplateId = @ApplicationTemplatePreId AND dbo.MechanismTemplateElement.DeletedFlag = 0;
	END
	IF @ApplicationTemplateMeetingId IS NULL
	BEGIN
			--Repeat for meeting template
			INSERT INTO dbo.[ApplicationTemplate] (ApplicationId, ApplicationStageId, MechanismTemplateId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [Application].ApplicationId, [ApplicationStage].ApplicationStageId, MechanismTemplate.MechanismTemplateId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	dbo.[ViewPanelApplicationReviewerAssignment] PanelApplicationReviewerAssignment INNER JOIN
					dbo.[ViewPanelApplication] PanelApplication ON PanelApplicationReviewerAssignment.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
					dbo.[ViewApplicationStage] ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = @MeetingReviewStage INNER JOIN
					dbo.[ViewApplication] [Application] ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					dbo.[ViewProgramMechanism] ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
					dbo.[MechanismTemplate] ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND MechanismTemplate.ReviewStageId = @MeetingReviewStage
			WHERE [PanelApplicationReviewerAssignmentId] = @PanelApplicationReviewerAssignmentId AND MechanismTemplate.DeletedFlag = 0;
		SELECT @ApplicationTemplateMeetingId = SCOPE_IDENTITY()
		INSERT INTO dbo.[ApplicationTemplateElement] (ApplicationTemplateId, MechanismTemplateElementId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [ApplicationTemplate].ApplicationTemplateId, [MechanismTemplateElement].MechanismTemplateElementId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	ViewApplicationTemplate ApplicationTemplate INNER JOIN 
					ViewMechanismTemplate MechanismTemplate ON ApplicationTemplate.ApplicationTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
					MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId
			WHERE	ApplicationTemplate.ApplicationTemplateId = @ApplicationTemplateMeetingId AND dbo.MechanismTemplateElement.DeletedFlag = 0;
	END
	
	--Next we need to create the ApplicationWorkflow instance for this reviewer
	INSERT INTO dbo.[ApplicationWorkflow] (WorkflowId,  ApplicationStageId, ApplicationTemplateId, PanelUserAssignmentId,
	ApplicationWorkflowName, DateAssigned, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
	SELECT PanelStage.WorkflowId, ApplicationStage.ApplicationStageId, @ApplicationTemplatePreId, PanelUserAssignment.PanelUserAssignmentId,
	'Pre-Meeting Critique', @CurrentDateTime, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment INNER JOIN 
		ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
		ViewUserInfo UserInfo ON PanelUserAssignment.UserId = UserInfo.UserId INNER JOIN
		ViewApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId AND @PreReviewStage = ApplicationStage.ReviewStageId INNER JOIN
		ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
		ViewPanelStage PanelStage ON PanelUserAssignment.SessionPanelId = PanelStage.SessionPanelId AND @PreReviewStage = PanelStage.ReviewStageId
	WHERE PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId = @PanelApplicationReviewerAssignmentId;
	SELECT @ApplicationWorkflowPreId = SCOPE_IDENTITY()
	--And populate application workflow steps
	INSERT INTO dbo.[ApplicationWorkflowStep] (ApplicationWorkflowId, StepTypeId, StepName, Active, StepOrder, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT @ApplicationWorkflowPreId, WorkflowStep.StepTypeId, WorkflowStep.StepName, WorkflowStep.ActiveDefault, WorkflowStep.StepOrder, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
	WorkflowStep ON ApplicationWorkflow.WorkflowId = WorkflowStep.WorkflowId
	WHERE WorkflowStep.DeletedFlag = 0 AND ApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowPreId;

	
	--Insert elements for all steps that have been set up
	INSERT INTO dbo.[ApplicationWorkflowStepElement] (ApplicationWorkflowStepId, ApplicationTemplateElementId, ClientScoringId, AccessLevelId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, MechanismTemplateElementScoring.ClientScoringId, @AccessLevelId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM         ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId LEFT OUTER JOIN
                      MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND 
                      ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId AND MechanismTemplateElementScoring.DeletedFlag = 0
	WHERE ApplicationWorkflow.ApplicationWorkflowId IN (@ApplicationWorkflowPreId); 
	
	--Insert assignments for reviewer at the step level
	INSERT INTO dbo.[ApplicationWorkflowStepAssignment] (ApplicationWorkflowStepId, UserId, AssignmentId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, ClientAssignmentType.AssignmentTypeId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	PanelApplicationReviewerAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	ApplicationStage.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
	ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
	WHERE ApplicationWorkflow.ApplicationWorkflowId IN (@ApplicationWorkflowPreId)
	AND PanelApplicationReviewerAssignment.DeletedFlag = 0;
END

GO


GRANT EXECUTE
    ON OBJECT::[dbo].[uspBeginReviewerWorkflow] TO [NetSqlAzMan_Users], [web-p2rmis]
    AS [dbo];