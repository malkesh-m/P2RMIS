INSERT INTO [dbo].[MechanismTemplateElement]
           ([MechanismTemplateId]
           ,[ClientElementId]
           ,[LegacyEcmId]
           ,[InstructionText]
           ,[SortOrder]
           ,[RecommendedWordCount]
           ,[ScoreFlag]
           ,[TextFlag]
           ,[OverallFlag]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT	MechanismTemplate.MechanismTemplateId, ClientElement.ClientElementId, PRO_Evaluation_Criteria_Member.ECM_ID,
	PRO_Evaluation_Criteria_Member.SCO_Description, PRO_Evaluation_Criteria_Member.SORT_ORDER,
	PRO_Evaluation_Criteria_Member.Word_Count, PRO_Evaluation_Criteria_Member.Score_Flag,
	PRO_Evaluation_Criteria_Member.Text_Flag, PRO_Evaluation_Criteria_Member.Overall_Eval,
	VUN.UserId, PRO_Evaluation_Criteria_Member.LAST_UPDATE_DATE
FROM	 ViewMechanismTemplate MechanismTemplate INNER JOIN
	ViewProgramMechanism ProgramMechanism ON MechanismTemplate.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
	ViewProgramYear ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
	ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
	[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON MechanismTemplate.MechanismId = PRO_Evaluation_Criteria_Member.ATM_ID INNER JOIN
	ClientElement ON PRO_Evaluation_Criteria_Member.EVAL_Criteria = ClientElement.ElementIdentifier AND ClientProgram.ClientId = ClientElement.ClientId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON PRO_Evaluation_Criteria_Member.LAST_UPDATED_BY = VUN.UserName 
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplateElement WHERE MechanismTemplateId = MechanismTemplate.MechanismTemplateId AND LegacyEcmId = PRO_Evaluation_Criteria_Member.ECM_ID)
ORDER BY MechanismTemplateId, ClientElementId