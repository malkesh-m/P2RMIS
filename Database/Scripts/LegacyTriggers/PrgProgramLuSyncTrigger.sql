CREATE TRIGGER [PrgProgramLuSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Program_LU
FOR INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
		UPDATE [$(DatabaseName)].dbo.ClientProgram
		SET ProgramAbbreviation = inserted.Program, ProgramDescription = inserted.Description, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM [$(DatabaseName)].dbo.ClientProgram ClientProgram INNER JOIN
		inserted ON ClientProgram.LegacyProgramId = inserted.Program LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	END
	--INSERT
	ELSE
	BEGIN
		INSERT INTO [$(DatabaseName)].dbo.ClientProgram ([LegacyProgramId]
           ,[ClientId]
           ,[ProgramAbbreviation]
           ,[ProgramDescription]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT inserted.Program, Client.ClientID, inserted.Program, inserted.[Description], VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN 
			[$(DatabaseName)].dbo.Client Client ON inserted.Client = Client.ClientAbrv LEFT OUTER JOIN
			[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	END
END

