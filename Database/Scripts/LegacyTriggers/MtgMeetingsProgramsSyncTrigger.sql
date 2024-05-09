CREATE TRIGGER [MtgMeetingsProgramsSyncTrigger]
ON [$(P2RMIS)].dbo.MTG_Meetings_Programs
FOR INSERT, DELETE
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (Select * FROM inserted) 
	BEGIN
	--This is a unique situation, since MTG is now by client, we only add a MTG when program is assigned
	INSERT INTO [$(DatabaseName)].dbo.ClientMeeting
	([LegacyMeetingId]
           ,[ClientId]
           ,[MeetingAbbreviation]
           ,[MeetingDescription]
           ,[StartDate]
           ,[EndDate]
           ,[MeetingLocation]
		   ,[HotelId]
           ,[MeetingTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT DISTINCT MTG_Master.Meeting_ID, Client.ClientID, MTG_Master.Meeting_ID, MTG_Master.Meeting_Desc, MTG_Master.Start_Date,
	MTG_Master.End_Date, MTG_Master.Meeting_Location, Hotel.HotelId, MeetingType.MeetingTypeId, VUN.UserId, MTG_Master.LAST_UPDATE_DATE, VUN.UserId, MTG_Master.LAST_UPDATE_DATE
	FROM [$(P2RMIS)].dbo.MTG_Master INNER JOIN
		[inserted] ON MTG_Master.Meeting_ID = [inserted].Meeting_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_Program_LU PRG_Program_LU ON inserted.Program = PRG_Program_LU.PROGRAM INNER JOIN
		[$(DatabaseName)].dbo.Client Client ON PRG_Program_LU.CLIENT = Client.ClientAbrv	LEFT OUTER JOIN
		[$(DatabaseName)].dbo.MeetingType MeetingType ON MTG_Master.Review_Type = MeetingType.LegacyMeetingTypeId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ClientMeeting ClientMeeting ON MTG_Master.Meeting_ID = ClientMeeting.LegacyMeetingId AND Client.ClientID = ClientMeeting.ClientId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.Hotel Hotel ON MTG_Master.Hotel_ID = Hotel.LegacyHotelId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE ClientMeeting.ClientMeetingId IS NULL;
	
	INSERT INTO [$(DatabaseName)].[dbo].[ProgramMeeting]
           ([ProgramYearId]
           ,[ClientMeetingId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ProgramYear.ProgramYearId, ClientMeeting.ClientMeetingId, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].dbo.ClientProgram ClientProgram ON inserted.Program = ClientProgram.LegacyProgramId INNER JOIN
		[$(DatabaseName)].dbo.ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId AND inserted.FY = ProgramYear.Year INNER JOIN
		[$(DatabaseName)].dbo.ViewClientMeeting ClientMeeting ON ClientProgram.ClientId = ClientMeeting.ClientId AND inserted.Meeting_ID = ClientMeeting.LegacyMeetingId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ProgramMeeting] 
	SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ClientProgram ClientProgram ON deleted.Program = ClientProgram.LegacyProgramId INNER JOIN
		[$(DatabaseName)].dbo.ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId AND deleted.FY = ProgramYear.Year INNER JOIN
		[$(DatabaseName)].dbo.ViewClientMeeting ClientMeeting ON ClientProgram.ClientId = ClientMeeting.ClientId AND deleted.Meeting_ID = ClientMeeting.LegacyMeetingId INNER JOIN
		[$(DatabaseName)].dbo.ProgramMeeting ProgramMeeting ON ProgramYear.ProgramYearId = ProgramMeeting.ProgramYearId AND ClientMeeting.ClientMeetingId = ProgramMeeting.ClientMeetingId
	WHERE ProgramMeeting.DeletedFlag = 0
	END
END
