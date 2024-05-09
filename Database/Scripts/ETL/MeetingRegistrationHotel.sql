INSERT INTO [dbo].[MeetingRegistrationHotel]
           ([MeetingRegistrationId]
           ,[HotelNotRequiredFlag]
           ,[HotelId]
           ,[HotelCheckInDate]
           ,[HotelCheckOutDate]
           ,[HotelDoubleOccupancy]
           ,[HotelAndFoodRequestComments]
           ,[LegacyHousingId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT MeetingRegistration.MeetingRegistrationId, CASE WHEN MTG_Housing.NoHotel = 0 THEN 1 ELSE 0 END, Hotel.HotelId, MTG_Housing.CheckIn, MTG_Housing.CheckOut, ISNULL(MTG_Housing.Double_Occupancy, 0),
	MTG_Housing.Additional_Nights, MTG_Housing.Housing_ID, VUN.UserId, MTG_Housing.Last_Update_Date
FROM [$(P2RMIS)].dbo.MTG_Housing MTG_Housing INNER JOIN
	MeetingRegistration ON MTG_Housing.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
	Hotel ON MTG_Housing.Hotel_ID = Hotel.LegacyHotelId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON MTG_Housing.LAST_UPDATED_BY = VUN.UserName