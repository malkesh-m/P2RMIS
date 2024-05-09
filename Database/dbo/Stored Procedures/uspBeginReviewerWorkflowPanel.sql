
-- =============================================
-- Author:		Craig Henson
-- Create date: 9/06/2018
-- Description:	This stored procedure populates the necessary data for an application to begin an assigned reviewer workflow for all reviewers on the panel.
-- =============================================
CREATE PROCEDURE [dbo].[uspBeginReviewerWorkflowPanel] 
	@SessionPanelId int,
	@UserId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Declare variables to be used in stored procedure
	DECLARE
		@CurrentDateTime datetime2(0) = dbo.GetP2rmisDateTime(),
		@PreReviewStage int = 1,
		@MeetingReviewStage int = 2, 
		@StepTypeId int = 4,
		@AccessLevelId int = 1

	
	--Add templates and elements for any panel applications that are missing (pre-meeting and meeting)

		INSERT INTO dbo.[ApplicationTemplate] (ApplicationId, ApplicationStageId, MechanismTemplateId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [Application].ApplicationId, [ApplicationStage].ApplicationStageId, MechanismTemplate.MechanismTemplateId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	dbo.[ViewPanelApplication] PanelApplication INNER JOIN
					dbo.[ViewApplicationStage] ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = @PreReviewStage INNER JOIN
					dbo.[ViewApplication] [Application] ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					dbo.[ViewProgramMechanism] ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
					dbo.[MechanismTemplate] ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ApplicationStage.ReviewStageId = MechanismTemplate.ReviewStageId 
			WHERE [PanelApplication].[SessionPanelId] = @SessionPanelId AND MechanismTemplate.DeletedFlag = 0 AND NOT EXISTS
			(Select 'X' FROM ViewApplicationTemplate WHERE ViewApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId AND ViewApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId); 
		INSERT INTO dbo.[ApplicationTemplateElement] (ApplicationTemplateId, MechanismTemplateElementId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
			SELECT [ApplicationTemplate].ApplicationTemplateId, [MechanismTemplateElement].MechanismTemplateElementId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
			FROM	ViewApplicationTemplate ApplicationTemplate INNER JOIN 
					ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
					ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
					ViewMechanismTemplate MechanismTemplate ON ApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId AND ApplicationStage.ReviewStageId = MechanismTemplate.ReviewStageId INNER JOIN
					MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId
			WHERE	[PanelApplication].[SessionPanelId] = @SessionPanelId AND dbo.MechanismTemplateElement.DeletedFlag = 0 AND NOT EXISTS
			(Select 'X' FROM ViewApplicationTemplateElement WHERE ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId AND MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId);
	
	--Next we need to create the ApplicationWorkflow instance for all reviewer preliminary only from here on out
	INSERT INTO dbo.[ApplicationWorkflow] (WorkflowId,  ApplicationStageId, ApplicationTemplateId, PanelUserAssignmentId,
	ApplicationWorkflowName, DateAssigned, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
	SELECT PanelStage.WorkflowId, ApplicationStage.ApplicationStageId, ApplicationTemplate.ApplicationTemplateId, PanelUserAssignment.PanelUserAssignmentId,
	'Pre-Meeting Critique', @CurrentDateTime, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment INNER JOIN 
		ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
		ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
		ViewUserInfo UserInfo ON PanelUserAssignment.UserId = UserInfo.UserId INNER JOIN
		ViewApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
		ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId  INNER JOIN
		ViewApplication [Application] ON PanelApplication.ApplicationId = [Application].ApplicationId INNER JOIN
		ViewProgramMechanism ProgramMechanism ON [Application].ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
		ViewMechanismTemplate MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ApplicationStage.ReviewStageId = MechanismTemplate.ReviewStageId INNER JOIN
		ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId AND MechanismTemplate.MechanismTemplateId = ApplicationTemplate.MechanismTemplateId INNER JOIN
		ViewPanelStage PanelStage ON PanelUserAssignment.SessionPanelId = PanelStage.SessionPanelId AND ApplicationStage.ReviewStageId = PanelStage.ReviewStageId 
	WHERE PanelStage.SessionPanelId = @SessionPanelId AND ApplicationStage.ReviewStageId = @PreReviewStage AND ClientAssignmentType.AssignmentTypeId in (5, 6, 9)
		AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflow WHERE ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId);
	--And populate application workflow steps
	INSERT INTO dbo.[ApplicationWorkflowStep] (ApplicationWorkflowId, StepTypeId, StepName, Active, StepOrder, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT ApplicationWorkflow.ApplicationWorkflowId, WorkflowStep.StepTypeId, WorkflowStep.StepName, WorkflowStep.ActiveDefault, WorkflowStep.StepOrder, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
	WorkflowStep ON ApplicationWorkflow.WorkflowId = WorkflowStep.WorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE WorkflowStep.DeletedFlag = 0 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationStage.ReviewStageId = @PreReviewStage
		AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStep WHERE ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId AND StepTypeId = WorkflowStep.StepTypeId);
	
	--Insert elements for all steps that have been set up
	INSERT INTO dbo.[ApplicationWorkflowStepElement] (ApplicationWorkflowStepId, ApplicationTemplateElementId, ClientScoringId, AccessLevelId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, MechanismTemplateElementScoring.ClientScoringId, @AccessLevelId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
	FROM         ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
					ViewApplicationStage ApplicationStage On ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
					ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId LEFT OUTER JOIN
                      MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND 
                      ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId AND MechanismTemplateElementScoring.DeletedFlag = 0
	WHERE PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationStage.ReviewStageId = @PreReviewStage
		AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId)
	
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
	WHERE PanelUserAssignment.SessionPanelId = @SessionPanelId AND PanelApplicationReviewerAssignment.DeletedFlag = 0 AND ApplicationStage.ReviewStageId = @PreReviewStage
		AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepAssignment WHERE UserId = PanelUserAssignment.UserId AND ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId);
END

GO


GRANT EXECUTE
    ON OBJECT::[dbo].[uspBeginReviewerWorkflowPanel] TO [NetSqlAzMan_Users], [web-p2rmis]
    AS [dbo];