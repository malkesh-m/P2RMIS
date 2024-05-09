CREATE TRIGGER [ProEvaluationCriteriaMemberSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRO_Evaluation_Criteria_Member]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
		UPDATE [$(DatabaseName)].dbo.MechanismTemplateElement
		SET [InstructionText] = PRO_Evaluation_Criteria_Member.Sco_Description
		  ,[SortOrder] = inserted.Sort_Order
		  ,[SummarySortOrder] = inserted.SS_Sort_Order
		  ,[RecommendedWordCount] = inserted.Word_Count
		  ,[ScoreFlag] = inserted.Score_Flag
		  ,[TextFlag] = inserted.Text_Flag
		  ,[OverallFlag] = inserted.Overall_Eval
		  ,[ShowAbbreviationOnScoreboard] = inserted.Criteria_Display
		  ,[ModifiedBy] = VUN.UserId
		  ,[ModifiedDate] = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member PRO_Evaluation_Criteria_Member ON inserted.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
		[$(DatabaseName)].dbo.MechanismTemplateElement MechanismTemplateElement ON inserted.ECM_ID = MechanismTemplateElement.LegacyEcmId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE MechanismTemplateElement.DeletedFlag = 0
	END
	--INSERT (works for single insert only)
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
		--First check for ClientElement and insert if doesn't exist
		IF NOT EXISTS (Select * FROM inserted INNER JOIN
						[$(DatabaseName)].dbo.ViewProgramMechanism ProgramMechanism ON inserted.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
						[$(DatabaseName)].dbo.ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN 
						[$(DatabaseName)].dbo.ClientElement ClientElement ON inserted.Eval_Criteria = ClientElement.ElementIdentifier AND ClientAwardType.ClientId = ClientElement.ClientId) 
		BEGIN
			INSERT INTO [$(DatabaseName)].[dbo].[ClientElement]
           ([ClientId]
           ,[ElementTypeId]
           ,[ElementIdentifier]
           ,[ElementAbbreviation]
           ,[ElementDescription]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   SELECT ClientAwardType.ClientId, 1, inserted.Eval_Criteria, PRO_Evaluation_Criteria.Eval_Abrv, PRO_Evaluation_Criteria.Description, 
		   VUN.UserId, dbo.GetP2rmisDateTime(), VUN.UserId, dbo.GetP2rmisDateTime()
		   FROM inserted INNER JOIN
			[$(P2RMIS)].dbo.PRO_Evaluation_Criteria PRO_Evaluation_Criteria ON inserted.Eval_Criteria = PRO_Evaluation_Criteria.EVAL_CRITERIA INNER JOIN
			[$(DatabaseName)].dbo.ViewProgramMechanism ProgramMechanism ON inserted.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
			[$(DatabaseName)].dbo.ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId LEFT OUTER JOIN
			[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		END
		--Next check for MechanismTemplate and insert for stage 1 and 2 if doesn't exist
		IF NOT EXISTS (Select * FROM inserted INNER JOIN
						[$(DatabaseName)].dbo.ProgramMechanism ProgramMechanism ON inserted.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
						[$(DatabaseName)].dbo.MechanismTemplate MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId) 
		BEGIN
			INSERT INTO [$(DatabaseName)].[dbo].[MechanismTemplate]
           ([ProgramMechanismId]
           ,[ReviewStageId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   SELECT ProgramMechanism.ProgramMechanismId, ReviewStage.ReviewStageId, VUN.UserId, dbo.GetP2rmisDateTime(), VUN.UserId, dbo.GetP2rmisDateTime()
		   FROM inserted INNER JOIN
		   [$(DatabaseName)].dbo.ViewProgramMechanism ProgramMechanism ON inserted.ATM_ID = ProgramMechanism.LegacyAtmId CROSS JOIN
		   [$(DatabaseName)].dbo.ReviewStage ReviewStage LEFT OUTER JOIN
		   [$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		   WHERE ReviewStage.ReviewStageId IN (1,2)
		END
		--Insert the new MechanismTemplateElements
		INSERT INTO [$(DatabaseName)].[dbo].[MechanismTemplateElement]
           ([MechanismTemplateId]
           ,[LegacyEcmId]
           ,[ClientElementId]
           ,[InstructionText]
           ,[SortOrder]
		   ,[SummarySortOrder]
           ,[RecommendedWordCount]
           ,[ScoreFlag]
           ,[TextFlag]
           ,[OverallFlag]
		   ,[ShowAbbreviationOnScoreboard]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT MechanismTemplate.MechanismTemplateId, inserted.ECM_ID, ClientElement.ClientElementId, PRO_Evaluation_Criteria_Member.Sco_Description,
			inserted.Sort_Order, inserted.SS_Sort_Order,inserted.Word_Count, inserted.Score_Flag, inserted.Text_Flag, inserted.Overall_Eval, inserted.Criteria_Display, VUN.UserId, inserted.LAST_UPDATE_DATE,
			VUN.UserId, inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member PRO_Evaluation_Criteria_Member ON inserted.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
		[$(DatabaseName)].dbo.ViewProgramMechanism ProgramMechanism ON inserted.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
		[$(DatabaseName)].dbo.ViewMechanismTemplate MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].dbo.ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].dbo.ClientElement ClientElement ON ClientAwardType.ClientId = ClientElement.ClientId AND inserted.Eval_Criteria = ClientElement.ElementIdentifier LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepElement]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].dbo.MechanismTemplateElement MechanismTemplateElement ON deleted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationTemplateElement ApplicationTemplateElement ON MechanismTemplateElement.MechanismTemplateElementId = ApplicationTemplateElement.MechanismTemplateElementId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationTemplateElement.ApplicationTemplateElementId = ApplicationWorkflowStepElement.ApplicationTemplateElementId
	WHERE MechanismTemplateElement.DeletedFlag = 0 AND ApplicationTemplateElement.DeletedFlag = 0 AND ApplicationWorkflowStepElement.DeletedFlag = 0
	UPDATE [$(DatabaseName)].[dbo].[ApplicationTemplateElement]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].dbo.MechanismTemplateElement MechanismTemplateElement ON deleted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
	[$(DatabaseName)].dbo.ApplicationTemplateElement ApplicationTemplateElement ON MechanismTemplateElement.MechanismTemplateElementId = ApplicationTemplateElement.MechanismTemplateElementId
	WHERE MechanismTemplateElement.DeletedFlag = 0 AND ApplicationTemplateElement.DeletedFlag = 0
	UPDATE [$(DatabaseName)].[dbo].[MechanismTemplateElement]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].dbo.MechanismTemplateElement MechanismTemplateElement ON deleted.ECM_ID = MechanismTemplateElement.LegacyEcmId
	WHERE MechanismTemplateElement.DeletedFlag = 0
	
	END
END
