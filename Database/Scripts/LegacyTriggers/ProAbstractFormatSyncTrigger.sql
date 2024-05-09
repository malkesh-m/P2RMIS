CREATE TRIGGER [ProAbstractFormatSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRO_Abstract_Format]
FOR INSERT, UPDATE
AS
BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ProgramMechanism]
	SET AbstractFormat = inserted.Abstract_Type, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ProgramMechanism] ProgramMechanism ON inserted.ATM_ID = ProgramMechanism.LegacyAtmId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
END
