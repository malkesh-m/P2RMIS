CREATE TRIGGER [MtgHotelsLuSyncTrigger]
ON [$(P2RMIS)].dbo.[MTG_Hotels_LU]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[Hotel]
	SET [HotelName] = MTG_Hotels_LU.Hotel_Name
      ,[HotelAbbreviation] = [MTG_Hotels_LU].[Abbreviation]
      ,[Address] = [MTG_Hotels_LU].[Address]
      ,[City] = [MTG_Hotels_LU].[City]
      ,[StateId] = [State].StateId
      ,[CountryId] = [Country].CountryId
      ,[ZipCode] = [MTG_Hotels_LU].[Zip_Code]
      ,[ModifiedBy] = VUN.UserId
      ,[ModifiedDate] = MTG_Hotels_LU.LAST_UPDATE_DATE
	  FROM inserted MTG_Hotels_LU INNER JOIN
		[$(DatabaseName)].dbo.[Hotel] Hotel ON MTG_Hotels_LU.Hotel_ID = [Hotel].LegacyHotelId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.[State] [State] ON MTG_Hotels_LU.[State] = [State].StateAbbreviation LEFT OUTER JOIN
		[$(DatabaseName)].dbo.[Country] [Country] ON MTG_Hotels_LU.[Country] = [Country].CountryAbbreviation LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON MTG_Hotels_LU.LAST_UPDATED_BY = VUN.UserName
		WHERE Hotel.DeletedFlag = 0
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[Hotel]
           ([HotelName]
           ,[HotelAbbreviation]
           ,[Address]
           ,[City]
           ,[StateId]
           ,[CountryId]
           ,[ZipCode]
           ,[LegacyHotelId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT [MTG_Hotels_LU].[Hotel_Name]
	  ,[MTG_Hotels_LU].[Abbreviation]
      ,[MTG_Hotels_LU].[Address]
      ,[MTG_Hotels_LU].[City]
      ,[State].StateId
	  ,[Country].[CountryId]
      ,[MTG_Hotels_LU].[Zip_Code]
	  ,[MTG_Hotels_LU].[Hotel_ID]
      ,VUN.UserId
	  ,[MTG_Hotels_LU].[LAST_UPDATE_DATE]
	  ,VUN.UserId
	  ,[MTG_Hotels_LU].[LAST_UPDATE_DATE]
	FROM inserted MTG_Hotels_LU LEFT OUTER JOIN
	[$(DatabaseName)].dbo.[State] [State] ON MTG_Hotels_LU.[State] = [State].StateAbbreviation LEFT OUTER JOIN
	[$(DatabaseName)].dbo.[Country] [Country] ON MTG_Hotels_LU.[Country] = [Country].CountryAbbreviation LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON MTG_Hotels_LU.LAST_UPDATED_BY = VUN.UserName
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[Hotel] 
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.[Hotel] Hotel ON deleted.Hotel_ID = [Hotel].LegacyHotelId	
	END
END