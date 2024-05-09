CREATE PROCEDURE [dbo].[uspCreateNewClient]
	@ClientAbbreviation varchar(10),
	@ClientDescription varchar(100),
	@ClientIdToClone int = null --Optional if blank client child data must be set up manually
AS
BEGIN
DECLARE @NewClientId int = 0,
@CurrentDateTime DateTime2(0) = dbo.GetP2rmisDateTime(),
@AdminUserId int = 10,
@NewClientRegistrationId int = 0;
INSERT INTO [Client]
           ([ClientAbrv]
           ,[ClientDesc])
     VALUES
           (@ClientAbbreviation
           ,@ClientDescription)
SELECT @NewClientId = SCOPE_IDENTITY();
IF @ClientIdToClone > 0 AND @NewClientId > 0
BEGIN
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
SELECT @NewClientId
			 ,[LegacyPartTypeId]
           ,[ParticipantTypeAbbreviation]
           ,[ParticipantTypeName]
           ,[ParticipantScope]
           ,[ActiveFlag]
           ,[ReviewerFlag]
           ,[ChairpersonFlag]
           ,[ConsumerFlag]
           ,@AdminUserId
           ,@CurrentDateTime
           ,@AdminUserId
           ,@CurrentDateTime
FROM ClientParticipantType WHERE ClientId = @ClientIdToClone;
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
SELECT @NewClientId
			,NULL --This needs updated seperately
           ,[RoleAbbreviation]
           ,[RoleName]
           ,[ActiveFlag]
           ,[SpecialistFlag]
		   ,@AdminUserId
           ,@CurrentDateTime
           ,@AdminUserId
           ,@CurrentDateTime
FROM ClientRole WHERE ClientId = @ClientIdToClone;
--Copy assignment types for new client
INSERT INTO [dbo].[ClientAssignmentType]
           ([ClientId]
           ,[AssignmentTypeId]
           ,[AssignmentAbbreviation]
           ,[AssignmentLabel])
SELECT @NewClientId
		   ,[AssignmentTypeId]
           ,[AssignmentAbbreviation]
           ,[AssignmentLabel]
FROM ClientAssignmentType WHERE ClientId = @ClientIdToClone;
--Copy users for new client
INSERT INTO  UserClient ([UserID]
           ,[ClientID]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[CreatedBy]
           ,[CreatedDate])
SELECT UserId, @NewClientId
			,@AdminUserId
           ,@CurrentDateTime
           ,@AdminUserId
           ,@CurrentDateTime
FROM ViewUserClient WHERE ClientId = @ClientIdToClone;
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
SELECT @NewClientId,[ReviewStageId]
           ,[WorkflowName]
           ,[WorkflowDescription]
		   ,@AdminUserId
           ,@CurrentDateTime
           ,@AdminUserId
           ,@CurrentDateTime
FROM Workflow WHERE ClientId = @ClientIdToClone AND DeletedFlag = 0;
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
		,@AdminUserId
        ,@CurrentDateTime
        ,@AdminUserId
        ,@CurrentDateTime
FROM Workflow ExistingWorkflow INNER JOIN
Workflow NewWorkflow ON ExistingWorkflow.WorkflowName = NewWorkflow.WorkflowName INNER JOIN
WorkflowStep ON ExistingWorkflow.WorkflowId = WorkflowStep.WorkflowId
WHERE ExistingWorkflow.DeletedFlag = 0 AND WorkflowStep.DeletedFlag = 0 AND ExistingWorkflow.ClientId = @ClientIdToClone AND NewWorkflow.ClientId = @NewClientId;
INSERT INTO [dbo].[ClientDefaultWorkflow]
           ([ClientId]
           ,[WorkflowId]
           ,[ReviewStatusId])
SELECT @NewClientId, WorkflowId, ReviewStatusId
FROM ClientDefaultWorkflow
WHERE ClientId = @ClientIdToClone;
--Copy scoring scales for new client
INSERT INTO [dbo].[ClientScoringScaleLegend]
           ([ClientId]
           ,[LegendName])
SELECT @NewClientId, LegendName
FROM ClientScoringScaleLegend
WHERE ClientId = @ClientIdToClone;
INSERT INTO [dbo].[ClientScoringScaleLegendItem]
           ([ClientScoringScaleLegendId]
           ,[LegendItemLabel]
           ,[HighValueLabel]
           ,[LowValueLabel]
           ,[SortOrder])
SELECT LegendNew.ClientScoringScaleLegendId, [LegendItemLabel]
           ,[HighValueLabel]
           ,[LowValueLabel]
           ,[SortOrder]
FROM ClientScoringScaleLegend LegendNew INNER JOIN
ClientScoringScaleLegend LegendOld ON LegendNew.LegendName = LegendOld.LegendName INNER JOIN
ClientScoringScaleLegendItem ON LegendOld.ClientScoringScaleLegendId = ClientScoringScaleLegendItem.ClientScoringScaleLegendId
WHERE LegendNew.ClientId = @NewClientId AND LegendOld.ClientId = @ClientIdToClone;
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
SELECT @NewClientId, NewClientScoringScaleLegend.ClientScoringScaleLegendId, [ScoreType]
           ,[HighValue]
           ,[HighValueDescription]
           ,[MiddleValue]
           ,[MiddleValueDescription]
           ,[LowValue]
           ,[LowValueDescription]
           ,@AdminUserId
        ,@CurrentDateTime
        ,@AdminUserId
        ,@CurrentDateTime
FROM ClientScoringScale LEFT OUTER JOIN
ClientScoringScaleLegend ON ClientScoringScale.ClientScoringScaleLegendId = ClientScoringScaleLegend.ClientScoringScaleLegendId LEFT OUTER JOIN
ClientScoringScaleLegend NewClientScoringScaleLegend ON ClientScoringScaleLegend.LegendName = NewClientScoringScaleLegend.LegendName AND NewClientScoringScaleLegend.ClientId = @NewClientId
WHERE ClientScoringScale.ClientId = @ClientIdToClone;
INSERT INTO [dbo].[ClientScoringScaleAdjectival]
           ([ClientScoringId]
           ,[ScoreLabel]
           ,[NumericEquivalent]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ClientScoringScaleNew.ClientScoringId, [ScoreLabel] ,[NumericEquivalent], @AdminUserId
        ,@CurrentDateTime
        ,@AdminUserId
        ,@CurrentDateTime
FROM ClientScoringScale INNER JOIN
ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId INNER JOIN
ClientScoringScale ClientScoringScaleNew ON ClientScoringScale.ScoreType = ClientScoringScaleNew.ScoreType AND ISNULL(ClientScoringScale.HighValue, 0) = ISNULL(ClientScoringScaleNew.HighValue, 0) AND ISNULL(ClientScoringScale.HighValueDescription, '') = ISNULL(ClientScoringScaleNew.HighValueDescription, '')
	AND ISNULL(ClientScoringScale.MiddleValue, 0)  = ISNULL(ClientScoringScaleNew.MiddleValue, 0) AND ISNULL(ClientScoringScale.MiddleValueDescription, '') = ISNULL(ClientScoringScaleNew.MiddleValueDescription, '') AND ISNULL(ClientScoringScale.LowValue, 0) = ISNULL(ClientScoringScaleNew.LowValue, 0) AND ISNULL(ClientScoringScale.LowValueDescription, '') = ISNULL(ClientScoringScaleNew.LowValueDescription, '')
WHERE ClientScoringScale.ClientId = @ClientIdToClone AND ClientScoringScaleNew.ClientId = @NewClientId;
INSERT INTO [dbo].[ClientApplicationInfoType]
           ([ClientId]
           ,[InfoTypeDescription])
SELECT @NewClientId, InfoTypeDescription
FROM ClientApplicationInfoType
WHERE ClientId = @ClientIdToClone;
INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[ApplicationPersonnelTypeAbbreviation]
           ,[CoiFlag])
SELECT @NewClientId, [ApplicationPersonnelType]
           ,[ApplicationPersonnelTypeAbbreviation]
           ,[CoiFlag]
FROM ClientApplicationPersonnelType
WHERE ClientId = @ClientIdToClone;
INSERT INTO [dbo].[ClientApplicationTextType]
           ([ClientId]
           ,[TextType])
SELECT @NewClientId, [TextType]
FROM ClientApplicationTextType
WHERE ClientId = @ClientIdToClone;
--Client expertise rating
INSERT INTO [dbo].[ClientExpertiseRating]
           ([ClientId]
           ,[RatingAbbreviation]
           ,[RatingName]
           ,[RatingDescription]
           ,[ConflictFlag])
SELECT @NewClientId
		,[RatingAbbreviation]
        ,[RatingName]
        ,[RatingDescription]
        ,[ConflictFlag]
FROM ClientExpertiseRating
WHERE ClientId = @ClientIdToClone;
INSERT INTO [dbo].[ClientReviewerEvaluationGroup]
           ([ClientId]
           ,[ReviewerEvaluationGroupName])
SELECT @NewClientId, ReviewerEvaluationGroupName
FROM ClientReviewerEvaluationGroup
WHERE ClientId = @ClientIdToClone;
INSERT INTO [dbo].[ReviewerEvaluationGroupGuidance]
           ([ClientReviewerEvaluationGroupId]
           ,[Rating]
           ,[RatingDescription])
SELECT NewClientEvaluationGroup.[ClientReviewerEvaluationGroupId], [Rating], [RatingDescription]
FROM ReviewerEvaluationGroupGuidance INNER JOIN
	ClientReviewerEvaluationGroup ON ReviewerEvaluationGroupGuidance.ClientReviewerEvaluationGroupId = ClientReviewerEvaluationGroup.ClientReviewerEvaluationGroupId INNER JOIN
	ClientReviewerEvaluationGroup NewClientEvaluationGroup ON ClientReviewerEvaluationGroup.ReviewerEvaluationGroupName = NewClientEvaluationGroup.ReviewerEvaluationGroupName
WHERE ClientReviewerEvaluationGroup.ClientId = @ClientIdToClone AND NewClientEvaluationGroup.ClientId = @NewClientId;
INSERT INTO [dbo].[ClientCoiType]
           ([ClientId]
           ,[CoiTypeName]
           ,[CoiTypeDescription])
SELECT @NewClientId, CoiTypeName, CoiTypeDescription
FROM ClientCoiType
WHERE ClientId = @ClientIdToClone;
--Set up registration
INSERT INTO [dbo].[ClientRegistration]
           ([ClientId]
           ,[ActiveFlag])
     VALUES
           (@NewClientId, 1);
SELECT @NewClientRegistrationId = SCOPE_IDENTITY();
IF (@NewClientRegistrationId > 0)
BEGIN
INSERT INTO [dbo].[ClientRegistrationDocument]
           ([ClientRegistrationId]
           ,[RegistrationDocumentTypeId]
           ,[DocumentName]
           ,[DocumentAbbreviation]
           ,[SortOrder]
           ,[DocumentRoute]
           ,[RequiredFlag]
           ,[DocumentVersion]
           ,[DocumentUpdateDate]
           ,[ConfirmationText]
           ,[ReportFileName])
SELECT @NewClientRegistrationId ,[RegistrationDocumentTypeId]
           ,[DocumentName]
           ,[DocumentAbbreviation]
           ,[SortOrder]
           ,[DocumentRoute]
           ,[RequiredFlag]
           ,[DocumentVersion]
           ,[DocumentUpdateDate]
           ,[ConfirmationText]
           ,[ReportFileName]
FROM ClientRegistrationDocument INNER JOIN
ClientRegistration ON ClientRegistrationDocument.ClientRegistrationId = ClientRegistration.ClientRegistrationId
WHERE ClientRegistration.ClientId = @ClientIdToClone AND ClientRegistration.ActiveFlag = 1;
INSERT INTO [dbo].[ClientRegistrationDocumentItem]
           ([ClientRegistrationDocumentId]
           ,[RegistrationDocumentItemId]
           ,[RequiredFlag]
           ,[RequiredMessage])
SELECT NewClientRegistration.ClientRegistrationDocumentId
		   ,[RegistrationDocumentItemId]
           ,ClientRegistrationDocumentItem.[RequiredFlag]
           ,[RequiredMessage]
FROM ClientRegistrationDocument INNER JOIN
	ClientRegistrationDocumentItem ON ClientRegistrationDocument.ClientRegistrationDocumentId = ClientRegistrationDocumentItem.ClientRegistrationDocumentId INNER JOIN
	ClientRegistrationDocument NewClientRegistration ON ClientRegistrationDocument.DocumentName = NewClientRegistration.DocumentName INNER JOIN
	ClientRegistration ON ClientRegistrationDocument.ClientRegistrationId = ClientRegistration.ClientRegistrationId
WHERE ClientRegistration.ClientId = @ClientIdToClone AND NewClientRegistration.ClientRegistrationId = @NewClientRegistrationId;
END
--Role participant type
INSERT INTO [dbo].[RoleParticipantType]
           ([SystemRoleId]
           ,[ClientId]
           ,[ClientParticipantTypeId])
SELECT RoleParticipantType.SystemRoleId, @NewClientId, ClientParticipantTypeNew.ClientParticipantTypeId
FROM RoleParticipantType INNER JOIN
ClientParticipantType ON RoleParticipantType.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
ClientParticipantType ClientParticipantTypeNew ON ClientParticipantType.ParticipantTypeAbbreviation = ClientParticipantTypeNew.ParticipantTypeAbbreviation
WHERE ClientParticipantType.ClientId = @ClientIdToClone AND ClientParticipantTypeNew.ClientId = @NewClientId;

--REMEMBER TO COPY Any necessary lookup scripts to database project after running
END
END
