INSERT INTO .[dbo].[ClientElement]
           ([ClientId]
           ,[ElementTypeId]
           ,[ElementIdentifier]
           ,[ElementAbbreviation]
           ,[ElementDescription]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ClientAwardType.ClientId, 1, PRO_Evaluation_Criteria.Eval_Criteria, PRO_Evaluation_Criteria.Eval_Abrv, 
	PRO_Evaluation_Criteria.Description, VUN.UserId, PRO_Evaluation_Criteria.LAST_UPDATE_DATE
FROM	[$(P2RMIS)].dbo.PRO_Evaluation_Criteria INNER JOIN
	[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_Evaluation_Criteria.EVAL_Criteria = PRO_Evaluation_Criteria_Member.EVAL_Criteria INNER JOIN
	ProgramMechanism ON PRO_Evaluation_Criteria_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
	ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId LEFT JOIN
	ViewLegacyUserNameToUserId VUN ON PRO_Evaluation_Criteria.Last_Updated_By = VUN.UserName LEFT JOIN
	ClientElement ON ClientAwardType.ClientId = ClientElement.ClientId AND PRO_Evaluation_Criteria.Eval_Criteria = ClientElement.ElementIdentifier
WHERE ClientElement.ClientElementId IS NULL
GROUP BY ClientAwardType.ClientId, PRO_Evaluation_Criteria.Eval_Criteria, PRO_Evaluation_Criteria.Eval_Abrv, 
	PRO_Evaluation_Criteria.Description, VUN.UserId, PRO_Evaluation_Criteria.LAST_UPDATE_DATE