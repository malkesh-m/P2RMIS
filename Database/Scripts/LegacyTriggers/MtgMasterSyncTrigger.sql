CREATE TRIGGER [MtgMasterSyncTrigger]
ON [$(P2RMIS)].dbo.MTG_Master
FOR UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON

	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
		UPDATE [$(DatabaseName)].dbo.ClientMeeting
		SET LegacyMeetingId = inserted.Meeting_ID, MeetingAbbreviation = inserted.Meeting_ID, MeetingDescription = inserted.Meeting_Desc, MeetingLocation = inserted.Meeting_Location,
			StartDate = inserted.Start_Date, EndDate = inserted.End_Date, ModifiedBy = VUN.UserId,
			ModifiedDate = inserted.LAST_UPDATE_DATE, MeetingTypeId = MeetingType.MeetingTypeId, HotelId = Hotel.HotelId
		FROM deleted CROSS JOIN
			inserted INNER JOIN
			[$(DatabaseName)].dbo.ClientMeeting ClientMeeting ON deleted.Meeting_ID = ClientMeeting.LegacyMeetingId LEFT OUTER JOIN
			[$(DatabaseName)].dbo.MeetingType MeetingType ON inserted.Review_Type = MeetingType.LegacyMeetingTypeId LEFT OUTER JOIN
			[$(DatabaseName)].dbo.Hotel Hotel ON inserted.Hotel_ID = Hotel.LegacyHotelId LEFT OUTER JOIN
			[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	--DELETE
	ELSE
		UPDATE [$(DatabaseName)].dbo.ClientMeeting
		SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].dbo.ClientMeeting ClientMeeting ON deleted.Meeting_ID = ClientMeeting.LegacyMeetingId
		WHERE ClientMeeting.DeletedFlag = 0
END
