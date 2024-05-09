CREATE TRIGGER [MtgMeetingInfoSyncTrigger]
ON [$(P2RMIS)].dbo.MTG_Meeting_Info
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[ProgramMeetingInformation]
	   SET [ProgramYearId] = ProgramYear.ProgramYearId
		  ,[DocumentName] = inserted.Heading
		  ,[DocumentDescription] = inserted.[Description]
		  ,[FileLocation] = inserted.Link
		  ,[ExternalAddressFlag] = CASE WHEN LEFT(inserted.Link, 1) = '/' THEN 0 ELSE 1 END
		  ,[ActiveFlag] = inserted.Active
		  ,[LegacyMiId] = inserted.MI_ID
		  ,[ModifiedBy] = VUN.UserId
		  ,[ModifiedDate] = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].dbo.ProgramMeetingInformation ProgramMeetingInformation ON inserted.MI_ID = ProgramMeetingInformation.LegacyMiId INNER JOIN
	[$(DatabaseName)].dbo.ClientProgram ClientProgram ON inserted.Program = ClientProgram.LegacyProgramId INNER JOIN
	[$(DatabaseName)].dbo.ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId AND inserted.FY = ProgramYear.[Year] LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.Last_Updated_By = VUN.UserName
	WHERE ProgramMeetingInformation.DeletedFlag = 0
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted)
	INSERT INTO [$(DatabaseName)].[dbo].[ProgramMeetingInformation]
           ([ProgramYearId]
           ,[DocumentName]
           ,[DocumentDescription]
           ,[FileLocation]
           ,[ExternalAddressFlag]
           ,[ActiveFlag]
           ,[LegacyMiId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ProgramYear.ProgramYearId, MTG_Meeting_Info.Heading, MTG_Meeting_Info.[Description], MTG_Meeting_Info.Link, CASE WHEN LEFT(MTG_Meeting_Info.Link, 1) = '/' THEN 0 ELSE 1 END,
	MTG_Meeting_Info.Active, MTG_Meeting_Info.MI_ID, VUN.UserId, MTG_Meeting_Info.LAST_UPDATE_DATE, VUN.UserId, MTG_Meeting_Info.LAST_UPDATE_DATE
	FROM inserted MTG_Meeting_Info INNER JOIN
		[$(DatabaseName)].dbo.ClientProgram ClientProgram ON MTG_Meeting_Info.Program = ClientProgram.LegacyProgramId INNER JOIN
		[$(DatabaseName)].dbo.ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId AND MTG_Meeting_Info.FY = ProgramYear.[Year] LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON MTG_Meeting_Info.Last_Updated_By = VUN.UserName
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[ProgramMeetingInformation]
	SET DeletedFlag = 1, DeletedDate = SYSDATETIME()
	FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ProgramMeetingInformation ProgramMeetingInformation ON deleted.MI_ID = ProgramMeetingInformation.LegacyMiId
	WHERE ProgramMeetingInformation.DeletedFlag = 0
END
