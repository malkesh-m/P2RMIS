CREATE PROCEDURE [dbo].[uspChangeApplicationMechanism]
	@LogNumber varchar(12),
	@ProgramMechanismId int
AS
BEGIN
--Program Mechanism
UPDATE [Application]
SET ProgramMechanismId = @ProgramMechanismId
WHERE [Application].LogNumber = @LogNumber AND [Application].DeletedFlag = 0;
--Application Template
UPDATE [ApplicationTemplate]
SET MechanismTemplateId = (Select TOP(1) ViewMechanismTemplate.MechanismTemplateId FROM ViewMechanismTemplate WHERE ProgramMechanismId = @ProgramMechanismId AND ReviewStageId = ViewApplicationStage.ReviewStageId)
FROM [ApplicationTemplate] INNER JOIN
[ViewApplicationStage] ON ApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
[ViewPanelApplication] ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
[ViewApplication] ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplication.LogNumber = @LogNumber AND ApplicationTemplate.DeletedFlag = 0;
--Application Template Element (Exists in old but not new so soft delete from old)
UPDATE [ApplicationTemplateElement]
SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10
FROM ApplicationTemplateElement INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
ViewApplicationTemplate ApplicationTemplate ON ApplicationTemplateElement.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId INNER JOIN
ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
ViewApplication Application ON PanelApplication.ApplicationId = [Application].ApplicationId
WHERE Application.LogNumber = @LogNumber AND ApplicationTemplateElement.DeletedFlag = 0 AND MechanismTemplateElement.ClientElementId NOT IN
(Select ViewMechanismTemplateElement.ClientElementId
FROM ViewMechanismTemplateElement INNER JOIN
ViewMechanismTemplate ON ViewMechanismTemplateElement.MechanismTemplateId = ViewMechanismTemplate.MechanismTemplateId
WHERE ViewMechanismTemplate.ProgramMechanismId = @ProgramMechanismId);
--Application Template Element (Exists in new but not old so insert)
INSERT INTO [dbo].[ApplicationTemplateElement]
           ([ApplicationTemplateId]
           ,[MechanismTemplateElementId]
           ,[PanelApplicationReviewerAssignmentId]
           ,[DiscussionNoteFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationTemplate.ApplicationTemplateId, MechanismTemplateElement.MechanismTemplateElementId, NULL, 0, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM ViewApplicationTemplate ApplicationTemplate INNER JOIN
ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
ViewApplication Application ON PanelApplication.ApplicationId = [Application].ApplicationId INNER JOIN
ViewMechanismTemplate MechanismTemplate ON ApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId
WHERE Application.LogNumber = @LogNumber AND MechanismTemplateElement.ClientElementId NOT IN
(Select ViewMechanismTemplateElement.ClientElementId
FROM ViewMechanismTemplateElement INNER JOIN
ViewApplicationTemplateElement ON ViewMechanismTemplateElement.MechanismTemplateElementId = ViewApplicationTemplateElement.MechanismTemplateElementId
WHERE ViewApplicationTemplateElement.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId);
--Application Template Element (Matched)
UPDATE [ApplicationTemplateElement]
SET MechanismTemplateElementId = (Select TOP(1) ViewMechanismTemplateElement.MechanismTemplateElementId FROM ViewMechanismTemplateElement
									INNER JOIN ViewMechanismTemplate ON ViewMechanismTemplateElement.MechanismTemplateId = ViewMechanismTemplate.MechanismTemplateId
									WHERE ProgramMechanismId = @ProgramMechanismId AND ReviewStageId = ViewApplicationStage.ReviewStageId AND ClientElementId = MechanismTemplateElement.ClientElementId)
FROM [ApplicationTemplateElement] INNER JOIN
[ViewMechanismTemplateElement] MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
[ViewApplicationTemplate] ON ApplicationTemplateElement.ApplicationTemplateId = ViewApplicationTemplate.ApplicationTemplateId INNER JOIN
[ViewApplicationStage] ON ViewApplicationTemplate.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
[ViewPanelApplication] ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
[ViewApplication] ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
WHERE ViewApplication.LogNumber = @LogNumber AND ApplicationTemplateElement.DeletedFlag = 0;

--Fix elements at workflow level
--Update ApplicationWorkflowStepElement records as needed
--Soft delete if element does not exist in new mech template
UPDATE ApplicationWorkflowStepElement
SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM ApplicationWorkflowStepElement INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
	ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = [Application].ApplicationId
WHERE Application.LogNumber = @LogNumber AND ApplicationWorkflowStepElement.ApplicationTemplateElementId NOT IN (Select ApplicationTemplateElementId FROM ViewApplicationTemplateElement INNER JOIN ViewApplicationTemplate ON ViewApplicationTemplateElement.ApplicationTemplateId = ViewApplicationTemplate.ApplicationTemplateId WHERE ViewApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId);
--Insert if element does exist in new template
INSERT INTO [dbo].[ApplicationWorkflowStepElement]
           ([ApplicationWorkflowStepId]
           ,[ApplicationTemplateElementId]
           ,[AccessLevelId]
           ,[ClientScoringId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, 1, MechanismTemplateElementScoring.ClientScoringId, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
	FROM ViewApplicationWorkflowStep ApplicationWorkflowStep INNER JOIN
	ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId INNER JOIN
	ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
	ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId LEFT OUTER JOIN
	ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = [Application].ApplicationId
WHERE Application.LogNumber = @LogNumber AND ApplicationTemplateElement.ApplicationTemplateElementId NOT IN (Select ApplicationTemplateElementId FROM ViewApplicationWorkflowStepElement WHERE ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId);
END