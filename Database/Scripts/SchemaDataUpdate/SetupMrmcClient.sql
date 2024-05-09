--Copy participation types  for new client
INSERT INTO ClientParticipantType ([ClientId]
           ,[LegacyPartTypeId]
           ,[ParticipantTypeAbbreviation]
           ,[ParticipantTypeName]
           ,[ParticipantScope]
           ,[ActiveFlag]
           ,[ReviewerFlag]
           ,[ChairpersonFlag]
           ,[ConsumerFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT 23
			 ,[LegacyPartTypeId]
           ,[ParticipantTypeAbbreviation]
           ,[ParticipantTypeName]
           ,[ParticipantScope]
           ,[ActiveFlag]
           ,[ReviewerFlag]
           ,[ChairpersonFlag]
           ,[ConsumerFlag]
           ,10
           ,dbo.GetP2rmisDateTime()
           ,10
           ,dbo.GetP2rmisDateTime()
FROM ClientParticipantType WHERE ClientId = 19;
--Copy roles for new client
INSERT INTO [dbo].[ClientRole]
           ([ClientId]
           ,[LegacyRoleId]
           ,[RoleAbbreviation]
           ,[RoleName]
           ,[ActiveFlag]
           ,[SpecialistFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT 23
			,PRG_Part_Role_Type.Role_ID
           ,[RoleAbbreviation]
           ,[RoleName]
           ,[ActiveFlag]
           ,[SpecialistFlag]
		   ,10
           ,dbo.GetP2rmisDateTime()
           ,10
           ,dbo.GetP2rmisDateTime()
FROM ClientRole INNER JOIN 
[$(P2RMIS)].dbo.PRG_Part_Role_Type PRG_Part_Role_Type ON ClientRole.RoleName = PRG_Part_Role_Type.Part_Role_Desc
WHERE ClientId = 19 AND Client = 'MRMC';
--Copy workflows for new client
INSERT INTO [dbo].[Workflow]
           ([ClientId]
           ,[ReviewStageId]
           ,[WorkflowName]
           ,[WorkflowDescription]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT 23,[ReviewStageId]
           ,[WorkflowName]
           ,[WorkflowDescription]
		   ,10
           ,dbo.GetP2rmisDateTime()
           ,10
           ,dbo.GetP2rmisDateTime()
FROM Workflow WHERE ClientId = 19 AND DeletedFlag = 0;
INSERT INTO [dbo].[WorkflowStep]
           ([WorkflowId]
           ,[StepTypeId]
           ,[StepName]
           ,[StepOrder]
           ,[ActiveDefault]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT NewWorkflow.WorkflowId
		,[StepTypeId]
        ,[StepName]
        ,[StepOrder]
        ,[ActiveDefault]
		,10
        ,dbo.GetP2rmisDateTime()
        ,10
        ,dbo.GetP2rmisDateTime()
FROM Workflow ExistingWorkflow INNER JOIN
Workflow NewWorkflow ON ExistingWorkflow.WorkflowName = NewWorkflow.WorkflowName INNER JOIN
WorkflowStep ON ExistingWorkflow.WorkflowId = WorkflowStep.WorkflowId
WHERE ExistingWorkflow.DeletedFlag = 0 AND WorkflowStep.DeletedFlag = 0 AND ExistingWorkflow.ClientId = 19 AND NewWorkflow.ClientId = 23;
INSERT INTO [dbo].[ClientDefaultWorkflow]
           ([ClientId]
           ,[WorkflowId]
           ,[ReviewStatusId])
SELECT 23, WorkflowId, ReviewStatusId
FROM ClientDefaultWorkflow
WHERE ClientId = 19;
INSERT INTO [dbo].[ClientScoringScale]
           ([ClientId]
           ,[ClientScoringScaleLegendId]
           ,[ScoreType]
           ,[HighValue]
           ,[HighValueDescription]
           ,[MiddleValue]
           ,[MiddleValueDescription]
           ,[LowValue]
           ,[LowValueDescription]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT 23, NewClientScoringScaleLegend.ClientScoringScaleLegendId, [ScoreType]
           ,[HighValue]
           ,[HighValueDescription]
           ,[MiddleValue]
           ,[MiddleValueDescription]
           ,[LowValue]
           ,[LowValueDescription]
           ,10
        ,dbo.GetP2rmisDateTime()
        ,10
        ,dbo.GetP2rmisDateTime()
FROM ClientScoringScale LEFT OUTER JOIN
ClientScoringScaleLegend ON ClientScoringScale.ClientScoringScaleLegendId = ClientScoringScaleLegend.ClientScoringScaleLegendId LEFT OUTER JOIN
ClientScoringScaleLegend NewClientScoringScaleLegend ON ClientScoringScaleLegend.LegendName = NewClientScoringScaleLegend.LegendName AND NewClientScoringScaleLegend.ClientId = 23
WHERE ClientScoringScale.ClientId = 19;
INSERT INTO [dbo].[ClientScoringScaleAdjectival]
           ([ClientScoringId]
           ,[ScoreLabel]
           ,[NumericEquivalent]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ClientScoringScaleNew.ClientScoringId, [ScoreLabel] ,[NumericEquivalent], 10
        ,dbo.GetP2rmisDateTime()
        ,10
        ,dbo.GetP2rmisDateTime()
FROM ClientScoringScale INNER JOIN
ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId INNER JOIN
ClientScoringScale ClientScoringScaleNew ON ClientScoringScale.ScoreType = ClientScoringScaleNew.ScoreType AND ISNULL(ClientScoringScale.HighValue, 0) = ISNULL(ClientScoringScaleNew.HighValue, 0) AND ISNULL(ClientScoringScale.HighValueDescription, '') = ISNULL(ClientScoringScaleNew.HighValueDescription, '')
	AND ISNULL(ClientScoringScale.MiddleValue, 0)  = ISNULL(ClientScoringScaleNew.MiddleValue, 0) AND ISNULL(ClientScoringScale.MiddleValueDescription, '') = ISNULL(ClientScoringScaleNew.MiddleValueDescription, '') AND ISNULL(ClientScoringScale.LowValue, 0) = ISNULL(ClientScoringScaleNew.LowValue, 0) AND ISNULL(ClientScoringScale.LowValueDescription, '') = ISNULL(ClientScoringScaleNew.LowValueDescription, '')
WHERE ClientScoringScale.ClientId = 19 AND ClientScoringScaleNew.ClientId = 23;

--Child record catch-up (re-run ETL in this order)
--Disable the triggers first!
/*
--ClientProgram
--ClientMeeting
--ClientAwardType
--ClientElement
--ProgramYear
--ProgramMechanism
--ProgramEmailTemplate
--ProgramMeetingInformation
--TrainingDocument
--TrainingDocumentAccess
--MeetingSession
--SessionPanel
--ProgramPanel
--ProgramUserAssignment
--Application
--ApplicationBudget
--ApplicationInfo
--ApplicationPersonnel
--ApplicationText
--ApplicationCompliance
--PanelStage **
--PanelStageStep **
--MechanismTemplate
--MechanismTemplateElement
--MechanismTemplateElementScoring **
--PanelUserAssignment **
--MeetingRegistration
--MeetingRegistrationAttendance
--MeetingRegistrationHotel
--MeetingRegistrationTravel
--PanelApplication (a lot)
--ReviewerEvaluation
--PanelApplicationReviewerAssignment (7715!)
--PanelApplicationReviewerExpertise
--PanelApplicationReviewerCoiDetail
--ApplicationReviewStatus **
--ApplicationStage
--ApplicationStageStep
--ApplicationStageStepDiscussion
--ApplicationStageStepDiscussionComment

 ApplicationTemplate;
 ApplicationTemplateElement;
 ApplicationWorkflow;
 ApplicationWorkflowStep;
 ApplicationWorkflowStepAssignment;
 ApplicationWorkflowStepWorkLog;
 ApplicationWorkflowStepElement;
 ApplicationWorkflowStepElementContent;



-Run application workflow incremental update scripts which should catch the rest
*/