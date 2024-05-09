CREATE TRIGGER [MtgHousingSyncTrigger]
ON [$(P2RMIS)].dbo.[MTG_Housing]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationHotel]
	SET [MeetingRegistrationId] = MeetingRegistrationHotel.MeetingRegistrationId
      ,[HotelRequiredFlag] = CASE WHEN MTG_Housing.NoHotel = 0 THEN 1 ELSE 0 END
      ,[HotelId] = Hotel.HotelId
      ,[HotelCheckInDate] = MTG_Housing.CheckIn
      ,[HotelCheckOutDate] = MTG_Housing.CheckOut
      ,[HotelDoubleOccupancy] = ISNULL(MTG_Housing.Double_Occupancy, 0)
      ,[HotelAndFoodRequestComments] = MTG_Housing.Additional_Nights
	  ,[CancellationFlag] = CASE WHEN MTG_Housing.Canceled IS NULL THEN 0 ELSE 1 END
	  ,[CancellationDate] = MTG_Housing.Canceled
      ,[ModifiedBy] = VUN.UserId
      ,[ModifiedDate] = MTG_Housing.Last_Update_Date
	FROM inserted MTG_Housing INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistrationHotel MeetingRegistrationHotel ON MTG_Housing.Housing_Id = MeetingRegistrationHotel.LegacyHousingId INNER JOIN
		[$(DatabaseName)].[dbo].Hotel Hotel ON MTG_Housing.Hotel_ID = Hotel.LegacyHotelId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON MTG_Housing.LAST_UPDATED_BY = VUN.UserName
	WHERE MeetingRegistrationHotel.DeletedFlag = 0

	IF UPDATE(Part_Info_Verified) AND EXISTS (Select * From inserted WHERE Part_Info_Verified IS NOT NULL)
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistration]
	SET RegistrationSubmittedFlag = 1, RegistrationSubmittedDate = Part_Info_Verified
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON inserted.MR_ID = MeetingRegistration.LegacyMrId
	WHERE MeetingRegistration.DeletedFlag = 0	
	-- Internal comments
	IF UPDATE(Internal_Comments)
		BEGIN
			IF EXISTS (Select * From [$(DatabaseName)].[dbo].[MeetingRegistrationComment] INNER JOIN
					[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON [MeetingRegistrationComment].MeetingRegistrationId = MeetingRegistration.MeetingRegistrationId INNER JOIN
					inserted ON inserted.MR_ID = MeetingRegistration.LegacyMrId)
				UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationComment]
					SET MeetingRegistrationComment.InternalComments = Internal_Comments
					FROM [$(DatabaseName)].[dbo].[MeetingRegistrationComment]
					INNER JOIN
					[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON [MeetingRegistrationComment].MeetingRegistrationId = MeetingRegistration.MeetingRegistrationId INNER JOIN
					inserted ON inserted.MR_ID = MeetingRegistration.LegacyMrId
			ELSE
				INSERT INTO [$(DatabaseName)].[dbo].[MeetingRegistrationComment]
					([MeetingRegistrationId]
				   ,[InternalComments]
				   ,[CreatedBy]
				   ,[CreatedDate]
				   ,[ModifiedBy]
				   ,[ModifiedDate])
				SELECT MeetingRegistration.MeetingRegistrationId, Internal_Comments, VUN.UserId, MTG_Housing.Last_Update_Date, VUN.UserId, MTG_Housing.Last_Update_Date
				FROM inserted MTG_Housing INNER JOIN
				[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON MTG_Housing.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
				[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON MTG_Housing.LAST_UPDATED_BY = VUN.UserName
		END

	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[MeetingRegistrationHotel]
           ([MeetingRegistrationId]
           ,[HotelRequiredFlag]
           ,[HotelId]
           ,[HotelCheckInDate]
           ,[HotelCheckOutDate]
           ,[HotelDoubleOccupancy]
           ,[HotelAndFoodRequestComments]
           ,[LegacyHousingId]
		   ,[CancellationFlag]
		   ,[CancellationDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT MeetingRegistration.MeetingRegistrationId, CASE WHEN MTG_Housing.NoHotel = 0 THEN 1 ELSE 0 END, Hotel.HotelId, MTG_Housing.CheckIn, MTG_Housing.CheckOut, ISNULL(MTG_Housing.Double_Occupancy, 0),
	(CASE WHEN MTG_Housing.Special_Requests IS NULL OR MTG_Housing.Special_Requests = '' THEN MTG_Housing.Additional_Nights
		WHEN MTG_Housing.Additional_Nights IS NULL OR MTG_Housing.Additional_Nights = '' THEN MTG_Housing.Special_Requests
		ELSE SUBSTRING(LTRIM(RTRIM(MTG_Housing.Additional_Nights)) + ';' + LTRIM(RTRIM(MTG_Housing.Special_Requests)), 1, 255) END), 
	MTG_Housing.Housing_ID, CASE WHEN MTG_Housing.Canceled IS NULL THEN 0 ELSE 1 END, MTG_Housing.Canceled, VUN.UserId, MTG_Housing.Last_Update_Date, VUN.UserId, MTG_Housing.Last_Update_Date
	FROM inserted MTG_Housing INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON MTG_Housing.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].Hotel Hotel ON MTG_Housing.Hotel_ID = Hotel.LegacyHotelId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON MTG_Housing.LAST_UPDATED_BY = VUN.UserName
	WHERE MeetingRegistration.DeletedFlag = 0
	IF UPDATE(Part_Info_Verified) AND EXISTS (Select * From inserted WHERE Part_Info_Verified IS NOT NULL)
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistration]
	SET RegistrationSubmittedFlag = 1, RegistrationSubmittedDate = Part_Info_Verified
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON inserted.MR_ID = MeetingRegistration.LegacyMrId
	WHERE MeetingRegistration.DeletedFlag = 0
	-- Internal comments
	IF UPDATE(Internal_Comments)
		BEGIN
			IF EXISTS (Select * From [$(DatabaseName)].[dbo].[MeetingRegistrationComment] INNER JOIN
					[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON [MeetingRegistrationComment].MeetingRegistrationId = MeetingRegistration.MeetingRegistrationId INNER JOIN
					inserted ON inserted.MR_ID = MeetingRegistration.LegacyMrId)
				UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationComment]
					SET MeetingRegistrationComment.InternalComments = Internal_Comments
					FROM [$(DatabaseName)].[dbo].[MeetingRegistrationComment]
					INNER JOIN
					[$(DatabaseName)].[dbo].[MeetingRegistration] MeetingRegistration ON [MeetingRegistrationComment].MeetingRegistrationId = MeetingRegistration.MeetingRegistrationId INNER JOIN
					inserted ON inserted.MR_ID = MeetingRegistration.LegacyMrId
			ELSE
				INSERT INTO [$(DatabaseName)].[dbo].[MeetingRegistrationComment]
					([MeetingRegistrationId]
				   ,[InternalComments]
				   ,[CreatedBy]
				   ,[CreatedDate]
				   ,[ModifiedBy]
				   ,[ModifiedDate])
				SELECT MeetingRegistration.MeetingRegistrationId, Internal_Comments, VUN.UserId, MTG_Housing.Last_Update_Date, VUN.UserId, MTG_Housing.Last_Update_Date
				FROM inserted MTG_Housing INNER JOIN
				[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON MTG_Housing.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
				[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON MTG_Housing.LAST_UPDATED_BY = VUN.UserName
		END
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationHotel] 
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistrationHotel MeetingRegistrationHotel ON deleted.Housing_Id = MeetingRegistrationHotel.LegacyHousingId
	WHERE MeetingRegistrationHotel.DeletedFlag = 0
	END
END