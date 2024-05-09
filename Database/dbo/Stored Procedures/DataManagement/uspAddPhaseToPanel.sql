CREATE PROCEDURE [dbo].[uspAddPreMeetingPhaseToPanel]
	@SessionPanelId int,
	@StepTypeId int,
	@PhaseStartDate datetime2(0), 
	@PhaseEndDate datetime2(0),
	@PhaseReOpenDate datetime2(0) = null,
	@PhaseReCloseDate datetime2(0)= null,
	@DeleteMeetingCritiques bit --Whether to delete any existing meeting critiques
AS
BEGIN
DECLARE @PreliminaryId int = 5,
@RevisedId int = 6,
@OnlineScoringId int = 7,
@PreStageId int = 1,
@MeetingStageId int = 2,
@StepCount int = 0,
@DateInterval int = -3
IF (@StepTypeId NOT IN (@PreliminaryId, @RevisedId, @OnlineScoringId))
	RETURN 'StepTypeId specified not supported. Please try again.'
BEGIN
--Calculate/set existing phase dates
SELECT @StepCount = Count(*)
FROM PanelStageStep INNER JOIN
PanelStage ON PanelStageStep.PanelStageId = PanelStage.PanelStageId
WHERE PanelStage.SessionPanelId = @SessionPanelId AND PanelStage.ReviewStageId = 1 AND PanelStageStep.DeletedFlag = 0
--Sets the dates to ensure existing phase do not overlap
UPDATE PanelStageStep SET StartDate = DATEADD(DAY, ((@StepCount + 1 - PanelStageStep.StepOrder) * @DateInterval), @PhaseStartDate), EndDate = DATEADD(DAY, ((@StepCount - PanelStageStep.StepOrder) * @DateInterval), @PhaseStartDate)
FROM PanelStageStep INNER JOIN
PanelStage ON PanelStageStep.PanelStageId = PanelStage.PanelStageId
WHERE PanelStage.SessionPanelId = @SessionPanelId AND PanelStage.ReviewStageId = 1 AND PanelStageStep.DeletedFlag = 0
--Sets the date for the meeting phase
UPDATE PanelStageStep SET StartDate = @PhaseEndDate, EndDate = DateAdd(DAY, -@DateInterval, @PhaseEndDate)
FROM PanelStageStep INNER JOIN
PanelStage ON PanelStageStep.PanelStageId = PanelStage.PanelStageId
WHERE PanelStage.SessionPanelId = @SessionPanelId AND PanelStage.ReviewStageId = 2 AND PanelStageStep.DeletedFlag = 0
UPDATE SessionPanel SET StartDate = @PhaseEndDate, EndDate = DateAdd(DAY, -@DateInterval, @PhaseEndDate)
WHERE SessionPanelId = @SessionPanelId
--Add the phase to PanelStageStep
INSERT INTO [dbo].[PanelStageStep]
           ([PanelStageId]
           ,[StepTypeId]
           ,[StepName]
           ,[StepOrder]
           ,[StartDate]
           ,[EndDate]
           ,[ReOpenDate]
           ,[ReCloseDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT PanelStage.PanelStageId, StepType.StepTypeId, StepType.StepTypeName, @StepCount + 1, @PhaseStartDate, @PhaseEndDate, @PhaseReOpenDate, @PhaseReCloseDate,
10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM PanelStage CROSS JOIN StepType
WHERE PanelStage.SessionPanelId = @SessionPanelId AND StepType.StepTypeId = @StepTypeId AND PanelStage.ReviewStageId = 1 AND PanelStage.DeletedFlag = 0
--Add the phase to ApplicationStageStep
INSERT INTO [dbo].[ApplicationStageStep]
			([ApplicationStageId]
           ,[PanelStageStepId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])	
SELECT ApplicationStage.ApplicationStageId, PanelStageStep.PanelStageStepId, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM PanelStage INNER JOIN
PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId INNER JOIN
PanelApplication ON PanelStage.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId
WHERE PanelStage.SessionPanelId = @SessionPanelId AND PanelStageStep.StepTypeId = @StepTypeId AND PanelStage.ReviewStageId = 1 AND ApplicationStage.ReviewStageId = 1
AND PanelStageStep.DeletedFlag = 0
--Add the phase to all child ApplicationWorkflowSteps
INSERT INTO [dbo].[ApplicationWorkflowStep]
([ApplicationWorkflowId]
           ,[StepTypeId]
           ,[StepName]
           ,[Active]
           ,[StepOrder]
           ,[Resolution]
           ,[ResolutionDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [ApplicationWorkflow].ApplicationWorkflowId, [StepType].StepTypeId, [StepType].StepTypeName, CASE WHEN StepType.StepTypeId = @OnlineScoringId THEN 0 ELSE 1 END,
@StepCount + 1, CASE WHEN StepType.StepTypeId = @OnlineScoringId AND ApplicationWorkflowStep.Resolution = 1 THEN 1 ELSE 0 END, CASE WHEN StepType.StepTypeId = @OnlineScoringId AND ApplicationWorkflowStep.Resolution = 1 THEN dbo.GetP2rmisDateTime() ELSE NULL END, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM 
	PanelApplication INNER JOIN
	ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
	ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId CROSS JOIN
	StepType INNER JOIN
	ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND ApplicationWorkflowStep.StepOrder = @StepCount
WHERE StepType.StepTypeId = @StepTypeId AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflow.DeletedFlag = 0 AND ApplicationStage.ReviewStageId = 1 AND ApplicationWorkflowStep.DeletedFlag = 0

--Add the phase to mechanism template element scoring based on previous step (essentially just cloning the previous step's scoring setup) (should not insert if already existing)
INSERT INTO [dbo].[MechanismTemplateElementScoring]
           ([MechanismTemplateElementId]
           ,[ClientScoringId]
           ,[StepTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [MechanismTemplateElementScoring].MechanismTemplateElementId, [MechanismTemplateElementScoring].ClientScoringId, @StepTypeId,
[MechanismTemplateElementScoring].CreatedBy, MechanismTemplateElementScoring.CreatedDate, MechanismTemplateElementScoring.ModifiedBy, MechanismTemplateElementScoring.ModifiedDate
FROM MechanismTemplateElementScoring INNER JOIN
MechanismTemplateElement ON MechanismTemplateElementScoring.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
MechanismTemplate ON MechanismTemplateElement.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
ProgramMechanism ON MechanismTemplate.ProgramMechanismId = ProgramMechanism.ProgramMechanismId
WHERE ProgramMechanism.ProgramMechanismId IN 
	(SELECT DISTINCT [Application].ProgramMechanismId 
	FROM [Application] INNER JOIN 
	PanelApplication ON [Application].ApplicationId = [PanelApplication].ApplicationId
	WHERE PanelApplication.SessionPanelId = @SessionPanelId)
AND MechanismTemplateElementScoring.StepTypeId IN
	(Select TOP(1) StepTypeId 
	FROM PanelStageStep INNER JOIN
	PanelStage ON PanelStageStep.PanelStageId = PanelStage.PanelStageId
	WHERE PanelStage.SessionPanelId = @SessionPanelId AND PanelStage.ReviewStageId = 1 AND PanelStageStep.StepTypeId <> @StepTypeId
	ORDER BY PanelStageStep.StepOrder DESC)
AND MechanismTemplateElementScoring.DeletedFlag = 0
AND NOT EXISTS (Select 'X' FROM MechanismTemplateElementScoring MTES WHERE MTES.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId AND MTES.StepTypeId = @StepTypeId)
--Insert elements for all steps that have been set up
	INSERT INTO dbo.[ApplicationWorkflowStepElement] (ApplicationWorkflowStepId, ApplicationTemplateElementId, ClientScoringId, AccessLevelId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, MechanismTemplateElementScoring.ClientScoringId, 1, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
	FROM         ApplicationWorkflow INNER JOIN
                      ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ApplicationTemplateElement ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId LEFT OUTER JOIN
                      MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND 
                      ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId AND MechanismTemplateElementScoring.DeletedFlag = 0 INNER JOIN
					  ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
					  PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationStage.ReviewStageId = 1 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStep.StepTypeId = @StepTypeId AND ApplicationTemplateElement.DeletedFlag = 0 AND ApplicationWorkflow.DeletedFlag = 0 AND ApplicationWorkflowStep.DeletedFlag = 0; 
	--Insert assignments for reviewer at the step level
	INSERT INTO dbo.[ApplicationWorkflowStepAssignment] (ApplicationWorkflowStepId, UserId, AssignmentId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, ClientAssignmentType.AssignmentTypeId, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflow INNER JOIN
	PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
	ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	PanelApplicationReviewerAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	ApplicationStage.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
	ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
	ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
	PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationStage.ReviewStageId = 1 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStep.StepTypeId = @StepTypeId 
	AND PanelApplicationReviewerAssignment.DeletedFlag = 0 AND ApplicationWorkflow.DeletedFlag = 0 AND ApplicationWorkflowStep.DeletedFlag = 0;
--Delete future critiques from meeting phase if specified
IF (@DeleteMeetingCritiques = 1)
BEGIN
	UPDATE ApplicationWorkflowStepElementContent SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStepElementContent INNER JOIN
		ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
		ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
		ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationStage.ReviewStageId = 2 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElementContent.DeletedFlag = 0
	UPDATE ApplicationWorkflowStepElement SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStepElement INNER JOIN
		ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
		ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationStage.ReviewStageId = 2 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepElement.DeletedFlag = 0
	UPDATE ApplicationWorkflowStepAssignment SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStepAssignment INNER JOIN
		ApplicationWorkflowStep ON ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
		ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationStage.ReviewStageId = 2 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStepAssignment.DeletedFlag = 0
	UPDATE ApplicationWorkflowStep SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflowStep INNER JOIN
		ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationStage.ReviewStageId = 2 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflowStep.DeletedFlag = 0
	UPDATE ApplicationWorkflow SET DeletedBy = 10, DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM ApplicationWorkflow INNER JOIN
		ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
	WHERE ApplicationStage.ReviewStageId = 2 AND PanelApplication.SessionPanelId = @SessionPanelId AND ApplicationWorkflow.DeletedFlag = 0
END
--Push content to new phase if previous phase was submitted
INSERT INTO ApplicationWorkflowStepElementContent
		 ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[Abstain]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		 SELECT ApplicationWorkflowStepElement2.ApplicationWorkflowStepElementId, ApplicationWorkflowStepElementContentCurrent.Score, ApplicationWorkflowStepElementContentCurrent.ContentText, 
		CASE WHEN ApplicationWorkflowStepElementContentCurrent.Score IS NULL AND ApplicationWorkflowStepElementContentCurrent.ContentText = 'n/a' THEN 1 ELSE 0 END,
		 ApplicationWorkflowStepElementContentCurrent.CreatedBy, dbo.GetP2rmisDateTime(), ApplicationWorkflowStepElementContentCurrent.ModifiedBy, dbo.GetP2rmisDateTime()
		 FROM ApplicationWorkflowStep INNER JOIN
		 ViewApplicationWorkflowStep ApplicationWorkflowStep2 ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflowStep2.ApplicationWorkflowId AND
		 ApplicationWorkflowStep.StepOrder + 1 = ApplicationWorkflowStep2.StepOrder INNER JOIN
		 ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
		 ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement2 ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement2.ApplicationTemplateElementId AND
		 ApplicationWorkflowStep2.ApplicationWorkflowStepId = ApplicationWorkflowStepElement2.ApplicationWorkflowStepId INNER JOIN
		 ViewApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContentCurrent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContentCurrent.ApplicationWorkflowStepElementId INNER JOIN
		 ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
		 ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
		 PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId
		 WHERE PanelApplication.SessionPanelId = @SessionPanelId AND
		 ApplicationStage.ReviewStageId = 1 AND
		 ApplicationWorkflowStep2.StepTypeId = @StepTypeId AND
		 ApplicationWorkflowStep.Resolution = 1 AND
		 ApplicationWorkflowStep.DeletedFlag = 0 AND ApplicationWorkflowStepElement.DeletedFlag = 0
END
END
