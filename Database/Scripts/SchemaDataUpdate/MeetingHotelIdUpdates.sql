UPDATE [ClientMeeting]
SET HotelId = Hotel.HotelId
FROM ClientMeeting
INNER JOIN [$(P2RMIS)].dbo.MTG_Master MTG_Master ON ClientMeeting.LegacyMeetingId = MTG_Master.Meeting_ID
INNER JOIN Hotel Hotel ON MTG_Master.Hotel_Id = Hotel.LegacyHotelId
WHERE ClientMeeting.HotelId IS NULL