CREATE TRIGGER [ProEvaluationCriteriaSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRO_Evaluation_Criteria]
FOR UPDATE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].dbo.ClientElement
	SET ElementAbbreviation = inserted.Eval_Abrv, ElementDescription = inserted.Description, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].dbo.ClientElement ClientElement ON inserted.Eval_Criteria = ClientElement.ElementIdentifier LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
END