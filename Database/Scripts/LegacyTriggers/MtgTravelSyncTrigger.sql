CREATE TRIGGER [MtgTravelSyncTrigger]
ON [$(P2RMIS)].dbo.[Mtg_Travel]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationTravel]
	SET [MeetingRegistrationId] = MeetingRegistrationTravel.MeetingRegistrationId
      ,[TravelModeId] = TravelMode.TravelModeId
      ,[TravelRequestComments] = MTG_Travel.Comments
      ,[LegacyTravelId] = MTG_Travel.Travel_ID
	  ,[CancellationFlag] = CASE WHEN MTG_Travel.Canceled IS NULL THEN 0 ELSE 1 END
	  ,[CancellationDate] = MTG_Travel.Canceled
	  ,[NteAmount] = MTG_Travel.NTE_Amount
      ,[ModifiedBy] = VUN.UserId
      ,[ModifiedDate] = MTG_Travel.LAST_UPDATE_DATE
	FROM inserted MTG_Travel INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistrationTravel MeetingRegistrationTravel ON MTG_Travel.Travel_ID = MeetingRegistrationTravel.LegacyTravelId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].TravelMode TravelMode ON MTG_Travel.Travel_Mode = TravelMode.LegacyTravelModeAbbreviation LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON MTG_Travel.LAST_UPDATED_BY = VUN.UserName
	WHERE MeetingRegistrationTravel.DeletedFlag = 0
	IF UPDATE(Part_Info_Verified) AND EXISTS (Select * From inserted WHERE Part_Info_Verified IS NOT NULL)
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistration]
	SET RegistrationSubmittedFlag = 1, RegistrationSubmittedDate = Part_Info_Verified
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON inserted.MR_ID = MeetingRegistration.LegacyMrId
	WHERE MeetingRegistration.DeletedFlag = 0
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[MeetingRegistrationTravel]
           ([MeetingRegistrationId]
           ,[TravelModeId]
           ,[TravelRequestComments]
           ,[LegacyTravelId]
		   ,[CancellationFlag]
		   ,[CancellationDate]
		   ,[NteAmount]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT MeetingRegistration.MeetingRegistrationId, TravelMode.TravelModeId, MTG_Travel.Comments, MTG_Travel.Travel_ID, CASE WHEN MTG_Travel.Canceled IS NULL THEN 0 ELSE 1 END, MTG_Travel.Canceled, MTG_Travel.NTE_Amount, VUN.UserId, MTG_Travel.LAST_UPDATE_DATE, VUN.UserId, MTG_Travel.LAST_UPDATE_DATE
	FROM inserted MTG_Travel INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON MTG_Travel.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].TravelMode TravelMode ON MTG_Travel.Travel_Mode = TravelMode.LegacyTravelModeAbbreviation LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON MTG_Travel.LAST_UPDATED_BY = VUN.UserName
	WHERE MeetingRegistration.DeletedFlag = 0
	IF UPDATE(Part_Info_Verified) AND EXISTS (Select * From inserted WHERE Part_Info_Verified IS NOT NULL)
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistration]
	SET RegistrationSubmittedFlag = 1, RegistrationSubmittedDate = Part_Info_Verified
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON inserted.MR_ID = MeetingRegistration.LegacyMrId
	WHERE MeetingRegistration.DeletedFlag = 0
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationTravel] 
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistrationTravel MeetingRegistrationTravel ON deleted.Travel_ID = MeetingRegistrationTravel.LegacyTravelId
	WHERE MeetingRegistrationTravel.DeletedFlag = 0
	END
END