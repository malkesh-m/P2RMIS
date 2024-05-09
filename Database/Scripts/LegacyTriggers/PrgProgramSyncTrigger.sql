CREATE TRIGGER [PrgProgramSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Program
FOR INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
		UPDATE [$(DatabaseName)].dbo.ProgramYear
		SET DateClosed = inserted.Closed, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM [$(DatabaseName)].dbo.ProgramYear ProgramYear INNER JOIN
		inserted ON ProgramYear.LegacyProgramId = inserted.PRG_ID LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	END
	--INSERT
	ELSE
	BEGIN
		INSERT INTO [$(DatabaseName)].dbo.ProgramYear ([LegacyProgramId]
           ,[ClientProgramId]
           ,[Year]
           ,[DateClosed]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[ModifiedDate]
           ,[ModifiedBy])
		SELECT inserted.PRG_ID, ClientProgram.ClientProgramId, inserted.FY, inserted.Closed, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId
		FROM inserted INNER JOIN 
			[$(DatabaseName)].dbo.ClientProgram ClientProgram ON inserted.Program = ClientProgram.LegacyProgramId LEFT OUTER JOIN
			[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	END
END
