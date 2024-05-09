INSERT INTO [dbo].[Hotel]
           ([HotelName]
           ,[HotelAbbreviation]
           ,[Address]
           ,[City]
           ,[StateId]
           ,[CountryId]
           ,[ZipCode]
           ,[LegacyHotelId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT MTG_Hotels_LU.Hotel_Name, MTG_Hotels_LU.Abbreviation, MTG_Hotels_LU.[Address], MTG_Hotels_LU.City, [State].StateId,
		 Country.CountryId, MTG_Hotels_LU.Zip_Code, MTG_Hotels_LU.Hotel_ID, VUN.UserId, MTG_Hotels_LU.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.MTG_Hotels_LU MTG_Hotels_LU LEFT OUTER JOIN
	[State] ON MTG_Hotels_LU.[State] = [State].StateAbbreviation LEFT OUTER JOIN
	[Country] ON MTG_Hotels_LU.[Country] = [Country].CountryAbbreviation LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON MTG_Hotels_LU.LAST_UPDATED_BY = VUN.UserName
	