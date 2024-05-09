CREATE TRIGGER [PrgPartRoleTypeSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Part_Role_Type
FOR INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
		UPDATE [$(DatabaseName)].dbo.ClientRole
		SET RoleName = inserted.Part_Role_Desc, ActiveFlag = inserted.Active,
			ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
			[$(DatabaseName)].[dbo].[Client] Client ON inserted.Client = Client.ClientAbrv INNER JOIN
			[$(DatabaseName)].[dbo].[ClientRole] ClientRole ON inserted.Role_ID = ClientRole.LegacyRoleId AND
			Client.ClientID = ClientRole.ClientId LEFT OUTER JOIN
			[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	ELSE
		INSERT INTO [$(DatabaseName)].[dbo].[ClientRole]
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
		SELECT Client.ClientId, inserted.Role_ID, inserted.Part_Role_Type, inserted.Part_Role_Desc, inserted.Active, CASE WHEN inserted.Part_Role_Type = 'CR' OR inserted.Part_Role_Type = 'BS' THEN 0 ELSE 1 END,
		VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Client] Client ON inserted.Client = Client.ClientAbrv LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
END
