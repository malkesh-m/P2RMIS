/*
Disabled - No use case for transferring critique information from 1.0 to 2.0 in real time


CREATE TRIGGER [PrgCritiquePhaseSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRG_Critique_Phase]
FOR INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @ApplicationWorkflowStepId int,
	@UID int
	--Get ApplicationWorkflowStepId corresponding to the modified critique phase
	SELECT @ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId, @UID = VUN.UserId
	FROM inserted INNER JOIN 
	[$(P2RMIS)].dbo.PRG_Critiques PRG_Critiques ON inserted.Critique_ID = PRG_Critiques.Critique_ID INNER JOIN
	[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PRG_Critiques.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationStage ApplicationStage ON PanelApplicationReviewerAssignment.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = 1 INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
	PanelApplicationReviewerAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId AND
	inserted.Scoring_Phase = CASE ApplicationWorkflowStep.StepTypeId WHEN 5 THEN 'initial' WHEN 6 THEN 'revised' WHEN 7 THEN 'meeting' END LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	WHERE ApplicationWorkflowStep.DeletedFlag = 0
	--Critique phase is being submitted
	IF EXISTS (Select * From inserted WHERE Date_Submitted IS NOT NULL)
	BEGIN
		IF @ApplicationWorkflowStepId IS NOT NULL
		BEGIN
			EXEC [$(DatabaseName)].dbo.uspSubmitApplicationWorkflowStep @ApplicationWorkflowStepId, @UID
		END
	END
	ELSE
	BEGIN
		IF @ApplicationWorkflowStepId IS NOT NULL
		BEGIN
			EXEC [$(DatabaseName)].dbo.uspResetApplicationWorkflowStep @ApplicationWorkflowStepId, @UID
		END
	END
END
*/