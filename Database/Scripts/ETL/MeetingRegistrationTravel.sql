INSERT INTO [dbo].[MeetingRegistrationTravel]
           ([MeetingRegistrationId]
           ,[TravelModeId]
           ,[TravelRequestComments]
           ,[LegacyTravelId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT MeetingRegistration.MeetingRegistrationId, TravelMode.TravelModeId, MTG_Travel.Comments, MTG_Travel.Travel_ID, VUN.UserId, MTG_Travel.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.MTG_Travel MTG_Travel INNER JOIN
	MeetingRegistration ON MTG_Travel.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
	TravelMode ON MTG_Travel.Travel_Mode = TravelMode.LegacyTravelModeAbbreviation LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON MTG_Travel.LAST_UPDATED_BY = VUN.UserName