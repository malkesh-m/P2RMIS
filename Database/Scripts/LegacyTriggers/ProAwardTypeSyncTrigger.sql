CREATE TRIGGER [ProAwardTypeSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRO_Award_Type]
FOR UPDATE
AS
BEGIN
	SET NOCOUNT ON
	UPDATE [$(DatabaseName)].[dbo].[ClientAwardType]
	SET AwardAbbreviation = inserted.Short_Desc, AwardDescription = inserted.Description, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ClientAwardType] ClientAwardType ON inserted.Award_Type = ClientAwardType.LegacyAwardTypeId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
END
